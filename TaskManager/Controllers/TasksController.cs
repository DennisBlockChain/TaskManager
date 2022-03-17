#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var allTasks = await _context.Tasks.ToListAsync();
            foreach (var task in allTasks) { 
            if(task.DateDone.Day < DateTime.Now.Day && task.DateDone.Month == DateTime.Now.Month)
                {
                    task.Done = false;
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                } else if(task.DateDone.Month < DateTime.Now.Month)
                {
                    task.Done = false;
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                } 
            }

            var applicationDbContext = _context.Tasks.Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskName,Date,Description,Repetitive,CategoryID")] Tasks tasks)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Name", tasks.CategoryID);
                return RedirectToAction(nameof(Index));
            //}
            
            //return View(tasks);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Name", tasks.CategoryID);
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaskName,Date,Description,Repetitive,CategoryID")] Tasks tasks)
        {
            if (id != tasks.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Name", tasks.CategoryID);
            return View(tasks);
        }

        public async Task<IActionResult> TaskDone(int id)
        {
            Tasks task = await _context.Tasks.FindAsync(id);
            if (task.Done == false)
            {
                task.Done = true;
                task.DateDone = DateTime.Now;
                _context.Update(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Tasks/Delete/5
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
