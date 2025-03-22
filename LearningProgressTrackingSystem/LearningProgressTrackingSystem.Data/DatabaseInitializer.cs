using LearningProgressTrackingSystem.Domain.Entities;
using LearningProgressTrackingSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Data;

public static class DatabaseInitializer
{
    public static void Initialize(LearningProgressTrackingSystemContext context)
    {
        try
        {
            context.Database.Migrate();
            Console.WriteLine("Database initialization completed successfully.");
            
            AddStartEntities(context);
            Console.WriteLine("Adding start entities completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
            throw;
        }
    }

    private static void AddStartEntities(LearningProgressTrackingSystemContext context)
    {
        if (!context.Accounts.Any())
        {
            context.Accounts.AddRange(new AccountEntity
            {
                Login = "account1",
                Password = "account1",
                Role = AccountRole.Student
            }, new AccountEntity
            {
                Login = "account2",
                Password = "account2",
                Role = AccountRole.Teacher
            });

            context.SaveChanges();
            Console.WriteLine("Accounts added successfully.");
        }

        if (!context.Students.Any())
        {
            var studentAccount = context.Accounts.First(account => account.Role == AccountRole.Student);

            context.Students.Add(new Student
            {
                Account = studentAccount,
                Name = "student1",
                Email = "student1@student.com",
                EnrollmentDate = DateOnly.FromDateTime(DateTime.UtcNow)
            });

            context.SaveChanges();
            Console.WriteLine("Student added successfully.");
        }

        if (!context.Teachers.Any())
        {
            var teacherAccount = context.Accounts.First(account => account.Role == AccountRole.Teacher);

            context.Teachers.Add(new Teacher
            {
                Account = teacherAccount,
                Email = "teacher1@teacher.com",
                Name = "teacher1"
            });

            context.SaveChanges();
            Console.WriteLine("Teacher added successfully.");
        }

        if (!context.Teachers.Any())
        {
            var teacher = context.Teachers.First();

            context.Courses.Add(new Course
            {
                Teacher = teacher,
                Description = "desc",
                Title = "Course title"
            });

            context.SaveChanges();
            Console.WriteLine("Course added successfully.");
        }
    }
}