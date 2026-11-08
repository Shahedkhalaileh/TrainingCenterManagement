using System;
using System.Collections.Generic;
using System.Text;
using TrainingCenterManagement.Data;
using TrainingCenterManagement.Models;

namespace TrainingCenterManagement.Services
{
    public class StudentService
    {
        private readonly TrainingCenterData _data;

        public StudentService(TrainingCenterData data)
        {
            _data = data;
        }

        public bool AddStudent(Student student)
        {
            if (_data.Students.ContainsKey(student.Id)) 
                return false;
            if (_data.StudentEmails.Contains(student.Email))
                return false;

            _data.Students.Add(student.Id, student);
            _data.StudentEmails.Add(student.Email);

            return true;
        }

        public Student? GetStudentById(int id)
        {
            if(_data.Students.TryGetValue(id, out Student? student)) return student;

            return null;
        }
        public List<Student> GetAllStudents()
        {
            return _data.Students.Values.ToList();
        }

        public bool DeleteStudent(int id)
        {
            if (!_data.Students.TryGetValue(id, out Student? student))
                return false;

            bool hasEnrollments = _data.Enrollments
                .Any(e => e.StudentId == id);

            if (hasEnrollments)
                return false;

            _data.Students.Remove(id);
            _data.StudentEmails.Remove(student.Email);

            return true;
        }
    }
}
