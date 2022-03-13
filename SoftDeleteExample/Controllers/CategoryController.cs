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
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDBContext applicationDBContext;

        public CategoryController(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await applicationDBContext.Categories.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var book = await applicationDBContext.Categories.FindAsync(id);
            return Ok(book);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                throw new BadHttpRequestException("Bad Request...");
            await applicationDBContext.Categories.AddAsync(category);
            await applicationDBContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, Category category)
        {
            if (!ModelState.IsValid)
                throw new BadHttpRequestException("Bad Request...");
            var UpdatedCategory = await applicationDBContext.Categories.FindAsync(id);
            UpdatedCategory.Title = category.Title;
            
            applicationDBContext.Categories.Update(UpdatedCategory);
            await applicationDBContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await applicationDBContext.Categories.FindAsync(id);
            applicationDBContext.Categories.Remove(category);
            await applicationDBContext.SaveChangesAsync();
            return Ok();
        }
    }
}
