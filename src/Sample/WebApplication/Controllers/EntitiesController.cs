using Microsoft.AspNetCore.Mvc;
using WebApplication.Dtos;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesController : ControllerBase
    {
        private AppService _service;

        public EntitiesController(AppService service)
        {
            _service = service;
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
