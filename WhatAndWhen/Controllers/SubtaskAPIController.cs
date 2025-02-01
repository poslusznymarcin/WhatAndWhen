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
    public class SubtaskAPIController : ControllerBase
    {
        private readonly WhatAndWhenContext _context;

        public SubtaskAPIController(WhatAndWhenContext context)
        {
            _context = context;
        }

        // GET: api/SubtaskAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubtaskEntity>>> GetSubtasks()
        {
            return await _context.Subtasks.ToListAsync();
        }

        // GET: api/SubtaskAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubtaskEntity>> GetSubtaskEntity(int id)
        {
            var subtaskEntity = await _context.Subtasks.FindAsync(id);

            if (subtaskEntity == null)
            {
                return NotFound();
            }

            return subtaskEntity;
        }

        // PUT: api/SubtaskAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubtaskEntity(int id, SubtaskEntity subtaskEntity)
        {
            if (id != subtaskEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(subtaskEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubtaskEntityExists(id))
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

        // POST: api/SubtaskAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubtaskEntity>> PostSubtaskEntity(SubtaskEntity subtaskEntity)
        {
            _context.Subtasks.Add(subtaskEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubtaskEntity", new { id = subtaskEntity.Id }, subtaskEntity);
        }

        // DELETE: api/SubtaskAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubtaskEntity(int id)
        {
            var subtaskEntity = await _context.Subtasks.FindAsync(id);
            if (subtaskEntity == null)
            {
                return NotFound();
            }

            _context.Subtasks.Remove(subtaskEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubtaskEntityExists(int id)
        {
            return _context.Subtasks.Any(e => e.Id == id);
        }
    }
}
