using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingCenterManagement.Models
{
    public class Person
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        protected Person(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
