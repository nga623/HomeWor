using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nolan.HK.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _UserService;
        public UserController
            (
            IUserService userService
            )
        {
            _UserService = userService;
        }
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Registered()
        {
            return View();
        }
        // POST: UserController/Create
        [HttpPost]
        public ActionResult Create(UserDto userDto)
        {
            try
            {
                var r = _UserService.CreateAsync(userDto).Result;
                if (r > 0)
                    return RedirectToAction(nameof(Index));
                else {
                     return Content(@"created!");
                }
            }
            catch
            {
                return View();
            }
        }
        
        
        public ActionResult Login(UserDto userDto)
        {
            try
            {
                var r = _UserService.LoginAsync(userDto).Result;
                if (r)
                {
                    return RedirectToAction("Index", "TimeCard");
                }
                else
                {
                    return Content(@"login faild!");
                }
               
            }
            catch
            {
                return View();
            }
        }
        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
