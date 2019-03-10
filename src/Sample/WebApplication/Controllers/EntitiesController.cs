using Microsoft.AspNetCore.Mvc;
using WebApplication.Dtos;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class EntitiesController : ControllerBase
    {
        private AppService _service;

        public EntitiesController(AppService service)
        {
            _service = service;
        }

        [Route("count")]
        [HttpGet]
        public int GetCount([Required]int minAge)
        {
            return _service.GetCount(minAge);
        }

        [HttpPost]
        public void Post(CreateEntityDto dto)
        {
            _service.AddEntity(dto);
        }

        [Route("attr")]
        [HttpPost]
        public void ValidateDeclaratively(AttributeValidatedDto dto)
        {
        }
    }
}
