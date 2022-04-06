#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using MyDataModels;
using SixLinksDataService;
using System.Diagnostics;
using MyDataManagerDataOperations;

namespace SixLinksWeb.Controllers
{
	public class ActorsController : Controller
	{
		private readonly IDataOperations _dataOps;

		public ActorsController(IDataOperations dataOps)
		{
			_dataOps = dataOps;
		}

		// GET: Actors
		public async Task<IActionResult> Index()
		{
			var actors = await _dataOps.GetActors();
			return View(actors);
		}

		// GET: Actors/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var actor = await _dataOps.GetActorById((int)id);
			if (actor == null)
			{
				return NotFound();
			}

			return View(actor);
		}

		// GET: Actors/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Actors/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,BaconRating")] Actor actor)
		{
			if (ModelState.IsValid)
			{
				await _dataOps.AddNewActor(actor);
				return RedirectToAction(nameof(Index));
			}
			return View(actor);
		}

		// GET: Actors/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var actor = await _dataOps.GetActorById((int)id);
			if (actor == null)
			{
				return NotFound();
			}
			return View(actor);
		}

		// POST: Actors/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,BaconRating")] Actor actor)
		{
			if (id != actor.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _dataOps.UpdateActor(actor.Id, actor.FirstName, actor.LastName);
				}
				catch (DbUpdateConcurrencyException)
				{
					bool isExist = await ActorExists(actor.Id);
					if (!isExist)
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(actor);
		}

		// GET: Actors/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var actor = await _dataOps.GetActorById((int)id);
			if (actor == null)
			{
				return NotFound();
			}

			return View(actor);
		}

		// POST: Actors/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var actor = await _dataOps.GetActorById(id);
			await _dataOps.DeleteActor(actor);
			return RedirectToAction(nameof(Index));
		}
		private async Task<bool> ActorExists(int id)
		{
			return await _dataOps.CheckExistingActor(id);
		}
	}
}
