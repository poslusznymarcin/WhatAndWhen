using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatAndWhenData;
using WhatAndWhenData.Entities;

namespace WhatAndWhen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentAPIController : ControllerBase
    {
        private readonly WhatAndWhenContext _context;

        public CommentAPIController(WhatAndWhenContext context)
        {
            _context = context;
        }

        // GET: api/CommentAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentEntity>>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }

        // GET: api/CommentAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentEntity>> GetCommentEntity(int id)
        {
            var commentEntity = await _context.Comments.FindAsync(id);

            if (commentEntity == null)
            {
                return NotFound();
            }

            return commentEntity;
        }

        // PUT: api/CommentAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommentEntity(int id, CommentEntity commentEntity)
        {
            if (id != commentEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(commentEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CommentAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CommentEntity>> PostCommentEntity(CommentEntity commentEntity)
        {
            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentEntity", new { id = commentEntity.Id }, commentEntity);
        }

        // DELETE: api/CommentAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentEntity(int id)
        {
            var commentEntity = await _context.Comments.FindAsync(id);
            if (commentEntity == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(commentEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentEntityExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
