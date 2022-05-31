using IdentityService.Host.Data;
using IdentityService.Host.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmocionesController
    {
        List<Persona> listPersonas = new List<Persona>();
        private UserDL userDL = new UserDL("Personas.dat");


        /// <summary>
        /// Crear personas 
        /// </summary>
        /// <returns>resultado Crear personas</returns>
        /// <param name="persona">Objeto tipo persona</param>
        /// <response code="200">Resultado de crear personas</response>
        /// <response code="400">Error en el proceso</response>
        [HttpPost("crearPersona")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<PetitionResponse> crearPersona(Persona persona)
        {
            if (persona == null)
            {
                return Task.FromResult(new PetitionResponse
                {
                    success = false,
                    message = "Persona no valida",
                    result = null
                });
            }
            var result = userDL.SavePersona(persona);
            if (result == false)
            {
                return Task.FromResult(new PetitionResponse
                {
                    success = false,
                    message = "Error al intentar guardar la persona",
                    result = null
                });
            }
            return Task.FromResult(new PetitionResponse
            {
                success = true,
                message = "Persona creada con exito!",
                result = result
            });
        }

        /// <summary>
        /// Consulta de todos las personas registradas
        /// </summary>
        /// <returns>resultado obtener todas las personas</returns>
        /// <response code="200">Resultado de consulta de personas</response>
        /// <response code="400">Error en el proceso</response>
        [HttpGet("getAllPersonas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public Task<PetitionResponse> getAllPersonas()
        {
            listPersonas = userDL.GetPersonas();
            ResultadoPersona resultado = new ResultadoPersona();
            resultado.Lista = listPersonas;
            resultado.Tamano = listPersonas.Count();
            if (listPersonas.Count == 0)
            {
                return Task.FromResult(new PetitionResponse
                {
                    success = false,
                    message = "Ninguna persona registrada actualmente",
                    result = null
                });
            }
            return Task.FromResult(new PetitionResponse
            {
                success = true,
                message = "Personas obtenidas correctamente",
                result = resultado
            });

        }

        /// <summary>
        /// Eliminar personas
        /// </summary>
        /// <returns>resultado eliminar personas</returns>
        /// <response code="200">Resultado de consulta de personas</response>
        /// <response code="400">Error en el proceso</response>
        [HttpDelete("deletePersona")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public Task<PetitionResponse> deletePersona(int AnimoId)
        {
            var result = userDL.Delete(AnimoId);

            if (result == false)
            {
                return Task.FromResult(new PetitionResponse
                {
                    success = false,
                    message = "Persona no encontrada para eliminar",
                    result = null
                });
            }
            return Task.FromResult(new PetitionResponse
            {
                success = true,
                message = "Persona Eliminada correctamente",
                result = null
            });
        }
    }
}
