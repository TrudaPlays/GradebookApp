using System;
using System.Collections.Generic;
using System.Linq;

namespace GradebookApp;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Gradebook App";
        var gradebook = new Gradebook();

        while (true)
        {
            Console.Clear();
            DisplayMenu();
            Console.Write("\nChoose an option (1-6): ");

            string? choice = Console.ReadLine()?.Trim();

            try
            {
                switch (choice)
                {
                    case "1":
                        AddSingleGrade(gradebook);
                        break;

                    case "2":
                        AddMultipleGrades(gradebook);
                        break;

                    case "3":
                        ShowSummary(gradebook);
                        break;

                    case "4":
                        ClearGrades(gradebook);
                        break;

                    case "5":
                        ListAllGrades(gradebook);
                        break;

                    case "6":
                        Console.WriteLine("\nThank you for using Gradebook App. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
            }
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

    static void DisplayMenu()
    {
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║          GRADEBOOK APP             ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.WriteLine(" 1. Add single grade");
        Console.WriteLine(" 2. Add multiple grades");
        Console.WriteLine(" 3. View summary");
        Console.WriteLine(" 4. Clear all grades");
        Console.WriteLine(" 5. List all grades");
        Console.WriteLine(" 6. Exit");
        Console.WriteLine("══════════════════════════════════════");
    }

    static void AddSingleGrade(Gradebook gradebook)
    {
        Console.Write("Enter grade (0-100): ");
        string? input = Console.ReadLine()?.Trim();

        if (double.TryParse(input, out double grade))
        {
            gradebook.AddGrade(grade);
            Console.WriteLine($"Grade {grade:F1} added successfully.");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    static void AddMultipleGrades(Gradebook gradebook)
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
        }
        else
        {
            Console.WriteLine("No valid grades were entered.");
        }
    }

    static void ShowSummary(Gradebook gradebook)
    {
        Console.WriteLine("\n┌──────────────────────────────┐");
        Console.WriteLine("│         GRADE SUMMARY        │");
        Console.WriteLine("└──────────────────────────────┘");

        int count = gradebook.GetCount();
        if (count == 0)
        {
            Console.WriteLine("No grades recorded yet.");
            return;
        }

        Console.WriteLine($"Number of grades: {count}");
        Console.WriteLine($"Average:       {gradebook.GetAverage():F2}");
        Console.WriteLine($"Highest:       {gradebook.GetHighest():F1}");
        Console.WriteLine($"Lowest:        {gradebook.GetLowest():F1}");
        Console.WriteLine("──────────────────────────────");
    }

    static void ClearGrades(Gradebook gradebook)
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

    static void ListAllGrades(Gradebook gradebook)
    {
        var grades = gradebook.Grades;
        if (grades.Count == 0)
        {
            Console.WriteLine("No grades to display.");
            return;
        }

        Console.WriteLine($"\nAll grades ({grades.Count}):");
        Console.WriteLine("──────────────────────────────");

        int i = 1;
        foreach (var grade in grades)
        {
            Console.WriteLine($"{i,3}. {grade,5:F1}");
            i++;
        }
    }
}