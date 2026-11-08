using TrainingCenterManagement.Data;
using TrainingCenterManagement.Enums;
using TrainingCenterManagement.Models;
using TrainingCenterManagement.Services;

TrainingCenterData data = new TrainingCenterData();

StudentService studentService = new StudentService(data);

InstructorService instructorService =
    new InstructorService(data);

CourseService courseService = new CourseService(data);

// Add instructor
Instructor instructor = new Instructor(
    1,
    "Ahmad",
    "ahmad@gmail.com",
    "Backend Development"
);

Console.WriteLine(instructorService.AddInstructor(instructor));

// Add student
Student student = new Student(
    1,
    "Sara",
    "sara@gmail.com"
);

Console.WriteLine(studentService.AddStudent(student));

// Add course
Course course = new Course(
    1,
    "C# Fundamentals",
    100,
    20,
    1,
    CourseLevel.Beginner
);

Console.WriteLine(courseService.AddCourse(course));

// Get all students
foreach (Student s in studentService.GetAllStudents())
{
    Console.WriteLine($"{s.Id} - {s.Name} - {s.Email}");
}