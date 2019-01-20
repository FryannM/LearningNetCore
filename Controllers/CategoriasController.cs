namespace SistemaAC.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SistemaAC.Data;
    using SistemaAC.ModelClass;
    using SistemaAC.Models;
    using SistemaAC.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CategoriasController" />
    /// </summary>
    public class CategoriasController : Controller
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Defines the _categoriservices
        /// </summary>
        private readonly CategoriaServices _categoriservices;

        // en caso de no funcionar ,saca el obj Categoria del constructor  y solo  inicianizalo
        // en el constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriasController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="ApplicationDbContext"/></param>
        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
            _categoriservices = new CategoriaServices(_context);
        }


        public List<object[]> filtrarDatos(int numPagina,string valor)
        {
            return _categoriservices.filtrarDatos(numPagina, valor);
        }
     
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoria.ToListAsync());
        }

        // GET: Categorias/Details/5
        /// <summary>
        /// The Details
        /// </summary>
        /// <param name="id">The id<see cref="int?"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .SingleOrDefaultAsync(m => m.CatagoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        /// <summary>
        /// The SaveCategoria
        /// </summary>
        /// <param name="vm">The vm<see cref="CategoriaViewModel"/></param>
        /// <returns>The <see cref="List{IdentityError}"/></returns>
        public List<IdentityError> SaveCategoria(CategoriaViewModel vm)
        {
            return _categoriservices.SaveCategoria(vm);
        }

      
       
        // GET: Categorias/Edit/5
        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="id">The id<see cref="int?"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.SingleOrDefaultAsync(m => m.CatagoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatagoriaID,Nombre,Descripcion,Estado")] Categoria categoria)
        {
            if (id != categoria.CatagoriaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.CatagoriaID))
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
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="int?"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .SingleOrDefaultAsync(m => m.CatagoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        /// <summary>
        /// The DeleteConfirmed
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categoria.SingleOrDefaultAsync(m => m.CatagoriaID == id);
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// The CategoriaExists
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.CatagoriaID == id);
        }
    }
}
