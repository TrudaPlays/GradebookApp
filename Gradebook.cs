using System;
using System.Collections.Generic;
using System.Linq;

namespace GradebookApp;

public class Gradebook
{
    private readonly List<double> _grades = new List<double>();

    public void AddGrade(double grade)
    {
        if (grade < 0 || grade > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 0 and 100 inclusive.");
        }
        _grades.Add(grade);
    }

    public void AddGrade(IEnumerable<double> grades) //validates the grades when adding multiple grades at once
    {
        if (grades == null) 
        {
            throw new ArgumentNullException(nameof(grades));
        }

        foreach (var grade in grades) //checks if the grade is less than 0 or greater than 100 and throws an error message
        {
            if (grade < 0 || grade > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(grade), $"Invalid grade: {grade}. All grades must be between 0 and 100.");
            }
        }

        _grades.AddRange(grades);
    }

    public void PrintSortedGrades()
    {
        if (_grades.Count == 0)
        {
            Console.WriteLine("No grades recorded yet.");
            return;
        }

        var sorted = _grades.OrderBy(g => g).ToList();

        Console.WriteLine($"\nSorted Grades (lowest to highest");

        for (int i = 0; i < sorted.Count; i++)
        {
            string position = (i == 0) ? "Lowest" :
                             (i == sorted.Count - 1) ? "Highest" :
                             (i + 1).ToString();

            Console.WriteLine($"{position,-8} | {sorted[i],6:F1}");
        }

        Console.WriteLine("───────────────────────────────────────────────────────");
    }

    public double GetAverage() //gets the average of the grades in the gradebook
    {
        if (_grades.Count == 0) return 0;
        return _grades.Average();
    }

    public double GetHighest() //finds the highest grade in the gradebook using the .Max function
    {
        if (_grades.Count == 0) return 0;
        return _grades.Max();
    }

    public double GetLowest() //finds the lowest grade in the gradebook using the .Min function
    {
        if (_grades.Count == 0) return 0;
        return _grades.Min();
    }

    public int GetCount() //counts all the grades in the gradebook
    {
        return _grades.Count;
    }

    public void Clear() // clears the gradebook 
    {
        _grades.Clear(); 
    }

    // Makes the grades read-only from outside
    public IReadOnlyList<double> Grades => _grades.AsReadOnly();
}