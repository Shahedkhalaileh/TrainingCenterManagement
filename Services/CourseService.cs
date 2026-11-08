using System;
using System.Collections.Generic;
using System.Text;
using TrainingCenterManagement.Data;
using TrainingCenterManagement.Models;

namespace TrainingCenterManagement.Services
{
    public class CourseService
    {
        private readonly TrainingCenterData _data;

        public CourseService(TrainingCenterData data)
        {
            _data = data;
        }

        public bool AddCourse(Course course)
        {
            if (_data.Courses.ContainsKey(course.Id))
                return false;

            if (course.Price < 0)
                return false;

            if (course.Capacity <= 0)
                return false;

            if (!_data.Instructors.ContainsKey(course.InstructorId))
                return false;

            _data.Courses.Add(course.Id, course);

            return true;
        }
        public Course? GetCourseById(int id)
        {
            if (_data.Courses.TryGetValue(id, out Course? course))
                return course;

            return null;
        }

        public List<Course> GetAllCourses()
        {
            return _data.Courses.Values.ToList();
        }
        public bool DeleteCourse(int id)
        {
            if (!_data.Courses.ContainsKey(id))
                return false;

            bool hasEnrollments = _data.Enrollments
                .Any(e => e.CourseId == id);

            if (hasEnrollments)
                return false;

            _data.Courses.Remove(id);
            _data.WaitingQueues.Remove(id);

            return true;
        }
    }
}
