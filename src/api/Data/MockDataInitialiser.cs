﻿using System;
using System.Linq;
using api.Models;

namespace api.Data
{
    public static class MockDataInitialiser
    {
        public static void InitializeMockIfEmpty(DataContext context)
        {
            context.Database.EnsureCreated();

            if (!InitWorkers(context)) return;
            if (!InitStudents(context)) return;

            InitCourses(context);
            InitEnrollment(context);
        }

        private static bool InitWorkers(DataContext context)
        {
            // Look for any students.
            if (context.Workers.Any())
            {
                return false;   // DB has been seeded
            }

            var workers = new Worker[]
            {
                new Worker { FirstName = "John", LastName = "Doe", Email = "john@example.com", StartDate = DateTime.Parse("2005-09-01")},
                new Worker { FirstName = "Mary", LastName = "Moe", Email = "mary@example.com", StartDate = DateTime.Parse("2005-09-01")},
                new Worker { FirstName = "July", LastName = "Dooley", Email = "july@example.com", StartDate = DateTime.Parse("2005-09-01")}
            };

            foreach (Worker w in workers)
            {
                context.Workers.Add(w);
            }
            context.SaveChanges();

            return true;
        }


        private static bool InitStudents(DataContext context)
        {
            // Look for any students.
            if (context.Students.Any())
            {
                return false;   // DB has been seeded
            }

            var students = new Student[]
            {
                new Student{FirstName="Carson",LastName="Alexander",Email="Carson@email.com",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstName="Meredith",LastName="Alonso",Email="Alonso@email.com",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Arturo",LastName="Anand",Email="Anand@email.com",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstName="Gytis",LastName="Barzdukas",Email="Barzdukas@email.com",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Yan",LastName="Li",Email="Li@email.com",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Peggy",LastName="Justice",Email="Justice@email.com",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstName="Laura",LastName="Norman",Email="Norman@email.com",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstName="Nino",LastName="Olivetto",Email="Olivetto@email.com",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            return true;
        }

        private static void InitCourses(DataContext context)
        {
            var courses = new Course[]
            {
                new Course{CourseID=1050,Title="Chemistry",Credits=3,},
                new Course{CourseID=4022,Title="Microeconomics",Credits=3,},
                new Course{CourseID=4041,Title="Macroeconomics",Credits=3,},
                new Course{CourseID=1045,Title="Calculus",Credits=4,},
                new Course{CourseID=3141,Title="Trigonometry",Credits=4,},
                new Course{CourseID=2021,Title="Composition",Credits=3,},
                new Course{CourseID=2042,Title="Literature",Credits=4,}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();
        }

        private static void InitEnrollment(DataContext context)
        {

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
                new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
                new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
                new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
                new Enrollment{StudentID=3,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=1050,},
                new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
                new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
                new Enrollment{StudentID=6,CourseID=1045},
                new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}
