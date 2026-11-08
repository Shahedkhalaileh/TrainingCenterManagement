using System;
using System.Collections.Generic;
using System.Text;
using TrainingCenterManagement.Models;

namespace TrainingCenterManagement.Data
{
    public class TrainingCenterData
    {

        // Main data collections

        public Dictionary<int, Student> Students { get; } = new();

        public Dictionary<int, Instructor> Instructors { get; } = new();

        public Dictionary<int, Course> Courses { get; } = new();

        // Historical records
        public List<Enrollment> Enrollments { get; } = new();
        public List<PaymentAttempt> PaymentAttempts { get; } = new();

        // Duplicate prevention
        public HashSet<string> StudentEmails { get; } =
            new(StringComparer.OrdinalIgnoreCase);

        public HashSet<string> InstructorEmails { get; } =
            new(StringComparer.OrdinalIgnoreCase);

        // Waiting lists: CourseId → Queue of StudentIds
        public Dictionary<int, Queue<int>> WaitingQueues { get; } = new();
    }
}
