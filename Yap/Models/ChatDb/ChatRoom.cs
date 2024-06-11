using System.ComponentModel.DataAnnotations;

namespace Yap.Models.ChatDb
{
    public class ChatRoom
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
