using MVC_SIS_Data;
using MVC_SIS_Models;
using MVC_SIS_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SIS_UI.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentAddVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentAddVM studentVM)
        {
            if (!ModelState.IsValid)
            {
                //had to add the two lines below or the drop down menus would not return
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());
                return View(studentVM);
            }

            studentVM.Student.Courses = new List<Course>();

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            StudentRepository.Add(studentVM.Student);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditStudentVM viewModel = new EditStudentVM();
            viewModel.Student = StudentRepository.Get(id);         
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            viewModel.SetStateItems(StateRepository.GetAll());

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditStudentVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Student = StudentRepository.Get(viewModel.Student.StudentId);
                viewModel.SetCourseItems(CourseRepository.GetAll());
                viewModel.SetMajorItems(MajorRepository.GetAll());
                viewModel.SetStateItems(StateRepository.GetAll());
                return View(viewModel);
            }

            viewModel.Student.Courses = new List<Course>();

            foreach (var id in viewModel.SelectedCourseIds)
                viewModel.Student.Courses.Add(CourseRepository.Get(id));

            viewModel.Student.Major = MajorRepository.Get(viewModel.Student.Major.MajorId);

            StudentRepository.Edit(viewModel.Student);
            StudentRepository.SaveAddress(viewModel.Student.StudentId, viewModel.Student.Address);
            return RedirectToAction("List");

            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            StudentDeleteVM viewModel = new StudentDeleteVM();
            viewModel.currentStudent = StudentRepository.Get(id);
            return View(viewModel);

            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Delete(StudentDeleteVM viewModel)
        {
            StudentRepository.Delete(viewModel.currentStudent.StudentId);
            return RedirectToAction("List");

            throw new NotImplementedException();
        }
    }
}