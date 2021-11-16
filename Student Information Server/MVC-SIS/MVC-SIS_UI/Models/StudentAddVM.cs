using MVC_SIS_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SIS_UI.Models
{
    public class StudentAddVM : IValidatableObject
    {
        public Student Student { get; set; }
        public List<SelectListItem> CourseItems { get; set; }
        public List<SelectListItem> MajorItems { get; set; }
        public List<SelectListItem> StateItems { get; set; }
        public List<int> SelectedCourseIds { get; set; }

        public StudentAddVM()
        {
            CourseItems = new List<SelectListItem>();
            MajorItems = new List<SelectListItem>();
            StateItems = new List<SelectListItem>();
            SelectedCourseIds = new List<int>();
            Student = new Student();
        }

        public void SetCourseItems(IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                CourseItems.Add(new SelectListItem()
                {
                    Value = course.CourseId.ToString(),
                    Text = course.CourseName
                });
            }
        }

        public void SetMajorItems(IEnumerable<Major> majors)
        {
            foreach (var major in majors)
            {
                MajorItems.Add(new SelectListItem()
                {
                    Value = major.MajorId.ToString(),
                    Text = major.MajorName
                });
            }
        }

        public void SetStateItems(IEnumerable<State> states)
        {
            foreach (var state in states)
            {
                StateItems.Add(new SelectListItem()
                {
                    Value = state.StateAbbreviation,
                    Text = state.StateName
                });
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Student == null)
            {
                errors.Add(new ValidationResult("Please enter Student Details", new[] { "Student" }));
            }

            if (Student.FirstName == null || Student.FirstName == "")
            {
                errors.Add(new ValidationResult("Please enter a First Name", new[] { "Student.FirstName" }));
            }

            if (Student.LastName == null || Student.LastName == "")
            {
                errors.Add(new ValidationResult("Please enter a Last Name", new[] { "Student.LastName" }));
            }

            if (SelectedCourseIds == null)
            {
                errors.Add(new ValidationResult("Please select a Course", new[] { "Student.Courses" }));
            }

            if (Student.Major == null)
            {
                errors.Add(new ValidationResult("Please select a Major", new[] { "Student.Major" }));
            }

           //seems to already validate whether gpa is a number or not though i can't find where
           //would attempt a tryparse on the value otherwise



            return errors;
        }
    }
}