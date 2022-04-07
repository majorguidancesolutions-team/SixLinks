﻿using Microsoft.AspNetCore.Mvc;
using SixLinksWeb.Data;
using SixLinksWeb.Models;
using System.Diagnostics;

namespace SixLinksWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUserRolesService _userRolesService;

		public HomeController(ILogger<HomeController> logger, IUserRolesService userRolesService)
		{
			_logger = logger;
			_userRolesService = userRolesService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public async Task<IActionResult> EnsureRolesAndUsers()
		{
			await _userRolesService.EnsureAdminUserRole();
			return RedirectToAction("Index");
		}
	}
}