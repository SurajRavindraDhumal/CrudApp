using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudApp.WebAPI.Helper;
using CrudApp.WebAPI.Models;
using Microsoft.Data.Sqlite;

namespace CrudApp.WebAPI.Data
{
    public class ActorRepository : IActorRepository
    {
        public bool AddActor(Actor actor)
        {
            using var connection = new SqliteConnection(DatabaseHelper.GetConnectionString());


            connection.Open();
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
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteActor(int Id)
        {
            using var connection = new SqliteConnection(DatabaseHelper.GetConnectionString());


            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Actors WHERE Id = @Id;";


            command.Parameters.AddWithValue("@Id", Id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Actor GetActorById(int Id)
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
            return actor;
        }

        public List<Actor> GetAllActors()
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
            return actors;
        }

        public bool UpdateActor(Actor actor)
        {
            if (actor == null || actor.Name == null)
            {
                return false;
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
            command.Parameters.AddWithValue("@Id", actor.Id);


            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}