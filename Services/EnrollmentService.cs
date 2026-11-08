using System;
using System.Collections.Generic;
using System.Text;
using TrainingCenterManagement.Data;
using TrainingCenterManagement.Enums;
using TrainingCenterManagement.Models;

namespace TrainingCenterManagement.Services
{
    public class EnrollmentService
    {
        private readonly TrainingCenterData _data;

        public EnrollmentService(TrainingCenterData data)
        {
            _data = data;
        }
        public EnrollmentResult EnrollStudent(int studentId, int courseId)
        {
            // 1. Check student
            if (!_data.Students.ContainsKey(studentId))
                return EnrollmentResult.StudentNotFound;

            // 2. Check course
            if (!_data.Courses.TryGetValue(courseId, out Course? course))
                return EnrollmentResult.CourseNotFound;

            // 3. Prevent duplicate enrollment
            foreach (Enrollment enrollment in _data.Enrollments)
            {
                if (enrollment.StudentId == studentId &&
                    enrollment.CourseId == courseId)
                {
                    return EnrollmentResult.AlreadyEnrolled;
                }
            }

            // 4. Create enrollment
            if (course.AvailableSeats > 0)
            {
                Enrollment enrollment = new Enrollment(
                    _data.Enrollments.Count + 1,
                    studentId,
                    courseId,
                    EnrollmentStatus.Active
                );

                _data.Enrollments.Add(enrollment);
                course.IncreaseEnrollmentCount();

                return EnrollmentResult.Enrolled;
            }

            // 5. Add to waiting list
            Enrollment waitingEnrollment = new Enrollment(
                _data.Enrollments.Count + 1,
                studentId,
                courseId,
                EnrollmentStatus.Waiting
            );

            _data.Enrollments.Add(waitingEnrollment);

            if (!_data.WaitingQueues.ContainsKey(courseId))
            {
                _data.WaitingQueues.Add(courseId, new Queue<int>());
            }

            _data.WaitingQueues[courseId].Enqueue(studentId);

            return EnrollmentResult.AddedToWaitingList;
        }
        public bool CancelEnrollment(int enrollmentId)
        {
            Enrollment? targetEnrollment = null;

            foreach (Enrollment enrollment in _data.Enrollments)
            {
                if (enrollment.Id == enrollmentId)
                {
                    targetEnrollment = enrollment;
                    break;
                }
            }

            if (targetEnrollment == null)
                return false;

            if (targetEnrollment.Status != EnrollmentStatus.Active &&
                targetEnrollment.Status != EnrollmentStatus.Waiting)
            {
                return false;
            }

            bool wasActive =
                targetEnrollment.Status == EnrollmentStatus.Active;

            targetEnrollment.UpdateStatus(EnrollmentStatus.Cancelled);

            if (wasActive)
            {
                Course course = _data.Courses[targetEnrollment.CourseId];

                course.DecreaseEnrollmentCount();

                PromoteNextStudent(targetEnrollment.CourseId);
            }

            return true;
        }
        private void PromoteNextStudent(int courseId)
        {
            if (!_data.WaitingQueues.TryGetValue(
                courseId, out Queue<int>? queue))
            {
                return;
            }

            while (queue.Count > 0)
            {
                int studentId = queue.Dequeue();

                Enrollment? waitingEnrollment = null;

                foreach (Enrollment enrollment in _data.Enrollments)
                {
                    if (enrollment.StudentId == studentId &&
                        enrollment.CourseId == courseId &&
                        enrollment.Status == EnrollmentStatus.Waiting)
                    {
                        waitingEnrollment = enrollment;
                        break;
                    }
                }

                // Skip cancelled waiting enrollments
                if (waitingEnrollment == null)
                    continue;

                waitingEnrollment.UpdateStatus(EnrollmentStatus.Active);

                _data.Courses[courseId].IncreaseEnrollmentCount();

                return;
            }
        }
        public List<Enrollment> GetEnrollmentsByStudent(int studentId)
        {
            List<Enrollment> result = new List<Enrollment>();

            foreach (Enrollment enrollment in _data.Enrollments)
            {
                if (enrollment.StudentId == studentId)
                {
                    result.Add(enrollment);
                }
            }

            return result;
        }

        public List<Enrollment> GetEnrollmentsByCourse(int courseId)
        {
            List<Enrollment> result = new List<Enrollment>();

            foreach (Enrollment enrollment in _data.Enrollments)
            {
                if (enrollment.CourseId == courseId)
                {
                    result.Add(enrollment);
                }
            }

            return result;
        }
    }
}
