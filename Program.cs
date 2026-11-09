using TrainingCenterManagement.Data;
using TrainingCenterManagement.Enums;
using TrainingCenterManagement.Interfaces;
using TrainingCenterManagement.Models;
using TrainingCenterManagement.PaymentMethods;
using TrainingCenterManagement.Services;

TrainingCenterData data = new TrainingCenterData();

StudentService studentService = new StudentService(data);
InstructorService instructorService = new InstructorService(data);
CourseService courseService = new CourseService(data);
EnrollmentService enrollmentService = new EnrollmentService(data);

// Create instructor
Instructor instructor = new Instructor(
    1,
    "Ahmad",
    "ahmad@gmail.com",
    "Backend Development"
);

instructorService.AddInstructor(instructor);

// Create students
Student student1 = new Student(1, "Sara", "sara@gmail.com");
Student student2 = new Student(2, "Omar", "omar@gmail.com");
Student student3 = new Student(3, "Lina", "lina@gmail.com");

studentService.AddStudent(student1);
studentService.AddStudent(student2);
studentService.AddStudent(student3);

// Create course with capacity 1
Course course = new Course(
    1,
    "C# Fundamentals",
    100,
    1,
    1,
    CourseLevel.Beginner
);

courseService.AddCourse(course);

// Enrollment tests
Console.WriteLine(enrollmentService.EnrollStudent(1, 1));
Console.WriteLine(enrollmentService.EnrollStudent(2, 1));
Console.WriteLine(enrollmentService.EnrollStudent(3, 1));

// Cancel first enrollment
Console.WriteLine(enrollmentService.CancelEnrollment(1));

// Check course
Console.WriteLine($"Enrolled: {course.EnrolledStudentsCount}");
Console.WriteLine($"Available: {course.AvailableSeats}");

// Print enrollments
foreach (Enrollment enrollment in enrollmentService.GetEnrollmentsByCourse(1))
{
    Console.WriteLine(
        $"Student: {enrollment.StudentId}, Status: {enrollment.Status}"
    );
}
PaymentService paymentService = new PaymentService(data);

// First attempt: failed card payment
IPaymentMethod failedCard = new CardPaymentMethod(false);

PaymentResult firstResult = paymentService.ProcessPayment(
    1,
    100,
    failedCard
);

Console.WriteLine($"First attempt: {firstResult}");

// Second attempt: successful card payment
IPaymentMethod successfulCard = new CardPaymentMethod(true);

PaymentResult secondResult = paymentService.ProcessPayment(
    1,
    100,
    successfulCard
);