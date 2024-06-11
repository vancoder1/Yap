using Microsoft.EntityFrameworkCore;
using Yap.Models.ChatDb;

namespace Yap.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        { }

        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
    }
}
