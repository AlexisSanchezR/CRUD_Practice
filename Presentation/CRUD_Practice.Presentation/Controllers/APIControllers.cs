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
        public APIControllers(IUserService userService)
        {
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
        public async Task<IActionResult> GetById(string userId)
        {
            var user = await _userService.GetById(userId);
            return Ok(user);
        }

        [HttpGet]
        [Route("getAll-user")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }
        
        [HttpPut]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser (string userId, [FromBody] UserModel updateUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _userService.UpdateUser(userId, updateUser);

            if (!updated)
                return NotFound(new { Message = "User not found." });

            return NoContent(); // Convención REST para Update
        }

        [HttpDelete]
        [Route("delete-user")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var deleted=await _userService.DeleteUser(userId);
            if (!deleted)
            {
                return NotFound(new { Message = "User not found." });
            }
            return Ok(new { Message = "user deleted successfully" });
        }
    }
}
