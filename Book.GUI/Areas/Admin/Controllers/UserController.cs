using BookShop.DAL.Data;
using BookShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookShop.GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticRole.RoleAdmin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }


        #region Call API
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _db.ApplicationUsers.ToList();
            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in users)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id)
                                      .RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }

            return Json(new { data = users });
        }

        [HttpPost]
        public IActionResult LockOrUnlock([FromBody] string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return Json(new { success = false, message = "Error while operating" });
            }

            if(user.LockoutEnd!=null && user.LockoutEnd > DateTime.Now)
            {
                // current is lock
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(222);
            }
            _db.SaveChanges();

            return Json(new { success = true, message = "Successfully" });
        }
        #endregion
    }
}
