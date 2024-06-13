using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Yap.Data;
using Yap.Models;

namespace Yap.Controllers
{
    public class ChatRoomsController : Controller
    {
        private readonly ILogger<ChatRoomsController> _logger;
        private readonly ChatDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatRoomsController(ChatDbContext context, UserManager<ApplicationUser> userManager, ILogger<ChatRoomsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var chatRooms = await _context.ChatRooms
                .Where(cr => cr.Users.Any(u => u.Id == userId))
                .ToListAsync();
            return View(chatRooms);
        }

        public async Task<IActionResult> Enter(int id)
        {
            var chatRoom = await _context.ChatRooms
                .Include(cr => cr.Messages)
                .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(cr => cr.Id == id);

            if (chatRoom == null)
            {
                return NotFound();
            }
            return View(chatRoom);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
