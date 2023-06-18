using Mansur_TodoGF.Areas.Identity.Data;
using Mansur_TodoGF.Models;
using Mansur_TodoGF.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Mansur_TodoGF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TodoGFContext _context;
        public HomeController(ILogger<HomeController> logger, TodoGFContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Todo> data = new List<Todo>();

            if (User.Identity.IsAuthenticated)
                data = _context.Todo.Where(t => t.CreatedBy == userId).ToList();
            else
                data = _context.Todo.ToList();

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] TodoSaveReq model)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Todo todo = new Todo
                {
                    Description = model.descriptions,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    IsCompleted = false,
                    ID = model.id.ToInt() > 0 ? model.id : 0
                };
                if (todo.ID.ToInt() > 0)
                    _context.Update(todo);
                else
                    _context.Add(todo);

                await _context.SaveChangesAsync();
                int newID = todo.ID;
                return Ok(new { id = newID });

            }
            return BadRequest(new { message = "Something went wrong", isSuccess = false });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Todo == null)
                return BadRequest(new { message = "Todo deleting failed!", isSuccess = false });

            var todo = await _context.Todo.FindAsync(id);
            if (todo != null)
            {
                _context.Todo.Remove(todo);
            }
            await _context.SaveChangesAsync();

            return Ok(new { message = "Todo deleted successfully.", isSuccess = true });
            //return Ok();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}