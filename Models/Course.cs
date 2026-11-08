using System;
using System.Collections.Generic;
using System.Text;
using TrainingCenterManagement.Enums;

namespace TrainingCenterManagement.Models
{
    public class Course
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public int Capacity { get; private set; }
        public int InstructorId { get; private set; }
        public CourseLevel Level { get; private set; }

        public Course(
            int id,
            string title,
            decimal price,
            int capacity,
            int instructorId,
            CourseLevel level)
        {
            Id = id;
            Title = title;
            Price = price;
            Capacity = capacity;
            InstructorId = instructorId;
            Level = level;
        }
    }
}
