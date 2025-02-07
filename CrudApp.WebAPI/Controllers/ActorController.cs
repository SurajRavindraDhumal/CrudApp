using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
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

            var actors = new List<Actor>();
            using (var connection = new SqliteConnection(DatabaseHelper.GetConnectionString()))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Actors;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        actors.Add(new Actor
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Age = reader.GetInt16(3),
                            NetWorth = reader.GetInt32(4)
                        });
                    }
                }
            }

            return Ok(actors);

        }

        [HttpGet("GetActorById/{Id}")]
        public IActionResult GetActorById(int Id)
        {
            Actor actor = new Actor();
            using (var connection = new SqliteConnection(DatabaseHelper.GetConnectionString()))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Actors WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id", Id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        actor = new Actor
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Age = reader.GetInt16(3),
                            NetWorth = reader.GetInt32(4)
                        };
                    }
                }
            }
            return Ok(actor);
        }

        [HttpPost("AddActor")]
        public IActionResult AddActor([FromBody] Actor actor)
        {
            using var connection = new SqliteConnection(DatabaseHelper.GetConnectionString());


            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @" INSERT INTO Actors (Name,Description,Age,NetWorth)
                VALUES (@Name,@Description,@Age,@NetWorth);";

            command.Parameters.AddWithValue("@Name", actor.Name);
            command.Parameters.AddWithValue("@Description", actor.Description);
            command.Parameters.AddWithValue("@Age", actor.Age);
            command.Parameters.AddWithValue("@NetWorth", actor.NetWorth);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                return Ok("Actor Added Succefully.");
            }
            else
            {
                return BadRequest("Actor failed to add");
            }

        }

        [HttpPost("UpdateActor/{Id}")]
        public IActionResult UpdateActor(int Id, [FromBody] Actor actor)
        {
            if (actor == null || actor.Name == null)
            {
                return BadRequest("Actor Is Not Valid");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            using var connection = new SqliteConnection(DatabaseHelper.GetConnectionString());


            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE Actors SET 
                                        Name = @Name, 
                                        Description = @Description, 
                                        Age = @Age, 
                                        NetWorth = @NetWorth 
                                        WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Name", actor.Name);
            command.Parameters.AddWithValue("@Description", actor.Description);
            command.Parameters.AddWithValue("@Age", actor.Age);
            command.Parameters.AddWithValue("@NetWorth", actor.NetWorth);
            command.Parameters.AddWithValue("@Id", Id);


            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                return Ok("Actor Updated Succefully");
            }
            else
            {
                return BadRequest("Actor Is Not Updated");
            }

        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult DeleteActor(int Id)
        {
            using var connection = new SqliteConnection(DatabaseHelper.GetConnectionString());


            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Actors WHERE Id = @Id;";


            command.Parameters.AddWithValue("@Id", Id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                return Ok("Actor Deleted successfully.");
            }
            else
            {
                return BadRequest("BadRequest");
            }

        }

    }
}