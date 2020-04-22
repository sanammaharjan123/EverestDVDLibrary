﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using coursework02.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace coursework02.Controllers
{
	[Authorize(Roles ="Manager")]
	public class RoleController : Controller
    {
		ApplicationDbContext context;

		public RoleController()
		{
			context = new ApplicationDbContext();
		}

		/// <summary>
		/// Get All Roles
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
            if (User.IsInRole("Manager"))
            {
                var Roles = context.Roles.ToList();
                return View(Roles);
            }
            else
            {
               	return RedirectToAction("Index", "Home");

            }

            //if (User.Identity.IsAuthenticated)
            //{


            //	if ()
            //	{
            //		return RedirectToAction("Index", "Home");
            //	}
            //}
            //else
            //{
            //	return RedirectToAction("Index", "Home");
            //}



        }
		public Boolean isAdminUser()
		{
			if (User.Identity.IsAuthenticated)
			{
				var user = User.Identity;
				var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
				var s = UserManager.GetRoles(user.GetUserId());
				if (s[0].ToString() == "Manager")
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}
		/// <summary>
		/// Create  a New role
		/// </summary>
		/// <returns></returns>
		public ActionResult Create()
		{
			if (User.Identity.IsAuthenticated)
			{


				if (!isAdminUser())
				{
					return RedirectToAction("Index", "Home");
				}
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}

			var Role = new IdentityRole();
			return View(Role);
		}

		/// <summary>
		/// Create a New Role
		/// </summary>
		/// <param name="Role"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Create(IdentityRole Role)
		{
			if (User.Identity.IsAuthenticated)
			{
				if (!isAdminUser())
				{
					return RedirectToAction("Index", "Home");
				}
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}

			context.Roles.Add(Role);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}