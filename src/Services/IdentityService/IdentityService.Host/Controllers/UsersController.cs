using IdentityService.Host.Data;
using IdentityService.Host.Models;
using IdentityService.Host.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SharedService.Responses;

namespace IdentityService.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[ApiExplorerSettings(GroupName = "API Identity Test Compurent")]
    public class UsersController : ControllerBase
    {
        private UserDL userDL = new UserDL("User.dat");
        User newUser;

        /// <summary>
        /// Autentificacion del usuario
        /// </summary>
        /// <param name="email">correo del usuario a consultar</param>
        /// <param name="password">contrasena del usuario a consultar</param>
        /// <returns>Authenticate result</returns>
        /// <response code="200">Resultado de la autentificacion</response>
        /// <response code="400">Error en el proceso</response>
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<PetitionResponse> Authenticate(string email, string password)
        {
            List<User> listUsers = new List<User>();
            listUsers = userDL.Get();
            int id = 1;
            if (listUsers.Count == 0)
            {
                return Task.FromResult(new PetitionResponse
                {
                    success = false,
                    message = "No hay usuarios registrados",
                    result = null
                });
            }
            else
            {
                for (int i = 0; i < listUsers.Count; i++)
                {
                    if (listUsers[i].email != email)
                    {
                        id += 1;
                    }
                    else
                    {
                        if (listUsers[i].userId == listUsers[i].userId)
                        {
                            if (email != null || password != null)
                            {

                                if (email.Equals(listUsers[i].email) && password.Equals(listUsers[i].password))
                                {
                                    return Task.FromResult(new PetitionResponse
                                    {
                                        success = true,
                                        message = "Ok",
                                        result = listUsers[i]
                                    });
                                }
                                else
                                {
                                    return Task.FromResult(new PetitionResponse
                                    {
                                        success = false,
                                        message = "Datos incorrectos, por favor validar la información!",
                                        result = null
                                    });
                                }
                            }
                        }
                    }
                    
                }
            }
            return Task.FromResult(new PetitionResponse
            {
                success = false,
                message = "Correo incorrecto, por favor validar la información!",
                result = null
            });
        }

        /// <summary>
        /// Crear usuario
        /// </summary>
        /// <param name="user">Objeto usuario a consultar</param>
        /// <returns>Authenticate result</returns>
        /// <response code="200">Resultado de la creacion del usuario</response>
        /// <response code="400">Error en el proceso</response>
        [HttpPost("createUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<PetitionResponse> createUser(User user)
        {
            List<User> listUsers = new List<User>();
            listUsers = userDL.Get();
            if (user == null)
            {
                return Task.FromResult(new PetitionResponse
                {
                    success = false,
                    message = "usuario no válido",
                    result = null
                });
            }
            var result = userDL.Save(user);
            for (int i = 0; i < listUsers.Count; i++)
            {
                if (listUsers[i].email == user.email)
                {
                    return Task.FromResult(new PetitionResponse
                    {
                        success = false,
                        message = "Este correo ya se encuentra en uso, por favor utilice un correo que no haya sido registrado",
                        result = null
                    });
                }
            }
            if (result == null)
            {
                return Task.FromResult(new PetitionResponse
                {
                    success = false,
                    message = "Error al intentar guardar el usuario",
                    result = null
                });
            }
            return Task.FromResult(new PetitionResponse
            {
                success = true,
                message = "Usuario creado con exito!",
                result = result
            });
        }

        /// <summary>
        /// Validar usuario existente
        /// </summary>
        /// <param name="userId">id del usuario a consultar</param>
        /// <returns>findUser result</returns>
        /// <response code="200">Resultado de si existe el usuario</response>
        /// <response code="400">Error en el proceso</response>
        [HttpGet("userExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<PetitionResponse> userExist(int userId)
        {
            newUser = new User();
            int id = userId;
            newUser = userDL.Get(id);
            if (newUser?.userId == userId)
            {
                return Task.FromResult(new PetitionResponse
                {
                    success = true,
                    message = "Usuario encontrado con exito",
                    result = newUser
                });
            }
            else
            {
                return Task.FromResult(new PetitionResponse
                {
                    success = false,
                    message = "Usuario no encontrado",
                    result = null
                });
            }
        }

        /// <summary>
        /// Consulta de todos los usuarios registrados
        /// </summary>
        /// <returns>findUser result</returns>
        /// <response code="200">Resultado de consulta de usuarios</response>
        /// <response code="400">Error en el proceso</response>
        [HttpGet("getAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public Task<PetitionResponse> getAllUsers()
        {
            List<User> listUsers = new List<User>();
            listUsers = userDL.Get();
            if (listUsers.Count == 0)
            {
                return Task.FromResult(new PetitionResponse
                {
                    success = false,
                    message = "Ningun usuario registrado actualmente",
                    result = null
                });
            }
            return Task.FromResult(new PetitionResponse
            {
                success = true,
                message = "Usuarios obtenidos correctamente",
                result = listUsers.Count()
            });
        
        }
    }
}
