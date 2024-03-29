﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvCreating.Data;
using EvCreating.Models;
using Microsoft.AspNetCore.Authorization;
using EvCreating.Areas.Identity.Data;

namespace EvCreating.Controllers
{
    [Authorize(Roles = "SystemAdministrator")]
    public class ParametersController : Controller
    {
        private readonly EvCreatingContext _context;

        public ParametersController(EvCreatingContext context)
        {
            _context = context;
        }

        // GET: Parameters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parameter.OrderBy(p => p.Name).ToListAsync());
        }


        // GET: Parameters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Parameter == null)
            {
                return NotFound();
            }

            var parameter = await _context.Parameter.FindAsync(id);
            if (parameter == null)
            {
                return NotFound();
            }
            return View(parameter);
        }

        // POST: Parameters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Value,UserId,Description,LastChanged,Obsolete,Destination")] Parameter parameter)
        {
            if (id != parameter.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    parameter.UserId = _context.Users.First(u => u.Id == User.Identity.Name).Id;
                    parameter.LastChanged = DateTime.Now;
                    _context.Update(parameter);
                    Globals.Parameters[parameter.Name] = parameter;
                    if (parameter.Destination == "Mail")
                        Globals.ConfigureMail();
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParameterExists(parameter.Name))
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
            return View(parameter);
        }


        private bool ParameterExists(string id)
        {
            return (_context.Parameter?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}