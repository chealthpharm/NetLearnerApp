﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetLearner.SharedLib.Data;
using NetLearner.SharedLib.Models;
using NetLearner.SharedLib.Services;

namespace NetLearner.Mvc.Controllers
{
    public class LearningResourcesController : Controller
    {
        private readonly ILearningResourceService _learningResourceService;

        public LearningResourcesController(ILearningResourceService learningResourceService)
        {
            _learningResourceService = learningResourceService;
        }

        // GET: LearningResources
        public async Task<IActionResult> Index()
        {
            return View(await _learningResourceService.Get());
        }

        // GET: LearningResources/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var learningResource = await _learningResourceService.Get(id);
            if (learningResource == null)
            {
                return NotFound();
            }

            return View(learningResource);
        }

        // GET: LearningResources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LearningResources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Url")] LearningResource learningResource)
        {
            if (ModelState.IsValid)
            {
                await _learningResourceService.Add(learningResource);
                return RedirectToAction(nameof(Index));
            }
            return View(learningResource);
        }

        // GET: LearningResources/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var learningResource = await _learningResourceService.Get(id);
            if (learningResource == null)
            {
                return NotFound();
            }
            return View(learningResource);
        }

        // POST: LearningResources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Url")] LearningResource learningResource)
        {
            if (id != learningResource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _learningResourceService.Update(learningResource);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningResourceExists(learningResource.Id))
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
            return View(learningResource);
        }

        // GET: LearningResources/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var learningResource = await _learningResourceService.Get(id);
            if (learningResource == null)
            {
                return NotFound();
            }

            return View(learningResource);
        }

        // POST: LearningResources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _learningResourceService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        private bool LearningResourceExists(int id)
        {
            var learningResource = _learningResourceService.Get(id);
            return (learningResource != null);

        }
    }
}