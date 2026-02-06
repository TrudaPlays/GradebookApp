using System;
using System.Collections.Generic;
using System.Linq;

namespace GradebookApp;

class Program
{
    static void Main()
    {
        Console.Title = "Gradebook App";
        var gradebook = new Gradebook();

        while (true)
        {
            Console.Clear(); //clears the console
            DisplayMenu(); //displays the menu for the user to choose options from
            Console.Write("\nChoose an option (1-7): ");

            string? choice = Console.ReadLine()?.Trim();

            try
            {
                switch (choice) //switching through the different cases depending on user input
                {
                    case "1":
                        AddSingleGrade(gradebook); //adds one grade to the gradebook
                        break;

                    case "2":
                        AddMultipleGrades(gradebook); //adds multiple grades in a string separated by commas
                        break;

                    case "3":
                        ShowSummary(gradebook); //prints a summary including the average, highest, lowest and number of grades
                        break;

                    case "4":
                        ClearGrades(gradebook); //deletes everything in the gradebook
                        break;

                    case "5":
                        ListAllGrades(gradebook); //lists all the grades in unsorted order (the order the user puts them in)
                        break;

                    case "6":
                        PrintSortedGrades(gradebook); //prints out a neatly sorted grade list
                        break;

                    case "7":
                        Console.WriteLine("\nThank you for using Gradebook App. Goodbye!"); //leaves the gradebookapp
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6."); //if a number other than 1-7 is entered
                        break;
                }
            }//catching errors and throwing error messages to handle user error gracefully
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nError: Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }

    static void DisplayMenu() //displays a menu
    {
        Console.WriteLine("Gradebook App");
        Console.WriteLine(" 1. Add single grade");
        Console.WriteLine(" 2. Add multiple grades in a string");
        Console.WriteLine(" 3. View summary of the grades (including Average, Highest and Lowest Grade values and the total number of grades");
        Console.WriteLine(" 4. Clear all grades from your gradebook (warning this cannot be undone)");
        Console.WriteLine(" 5. List all grades in the order you inputted them");
        Console.WriteLine(" 6. View your grades sorted from lowest to highest");
        Console.WriteLine(" 7. Exit");
    }

    static void PrintSortedGrades(Gradebook book) //prints a sorted grade list from a previously made list that sorted all the lists in the gradebook
    {
        // Get the sorted list from your method
        var sortedGrades = book.GetSortedGradesInAscendingOrder();

        // Check if there are any grades
        if (sortedGrades.Count == 0) // in case this is called before any grades are entered
        {
            Console.WriteLine("\nNo grades recorded yet.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        // Prints a nice header
        Console.WriteLine("\nSorted Grades (Lowest to Highest)");
        Console.WriteLine("---------------------------------");

        // Loop through and prints each grade with one decimal place formatting
        for (int i = 0; i < sortedGrades.Count; i++)
        {
            string positionLabel = (i == 0) ? "Lowest" :
                                  (i == sortedGrades.Count - 1) ? "Highest" :
                                  $"{i + 1}";

            // Format: position | grade (one decimal place)
            Console.WriteLine($"{positionLabel,-10} | {sortedGrades[i],6:F1}");
        }

        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Total grades: {sortedGrades.Count}");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }


    static void AddSingleGrade(Gradebook gradebook)
    {
        while (true)   // ← outer loop: keeps asking until valid grade is added
        {
            Console.Write("Enter grade (0-100): ");
            string? input = Console.ReadLine()?.Trim(); //trims extra white space

            if (string.IsNullOrWhiteSpace(input)) //checks if just a blank space is given 
            {
                Console.WriteLine("No input received. Please enter a number.");
                continue;
            }

            if (double.TryParse(input, out double grade)) //tries to convert the input into a double value
            {
                try
                {
                    gradebook.AddGrade(grade);  // this will throw if < 0 or > 100
                    Console.WriteLine($"Grade {grade:F1} added successfully.");
                    return;                     // ← success → exit the method
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please try again.");
                    // loop continues → ask again
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                // loop continues → ask again
            }
        }
    }
         
    

    static void AddMultipleGrades(Gradebook gradebook)
    {

        while (true)
        {
            Console.WriteLine("Enter grades separated by commas or spaces (e.g. 85, 92 78 65.5):");
            Console.Write("> ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No grades entered.");
                return;
            }

            var gradeStrings = input.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var grades = new List<double>();

            foreach (var gs in gradeStrings)
            {
                if (double.TryParse(gs, out double g))
                {
                    grades.Add(g);
                }
                else
                {
                    Console.WriteLine($"Skipping invalid entry: {gs}");
                }
            }

            if (grades.Count > 0)
            {
                gradebook.AddGrade(grades);
                Console.WriteLine($"Added {grades.Count} valid grade(s).");
                return;
            }
            else
            {
                Console.WriteLine("No valid grades were entered.");
            }
        }
    }
        

    static void ShowSummary(Gradebook gradebook) //shows average, highest, lowest and number of grades
    {
        Console.WriteLine("\nGradebook summary");

        int count = gradebook.GetCount();
        if (count == 0)
        {
            Console.WriteLine("No grades recorded yet."); //for an empty gradebook
            return;
        }

        Console.WriteLine($"Number of grades: {count}");
        Console.WriteLine($"Average:       {gradebook.GetAverage():F2}");
        Console.WriteLine($"Highest:       {gradebook.GetHighest():F1}");
        Console.WriteLine($"Lowest:        {gradebook.GetLowest():F1}");
    }

    static void ClearGrades(Gradebook gradebook) //deletes everything!! :)
    {
        Console.Write("Are you sure you want to clear all grades? (yes/no): ");
        string? confirm = Console.ReadLine()?.Trim().ToLower();

        if (confirm == "yes" || confirm == "y")
        {
            gradebook.Clear();
            Console.WriteLine("All grades have been cleared.");
        }
        else
        {
            Console.WriteLine("Clear operation cancelled.");
        }
    }

    static void ListAllGrades(Gradebook gradebook) //spits out a messy list of grades
    {
        var grades = gradebook.Grades;
        if (grades.Count == 0)
        {
            Console.WriteLine("No grades to display.");
            return;
        }

        Console.WriteLine($"\nAll grades ({grades.Count}):");

        int i = 1;
        foreach (var grade in grades)
        {
            Console.WriteLine($"{i,3}. {grade,5:F1}");
            i++;
        }
    }
}