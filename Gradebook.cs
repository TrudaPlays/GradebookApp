using System;
using System.Collections.Generic;
using System.Linq;

namespace GradebookApp;

public class Gradebook
{
    private readonly List<double> _grades = new List<double>();//creates a dictionary to store the grades in

    public void AddGrade(double grade) //adds a single grade
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

    public List<double> GetSortedGradesInAscendingOrder() //sorts the grades and stores them in a string for the program.cs to access later
    {
        if (_grades.Count == 0)
        {
            return new List<double>();  //if there are no grades
        }

        return _grades.OrderBy(g => g).ToList();
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