using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            var studentsCount = Students.Count;

            if (studentsCount < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            var orderedStudents = Students.OrderByDescending(student => student.AverageGrade).ToList();
            int bins_number = 5;
            int bin_size = studentsCount / bins_number;

            var breakpointScores = new List<double>();

            for(var i = 1; i < studentsCount; i++)
            {
                if (i % bin_size == 0)
                {
                    breakpointScores.Add(orderedStudents[i-1].AverageGrade);
                }
            }

            if (averageGrade >= breakpointScores[0])
                return 'A';
            else if (averageGrade >= breakpointScores[1])
                return 'B';
            else if (averageGrade >= breakpointScores[2])
                return 'C';
            else if (averageGrade >= breakpointScores[3])
                return 'D';
            else
                return 'F';

        }
    }
}
