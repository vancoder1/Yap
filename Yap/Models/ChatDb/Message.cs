using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yap.Models.ChatDb
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        public string SenderId { get; set; } = null!;

        [ForeignKey(nameof(SenderId))]
        public virtual ApplicationUser Sender { get; set; } = null!;

        [Required]
        public string RecipientId { get; set; } = null!;

        [ForeignKey(nameof(RecipientId))]
        public virtual ApplicationUser Recipient { get; set; } = null!;
    }
}
