using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admonish;
using Domain;
using WebApplication.Dtos;

namespace WebApplication
{
    // NB. Fix documentation (intro.md) if you change this class.
    public class AppService
    {
        private static Dictionary<string, Entity> _db =
            new Dictionary<string, Entity>();

        internal void AddEntity(CreateEntityDto dto)
        {
            Validator
                .Create()
                .Check(
                    nameof(dto.Name),
                    !_db.ContainsKey(dto.Name ?? ""),
                    "An entity with this name already exists.")
                .ThrowIfInvalid();

            var e = new Entity(dto.Age, dto.Name);
            _db.Add(e.Name, e);
        }

        internal int GetCount(int minAge)
        {
            return _db.Values.Where(x => x.Age >= minAge).Count();
        }
    }
}
