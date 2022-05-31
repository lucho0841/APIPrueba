using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Host.Models
{
    public class Persona
    {
        public int AnimoId { get; set; }
        public string Nombre { get; set; }
        public string EstadoAnimo { get; set; }
        public double Resultado { get; set; }
        public string FechaCreacion { get; set; }
    }
}
