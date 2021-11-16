using CarDealership.Data.Factories;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using CarDealership.UI.Models;
using CarDealership.UI.Utitlites;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarDealership.UI.Controllers
{
    public class AdminController : Controller
    {
        //GET Makes
        [Authorize(Roles = "admin")]
        public ActionResult Makes()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.UserID = AuthorizeUtilities.GetUserId(this);
            }
            var repo = MakeRepoFactory.GetRepository();
            var model = repo.GetMakeList();

            return View(model);
        }
        //GET Models
        [Authorize(Roles = "admin")]
        public ActionResult Models()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.UserID = AuthorizeUtilities.GetUserId(this);
            }
            var repo = ModelRepoFactory.GetRepository();
            var makeRepo = MakeRepoFactory.GetRepository();
            var model = new ModelListVM();
            model.ModelList = repo.GetModelList().ToList();
            model.SetMakeItems(makeRepo.GetAll());
            return View(model);
        }
        //GET specials
        [Authorize(Roles = "admin")]
        public ActionResult Specials()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.UserID = AuthorizeUtilities.GetUserId(this);
            }
            var repo = SpecialRepoFactory.GetRepository();
            var model = new AddSpecialsVM();
            model.SpecialList = repo.GetAll().ToList();

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Specials(AddSpecialsVM model)
        {
            if (Request.IsAuthenticated)
            {
                model.Special.UserID = AuthorizeUtilities.GetUserId(this);
            }
            if (ModelState.IsValid)
            {
                var repo = SpecialRepoFactory.GetRepository();

                try
                {
                    repo.Insert(model.Special);
                    return RedirectToAction("Specials");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteSpecial(int specialID)
        {
            var repo = SpecialRepoFactory.GetRepository();
            var model = repo.GetByID(specialID);
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult DeleteSpecial(Special special)
        {
            var repo = SpecialRepoFactory.GetRepository();
            repo.Delete(special.SpecialID);
            return RedirectToAction("Specials");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Vehicles()
        {
            SearchVM model = new SearchVM();
            model.SearchYears = VehicleRepoFactory.GetRepository().CreateSearchYears().ToList();
            model.PriceRange = VehicleRepoFactory.GetRepository().CreatePriceRange().ToList();
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddVehicle()
        {
            var model = new AddVehicleVM();

            var makeRepo = MakeRepoFactory.GetRepository();
            var bodyStyleRepo = BodyStyleRepoFactory.GetRepository();
            var transRepo = TransmissionRepoFactory.GetRepository();
            var bodyColorRepo = BodyColorRepoFactory.GetRepository();
            var interiorColorRepo = InteriorColorRepoFactory.GetRepository();
            var vehicleRepo = VehicleRepoFactory.GetRepository();

            model.Make = new SelectList(makeRepo.GetAll(), "MakeID", "MakeName");
            model.BodyStyle = new SelectList(bodyStyleRepo.GetAll(), "BodyStyleID", "BodyDescription");
            model.Transmission = new SelectList(transRepo.GetAll(), "TransmissionID", "TransStyle");
            model.BodyColor = new SelectList(bodyColorRepo.GetAll(), "BodyColorID", "BodyColorName");
            model.InteriorColor = new SelectList(interiorColorRepo.GetAll(), "InteriorColorID", "InteriorColorName");

            var yearList = vehicleRepo.CreateSearchYears();

            model.Years = yearList.ToList().ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.ToString(),
                    Value = a.ToString(),
                    Selected = false
                };
            });

            model.Vehicle = new Vehicle();

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddVehicle(AddVehicleVM model)
        {
            if (ModelState.IsValid)
            {
                var repo = VehicleRepoFactory.GetRepository();

                try
                {
                    model.Vehicle.IsAvailable = true;
                    

                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string extension = Path.GetExtension(model.ImageUpload.FileName);
                        string fileName = "inventory-" + model.Vehicle.VehicleID.ToString();
                        var filePath = Path.Combine(savepath, fileName + extension);

                        model.ImageUpload.SaveAs(filePath);
                        model.Vehicle.ImageFileName = Path.GetFileName(filePath);

                    }
                    model.Vehicle.IsFeatured = false;
                    repo.Insert(model.Vehicle);

                    repo.Update(model.Vehicle);

                    return RedirectToAction("EditVehicle", new { id = model.Vehicle.VehicleID });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                AddVehicleVM redoModel = new AddVehicleVM(); 

                var makeRepo = MakeRepoFactory.GetRepository();
                var bodyStyleRepo = BodyStyleRepoFactory.GetRepository();
                var transRepo = TransmissionRepoFactory.GetRepository();
                var bodyColorRepo = BodyColorRepoFactory.GetRepository();
                var interiorColorRepo = InteriorColorRepoFactory.GetRepository();
                var vehicleRepo = VehicleRepoFactory.GetRepository();

                redoModel.Make = new SelectList(makeRepo.GetAll(), "MakeID", "MakeName");
                redoModel.BodyStyle = new SelectList(bodyStyleRepo.GetAll(), "BodyStyleID", "BodyDescription");
                redoModel.Transmission = new SelectList(transRepo.GetAll(), "TransmissionID", "TransStyle");
                redoModel.BodyColor = new SelectList(bodyColorRepo.GetAll(), "BodyColorID", "BodyColorName");
                redoModel.InteriorColor = new SelectList(interiorColorRepo.GetAll(), "InteriorColorID", "InteriorColorName");

                var yearList = vehicleRepo.CreateSearchYears();

                redoModel.Years = yearList.ToList().ConvertAll(a =>
                {
                    return new SelectListItem()
                    {
                        Text = a.ToString(),
                        Value = a.ToString(),
                        Selected = false
                    };
                });

                return View(redoModel);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditVehicle(int id)
        {
            var model = new EditVehicleVM();

            var makeRepo = MakeRepoFactory.GetRepository();
            var modelRepo = ModelRepoFactory.GetRepository();
            var bodyStyleRepo = BodyStyleRepoFactory.GetRepository();
            var transRepo = TransmissionRepoFactory.GetRepository();
            var bodyColorRepo = BodyColorRepoFactory.GetRepository();
            var interiorColorRepo = InteriorColorRepoFactory.GetRepository();
            var vehicleRepo = VehicleRepoFactory.GetRepository();

            model.Vehicle = vehicleRepo.GetByID(id);

            model.Make = new SelectList(makeRepo.GetAll(), "MakeID", "MakeName");
            model.Model = new SelectList(modelRepo.GetAll(), "ModelID", "ModelName");
            model.BodyStyle = new SelectList(bodyStyleRepo.GetAll(), "BodyStyleID", "BodyDescription");
            model.Transmission = new SelectList(transRepo.GetAll(), "TransmissionID", "TransStyle");
            model.BodyColor = new SelectList(bodyColorRepo.GetAll(), "BodyColorID", "BodyColorName");
            model.InteriorColor = new SelectList(interiorColorRepo.GetAll(), "InteriorColorID", "InteriorColorName");

            var yearList = vehicleRepo.CreateSearchYears();

            model.Years = yearList.ToList().ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.ToString(),
                    Value = a.ToString(),
                    Selected = false
                };
            });

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditVehicle(EditVehicleVM model)
        {
            if (ModelState.IsValid)
            {
                var repo = VehicleRepoFactory.GetRepository();

                try
                {
                    var oldVehicle = repo.GetByID(model.Vehicle.VehicleID);

                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string extension = Path.GetExtension(model.ImageUpload.FileName);
                        string fileName = "inventory-" + model.Vehicle.VehicleID.ToString();
                        var filePath = Path.Combine(savepath, fileName + extension);

                        model.ImageUpload.SaveAs(filePath);
                        model.Vehicle.ImageFileName = Path.GetFileName(filePath);

                        var oldPath = Path.Combine(savepath, oldVehicle.ImageFileName);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    else
                    {
                        model.Vehicle.ImageFileName = oldVehicle.ImageFileName;
                    }

                    model.Vehicle.IsAvailable = oldVehicle.IsAvailable;
                    repo.Update(model.Vehicle);

                    return RedirectToAction("EditVehicle", new { id = model.Vehicle.VehicleID });

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var makeRepo = MakeRepoFactory.GetRepository();
                var modelRepo = ModelRepoFactory.GetRepository();
                var bodyStyleRepo = BodyStyleRepoFactory.GetRepository();
                var transRepo = TransmissionRepoFactory.GetRepository();
                var bodyColorRepo = BodyColorRepoFactory.GetRepository();
                var interiorColorRepo = InteriorColorRepoFactory.GetRepository();
                var vehicleRepo = VehicleRepoFactory.GetRepository();

                model.Make = new SelectList(makeRepo.GetAll(), "MakeID", "MakeName");
                model.Model = new SelectList(modelRepo.GetAll(), "ModelID", "ModelName");
                model.BodyStyle = new SelectList(bodyStyleRepo.GetAll(), "BodyStyleID", "BodyDescription");
                model.Transmission = new SelectList(transRepo.GetAll(), "TransmissionID", "TransStyle");
                model.BodyColor = new SelectList(bodyColorRepo.GetAll(), "BodyColorID", "BodyColorName");
                model.InteriorColor = new SelectList(interiorColorRepo.GetAll(), "InteriorColorID", "InteriorColorName");

                var yearList = vehicleRepo.CreateSearchYears();

                model.Years = yearList.ToList().ConvertAll(a =>
                {
                    return new SelectListItem()
                    {
                        Text = a.ToString(),
                        Value = a.ToString(),
                        Selected = false
                    };
                });
                return View(model);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult DeleteVehicle(int vehicleID)
        {
            var userId = AuthorizeUtilities.GetUserId(this);

            var repo = VehicleRepoFactory.GetRepository();
            var vehicleToDelete = repo.GetByID(vehicleID);

            var savepath = Server.MapPath("~/Images");
            var oldPath = Path.Combine(savepath, vehicleToDelete.ImageFileName);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            repo.Delete(vehicleID);

            return RedirectToAction("Vehicles");
        }

        [Authorize(Roles = "admin , sales")]
        [HttpGet]

        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordViewModel();

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Users()
        {
            var model = new UserListVM();
            var context = new ApplicationDbContext();
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var allUsers = context.Users.Include(u => u.Roles).ToList();
            var userVM = allUsers.Select(user => new UserListVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Email = user.Email,
                Roles = string.Join(", ", roleMgr.Roles.Where(r => r.Users.Any(rr => rr.UserId == user.Id)).Select(r => r.Name))
            });

            return View(userVM);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddUser()
        {
            var model = new AddUserVM();
            var context = new ApplicationDbContext();
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var list = roleMgr.Roles.ToList();

            model.RoleList = list.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id
            });

            return View(model);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> AddUser(AddUserVM model)
        {
            if (ModelState.IsValid)
            {
                var context = new ApplicationDbContext();
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                try
                {
                    var user = new ApplicationUser()
                    {
                        FirstName = model.User.FirstName,
                        LastName = model.User.LastName,
                        Email = model.User.Email,
                        UserName = model.User.Email
                    };

                    var result = await userMgr.CreateAsync(user, model.Password);
                    var role = roleMgr.FindById(model.SelectedRoleID.ToString());

                    if (result.Succeeded)
                    {
                        result = userMgr.AddToRole(user.Id, role.Name);
                    }
                    return RedirectToAction("Users", new { id = user.Id });
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var context = new ApplicationDbContext();

                var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var list = roleMgr.Roles.ToList();

                model.RoleList = list.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id
                });

                return View(model);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditUser(string Id)
        {
            var model = new EditUserVM();
            var context = new ApplicationDbContext();
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var list = roleMgr.Roles.ToList();

            model.User = userMgr.FindById(Id);
            var oldRoleName = userMgr.GetRoles(Id).ToString();
            model.SelectedRoleID = oldRoleName;

            model.RoleList = list.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = model.User.Roles.Any(s => s.UserId == model.User.Id)
            }).ToList();

            var role = roleMgr.FindById(model.User.Roles.FirstOrDefault(s => s.UserId == model.User.Id).RoleId);
            model.SelectedRoleID = role.Id;
            return View(model);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> EditUser(EditUserVM model)
        {
            if (ModelState.IsValid)
            {
                var context = new ApplicationDbContext();
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                try
                {
                    var oldUser = userMgr.FindById(model.User.Id);

                    oldUser.FirstName = model.User.FirstName;
                    oldUser.LastName = model.User.LastName;
                    oldUser.Email = model.User.Email;
                    oldUser.UserName = model.User.Email;

                    var result = await userMgr.UpdateAsync(oldUser);
                    var role = roleMgr.FindById(model.SelectedRoleID.ToString());
                    var oldRole = roleMgr.FindById(oldUser.Roles.FirstOrDefault(s => s.UserId == oldUser.Id).RoleId);
                    if (result.Succeeded)
                    {
                        result = userMgr.RemoveFromRole(oldUser.Id, oldRole.Name);
                        result = userMgr.AddToRole(oldUser.Id, role.Name);

                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            result = userMgr.RemovePassword(oldUser.Id);
                            result = userMgr.AddPassword(oldUser.Id, model.Password);
                        }
                    }
                    return RedirectToAction("Users", new { id = oldUser.Id });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var context = new ApplicationDbContext();

                var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var list = roleMgr.Roles.ToList();

                model.RoleList = list.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id
                });

                return View(model);
            }
        }
    }
}