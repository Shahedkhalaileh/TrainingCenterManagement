using System;
using System.Collections.Generic;
using System.Text;

using TrainingCenterManagement.Enums;

namespace TrainingCenterManagement.Models;

public class Enrollment
{
    public int Id { get; private set; }
    public int StudentId { get; private set; }
    public int CourseId { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    public decimal? Grade { get; private set; }
    public EnrollmentStatus Status { get; private set; }

    public Enrollment(
        int id,
        int studentId,
        int courseId,
        EnrollmentStatus status)
    {
        Id = id;
        StudentId = studentId;
        CourseId = courseId;
        EnrollmentDate = DateTime.Now;
        Status = status;
    }
    public void UpdateStatus(EnrollmentStatus status)
    {
        Status = status;
    }
}