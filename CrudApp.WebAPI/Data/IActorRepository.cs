using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudApp.WebAPI.Models;

namespace CrudApp.WebAPI.Data
{
    public interface IActorRepository
    {
       public List<Actor> GetAllActors();
       public Actor GetActorById(int Id);
       public bool AddActor(Actor actor);
       public bool UpdateActor(Actor actor);
       public bool DeleteActor(int Id);


        
    }
}