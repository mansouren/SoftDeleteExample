using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftDeleteExample.DatabaseContext;
using SoftDeleteExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftDeleteExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDBContext applicationDBContext;

        public BookController(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return await applicationDBContext.Books.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await applicationDBContext.Books.FindAsync(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
                throw new BadHttpRequestException("Bad Request...");
            await applicationDBContext.Books.AddAsync(book);
            await applicationDBContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task Update(int id, Book book)
        {
            if (!ModelState.IsValid)
                throw new BadHttpRequestException("Bad Request...");
            var UpdatedBook = await applicationDBContext.Books.FindAsync(id);
            if (UpdatedBook != null)
            {
                UpdatedBook.Name = book.Name;
                UpdatedBook.IsDeleted = book.IsDeleted;
                UpdatedBook.CatgoryId = book.CatgoryId;
                applicationDBContext.Books.Update(UpdatedBook);
                await applicationDBContext.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("", "This record had been deleted Before!");
            }
           
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var book = await applicationDBContext.Books.FindAsync(id);
            applicationDBContext.Books.Remove(book);
            await applicationDBContext.SaveChangesAsync();
            return Ok();
        }
    }
}
