using System;
using System.Collections.Generic;
using System.Text;
using TrainingCenterManagement.Data;
using TrainingCenterManagement.Models;

namespace TrainingCenterManagement.Services
{
    internal class InstructorService
    {
        private readonly TrainingCenterData _data;

        public InstructorService(TrainingCenterData data)
        {
            _data = data;
        }

        public bool AddInstructor(Instructor instructor)
        {
            if (_data.Instructors.ContainsKey(instructor.Id))
                return false;

            if (_data.InstructorEmails.Contains(instructor.Email))
                return false;

            _data.Instructors.Add(instructor.Id, instructor);
            _data.InstructorEmails.Add(instructor.Email);

            return true;
        }

        public Instructor? GetInstructorById(int id)
        {
            if (_data.Instructors.TryGetValue(id, out Instructor? instructor))
                return instructor;

            return null;
        }

        public List<Instructor> GetAllInstructors()
        {
            return _data.Instructors.Values.ToList();
        }

        public bool DeleteInstructor(int id)
        {
            if (!_data.Instructors.TryGetValue(id, out Instructor? instructor))
                return false;

            bool hasCourses = _data.Courses.Values
                .Any(c => c.InstructorId == id);

            if (hasCourses)
                return false;

            _data.Instructors.Remove(id);
            _data.InstructorEmails.Remove(instructor.Email);

            return true;
        }
    }
}
