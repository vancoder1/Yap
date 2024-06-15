using Microsoft.EntityFrameworkCore;
using Yap.Data;
using Yap.Models;
using Yap.Models.ChatDb;

namespace Yap.Services
{
    public class ChatService
    {
        private readonly ChatDbContext _context;

        public ChatService(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChatDto>> GetInboxAsync(string? userId)
        {
            if (userId == null)
            {
                return new List<ChatDto>();
            }
            var messages = await _context.Messages
                .Where(m => m.SenderId == userId || m.RecipientId == userId)
                .ToListAsync();

            var chats = messages
                .GroupBy(m => m.SenderId == userId ? m.RecipientId : m.SenderId)
                .Select(g => new ChatDto
                {
                    ParticipantId = g.Key,
                    Participant = g.Select(m => m.SenderId == userId ? m.Recipient : m.Sender).FirstOrDefault(),
                    Messages = g.OrderBy(m => m.Timestamp).ToList()
                })
                .ToList();

            return chats;
        }

        public class ChatDto
        {
            public string? ParticipantId { get; set; }
            public ApplicationUser? Participant { get; set; }
            public List<Message> Messages { get; set; } = new List<Message>();
        }
    }
}
