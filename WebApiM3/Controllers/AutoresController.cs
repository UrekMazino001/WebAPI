using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiM3.Context;
using WebApiM3.Entities;
using WebApiM3.Helpers;
using WebApiM3.Models;

namespace WebApiM3.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // -> Se encarga de validar que el modelo sea valido.
    public class AutoresController : ControllerBase
    {
        //Inyeccion de dependencias, de la instancia del DbContext

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/listadoautores")] //combinacion de ruteo, ignora la comveniencia de ruteo.

        [ServiceFilter(typeof(MiFiltroAccion))]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> get()
        {
            ///var autores = await context.Autores.Include(x => x.Libros).ToListAsync();
            var autores = await context.Autores.ToListAsync();
            var autoresDTO = mapper.Map<List<AutorDTO>>(autores);

            return autoresDTO;

        }

        //[HttpGet("{id}/{param=Ariel}", Name = "ObtenerAutor")] // segundo parametro con valor inicializado.      
        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> get(int id) // , [BindRequired] string param Bind Required hace que el parametro sea requerido.
        {
            var autor = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id); // & x.Nombre == param

            if (autor == null)
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorDTO>(autor);
            return autorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacion)
        {
            var autor = mapper.Map<Autor>(autorCreacion); //Mappeo el modelo de creacion con la entidad, ya que EF trabaja con entidades.

            TryValidateModel(autor); //Funcion para validar el modelo.
            context.Autores.Add(autor);
            await context.SaveChangesAsync();

            var autorDTO = mapper.Map<AutorDTO>(autor);
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autorDTO); //Respuesta con el nuevo registro creado.
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AutorCreacionDTO autorActualizacion)
        {
            var autor = mapper.Map<Autor>(autorActualizacion);
            autor.Id = id;
            //Entry -> Marca la entidad como modificada.
            context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent(); //204
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Autor> PatchDocument) 
        {
            if(PatchDocument == null)
            {
                return BadRequest();
            }

            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if(autor == null)
            {
               return NotFound();
            }

            //Aplicar las operaciones que hemos recibido a traves del JsonPatch
            PatchDocument.ApplyTo(autor, ModelState); //Si una de las operaciones es mala, podemos saber que el modelo no es valido.
            var isValid = TryValidateModel(autor);

            if(isValid){
                return BadRequest(ModelState);
            }


            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> Delete(int id)
        {
            var autorID = await context.Autores.Select(x => x.Id).FirstOrDefaultAsync(x => x == id); //Selecciono solo el campo de ID

            if(autorID == default(int))
            {
                return NotFound();
            }

            context.Autores.Remove(new Autor { Id = autorID }); //Remueve el autor cuyo id es El ID que acabamos de encontrar.
            await  context.SaveChangesAsync();
            return Ok();
        }
    }
}
