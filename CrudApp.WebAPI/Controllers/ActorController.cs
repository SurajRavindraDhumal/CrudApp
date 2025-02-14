using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using CrudApp.WebAPI.Data;
using CrudApp.WebAPI.Helper;
using CrudApp.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace CrudApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        [HttpGet("GetActors")]
        public IActionResult GetActors()
        {
            ActorRepository actorRepository = new ActorRepository();
            return Ok(actorRepository.GetAllActors());

        }

        [HttpGet("GetActorById/{Id}")]
        public IActionResult GetActorById(int Id)
        {
            ActorRepository actorRepository = new ActorRepository();
            return Ok(actorRepository.GetActorById(Id));
        }

        [HttpPost("AddActor")]
        public IActionResult AddActor([FromBody] Actor actor)
        {
            ActorRepository actorRepository = new ActorRepository();
            return Ok(actorRepository.AddActor(actor));
        }

        [HttpPost("UpdateActor/{Id}")]
        public IActionResult UpdateActor(int Id, [FromBody] Actor actor)
        {
            ActorRepository actorRepository = new ActorRepository();
            actor.Id = Id;
            return Ok(actorRepository.UpdateActor(actor));
        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult DeleteActor(int Id)
        {
            ActorRepository actorRepository = new ActorRepository();
            return Ok(actorRepository.DeleteActor(Id));
        }

    }
}