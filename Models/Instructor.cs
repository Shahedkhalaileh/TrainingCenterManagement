using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingCenterManagement.Models
{
    public class Instructor : Person
    {
        public string Specialization { get; private set; }

        public Instructor(int id ,string name , string email , string specialization)
            :base(id , name , email ){
            Specialization = specialization;
        }
    }
}
