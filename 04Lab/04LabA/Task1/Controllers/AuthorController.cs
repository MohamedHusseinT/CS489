using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MohamedElibrary.Data;
using MohamedElibrary.Models;

namespace MohamedElibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly MohamedElibraryDbContext _context;

        public AuthorController(MohamedElibraryDbContext context)
            => _context = context;

        [HttpGet("{id}")]
        public async Task<IActionResult> AuthorBookList(int id)
        {
            // Check if author exists
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == id);
            if (!authorExists)
                return NotFound();

            // Get books without navigation properties to avoid circular references
            var books = await _context.Books
                .Where(b => b.AuthorId == id)
                .Select(b => new Book
                {
                    Id = b.Id,
                    Isbn = b.Isbn,
                    Title = b.Title,
                    DatePublished = b.DatePublished,
                    PublisherPublisherId = b.PublisherPublisherId,
                    AuthorId = b.AuthorId,
                    PublishingCompanyId = b.PublishingCompanyId
                })
                .ToListAsync();

            return Ok(books);
        }

        [HttpGet("{id}/Books")]
        public async Task<ActionResult<List<Book>>> AuthorBookListAsync(long id)
        {
            var books = await _context.Books
                .Where(b => b.AuthorId == id)
                .Select(b => new Book
                {
                    Id = b.Id,
                    Isbn = b.Isbn,
                    Title = b.Title,
                    DatePublished = b.DatePublished,
                    PublisherPublisherId = b.PublisherPublisherId,
                    AuthorId = b.AuthorId,
                    PublishingCompanyId = b.PublishingCompanyId
                    // Exclude navigation properties to avoid circular references
                })
                .ToListAsync();

            if (!books.Any())
                return NotFound();

            return books;
        }
    }
}
