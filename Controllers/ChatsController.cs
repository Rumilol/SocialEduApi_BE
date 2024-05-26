using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialEduApi.Data;
using SocialEduApi.Models.Entities;
using SocialEduApi.Models.ViewModels;

namespace SocialEduApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Chats/5
        [HttpGet("foruser/{email}")]
        public async Task<ActionResult<IList<UserChatVM>>> GetChats(string email)
        {
            var thisUser = await _context.Users.FirstOrDefaultAsync(p => p.Email == email);
            if (thisUser == null) return NotFound("User s tim emailom nije pronađen");

            var users = await _context.Users.ToListAsync();
            var messages = _context.ChatMessages.Include(p => p.Sender)
                                                .Include(p => p.Recipient)
                                                .Where(p => p.SenderID == thisUser.Id || p.RecipientID == thisUser.Id)
                                                .ToList();
            var groupedM = messages.GroupBy(p => p.UniqueChatId);
            var returned = new List<UserChatVM>();
            foreach (var group in groupedM)
            {
                var message = group.First();
                var participants = new List<string>() { message.SenderID, message.RecipientID };
                var otherPersonID = participants.FirstOrDefault(p => p != thisUser.Id);
                if (otherPersonID == null) otherPersonID = thisUser.Id;
                var otherPerson = users.FirstOrDefault(p => p.Id == otherPersonID);
                var chat = new UserChatVM(otherPerson, group.ToList());
                returned.Add(chat);
            }
            return returned;
        }

        // GET: api/Chats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatMessage>> GetChatMessage(int id)
        {
            var chat = await _context.ChatMessages.FindAsync(id);

            if (chat == null)
            {
                return NotFound();
            }

            return chat;
        }

        // POST: api/Chats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChatMessage>> PostChat(ChatMessageVM vm)
        {
            if (vm.Content == null) return BadRequest("Nije poslan content");
            var sender = await _context.Users.FirstOrDefaultAsync(p => p.Email == vm.SenderEmail);
            if (sender == null) return BadRequest("Nije pronađen sender s tim emailom");
            var recipient = await _context.Users.FirstOrDefaultAsync(p => p.Email == vm.RecipientEmail);
            if (recipient == null) return BadRequest("Nije pronađen recipient s tim emailom");

            var chat = new ChatMessage
            {
                SenderID = sender.Id,
                RecipientID = recipient.Id,
                Content = vm.Content,
                TimeStamp = DateTime.Now,
            };

            _context.ChatMessages.Add(chat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChatMessage", new { id = chat.Id }, chat);
        }

        // DELETE: api/Chats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            var chat = await _context.ChatMessages.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.ChatMessages.Remove(chat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
