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
    public class AdminController : Controller
    {
        //ask about the home controller and how it is not completed. the instructions said nothing about completing the missing views

        [HttpGet]
        public ActionResult Majors()
        {
            var model = MajorRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddMajor()
        {
            return View(new AddEditMajorVM());
        }

        [HttpPost]
        public ActionResult AddMajor(AddEditMajorVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            MajorRepository.Add(viewModel.currentMajor.MajorName);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult EditMajor(int id)
        {
            AddEditMajorVM viewmodel = new AddEditMajorVM();
            viewmodel.currentMajor = MajorRepository.Get(id);
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult EditMajor(AddEditMajorVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            MajorRepository.Edit(viewModel.currentMajor);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult DeleteMajor(int id)
        {
            DeleteMajorVM viewModel = new DeleteMajorVM();
            viewModel.currentMajor = MajorRepository.Get(id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteMajor(DeleteMajorVM viewModel)
        {
            MajorRepository.Delete(viewModel.currentMajor.MajorId);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult States()
        {
            var model = StateRepository.GetAll();
            return View(model.ToList()) ;
        }

        [HttpGet]
        public ActionResult AddState()
        {
            return View(new AddStateVM());
        }

        [HttpPost]
        public ActionResult AddState(AddStateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            StateRepository.Add(viewModel.currentState);
            return RedirectToAction("States");
        }

        //exercise never explicitly says to create edit state method just to link to it
        [HttpGet]
        public ActionResult EditState(string stateAbbreviation)
        {
            EditStateVM viewModel = new EditStateVM();
            viewModel.currentState = StateRepository.Get(stateAbbreviation);
            viewModel.stateAbbreviationSearchId = stateAbbreviation;
            return View(viewModel);
        }

        //you guys did not make this one simple. following the example above causes it to save the original state abbreviation rather than the new one that is typed in
        [HttpPost]
        public ActionResult EditState(EditStateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            EditState newState = new EditState();
            newState.StateAbbreviation = viewModel.currentState.StateAbbreviation;
            newState.StateName = viewModel.currentState.StateName;
            newState.searchStateAbbreviation = viewModel.stateAbbreviationSearchId;

            StateRepository.Edit(newState);
            return RedirectToAction("States");
        }

        [HttpGet]
        public ActionResult DeleteState(string stateAbbreviation)
        {
            DeleteStateVM viewModel = new DeleteStateVM();
            viewModel.currentState = StateRepository.Get(stateAbbreviation);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteState(DeleteStateVM viewModel)
        {
            StateRepository.Delete(viewModel.currentState.StateAbbreviation);
            return RedirectToAction("States");
        }

        [HttpGet]
        public ActionResult Courses()
        {
            var model = CourseRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            return View(new AddEditCourseVM());
        }

        [HttpPost]
        public ActionResult AddCourse(AddEditCourseVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            CourseRepository.Add(viewModel.currentCourse.CourseName);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            AddEditCourseVM viewmodel = new AddEditCourseVM();
            viewmodel.currentCourse = CourseRepository.Get(id);
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult EditCourse(AddEditCourseVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            CourseRepository.Edit(viewModel.currentCourse);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            DeleteCourseVM viewModel = new DeleteCourseVM();
            viewModel.currentCourse = CourseRepository.Get(id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteCourse(DeleteCourseVM viewModel)
        {
            CourseRepository.Delete(viewModel.currentCourse.CourseId);
            return RedirectToAction("Courses");
        }
    }
}