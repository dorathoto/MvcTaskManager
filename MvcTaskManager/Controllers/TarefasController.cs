using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcTaskManager.Context;
using MvcTaskManager.Entities;

namespace MvcTaskManager.Controllers
{
    [Authorize]

    public class TarefasController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context,UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tarefas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Tarefas.Include(t => t.Usuario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Tarefas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id");
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TarefaId,Descricao,DataRealizacaoTarefa,UsuarioId")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.TarefaId = Guid.NewGuid();
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", tarefa.UsuarioId);
            return View(tarefa);
        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", tarefa.UsuarioId);
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TarefaId,Descricao,DataRealizacaoTarefa,UsuarioId")] Tarefa tarefa)
        {
            if (id != tarefa.TarefaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.TarefaId))
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
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", tarefa.UsuarioId);
            return View(tarefa);
        }

        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Tarefas == null)
            {
                return Problem("Entity set 'AppDbContext.Tarefas'  is null.");
            }
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(Guid id)
        {
            return _context.Tarefas.Any(e => e.TarefaId == id);
        }
    }
}