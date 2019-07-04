using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiSegunda.Context;
using MiSegunda.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiSegunda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController: ControllerBase 
    {
        private readonly ApplicationDbContext Context;
        public AutoresController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public ActionResult <IEnumerable <Autor>> Get()
        {
            return Context.Autores.ToList();
        }
        [HttpGet("{id}", Name ="ObtenerAutor")]
        public ActionResult<Autor> Get(int id)
        {
            var autor  = Context.Autores.Include(x=> x.Libros).FirstOrDefault(x => x.Id == id);
            if(autor == null)
            {
                return NotFound();
            }
            return autor;
        }
        [HttpPost]
        public ActionResult Post ([FromBody] Autor autor)
        {
            Context.Autores.Add(autor);
            Context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id}, autor );
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Autor value)
        {
            if(id!= value.Id)
            {
                return BadRequest();
            }
            Context.Entry(value).State = EntityState.Modified;
            Context.SaveChanges();
            return Ok(value);
        }
        [HttpDelete("{id}")]
        public  ActionResult<Autor> Delete(int id)
        {
            var autor = Context.Autores.FirstOrDefault(x => x.Id == id);
            if (autor == null) return NotFound();
            Context.Autores.Remove(autor);
            Context.SaveChanges();
            return autor;
        }

        
    }
}
