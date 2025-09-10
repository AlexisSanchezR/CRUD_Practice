using CRUD_Practice.Bussines.Interfaces;
using CRUD_Practice.Domain.models;
using CRUD_Practice.models;
using Microsoft.AspNetCore.Mvc;


namespace CRUD_Practice.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class APIControllers : ControllerBase
    {
        private readonly IUserService _userService;
        public APIControllers(IUserService userService) {
            _userService = userService;
        }
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CRUDRequest CRUDRequest)
        {
            var model = new UserModel();
            //Esa línea genera un identificador único en forma de texto
            model.Id = Guid.NewGuid().ToString();
            model.Username = CRUDRequest.Username;
            model.Userlastname = CRUDRequest.Userlastname;
            model.Password = CRUDRequest.Password;
            model.Email = CRUDRequest.Email;
            model.Phone = CRUDRequest.Phone;
            await _userService.CreateUser(model);
            return Created("respuesta", CRUDRequest);
        }

        [HttpGet]
        [Route("get-user")]

        [HttpGet]
        [Route("getAll-user")]

        [HttpPut]
        [Route("update-user")]

        [HttpDelete]
        [Route("delete-user")]
    }
}
