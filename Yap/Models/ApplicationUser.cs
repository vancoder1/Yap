using Microsoft.AspNetCore.Identity;
using Yap.Models.ChatDb;

namespace Yap.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();
    }
}
