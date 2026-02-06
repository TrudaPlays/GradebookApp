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

    public void AddGrade(IEnumerable<double> grades)
    {
        if (grades == null)
        {
            throw new ArgumentNullException(nameof(grades));
        }

        foreach (var grade in grades)
        {
            if (grade < 0 || grade > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(grade), $"Invalid grade: {grade}. All grades must be between 0 and 100.");
            }
        }

        _grades.AddRange(grades);
    }

    public double GetAverage()
    {
        if (_grades.Count == 0) return 0;
        return _grades.Average();
    }

    public double GetHighest()
    {
        if (_grades.Count == 0) return 0;
        return _grades.Max();
    }

    public double GetLowest()
    {
        if (_grades.Count == 0) return 0;
        return _grades.Min();
    }

    public int GetCount()
    {
        return _grades.Count;
    }

    public void Clear()
    {
        _grades.Clear();
    }

    // Makes the grades read-only from outside
    public IReadOnlyList<double> Grades => _grades.AsReadOnly();
}