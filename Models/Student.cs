using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingCenterManagement.Models
{
    public class Student : Person
    {
        public Student(int id, string name, string email)
            : base(id, name, email)
        {
        }
    }
}
