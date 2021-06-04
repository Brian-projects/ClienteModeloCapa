using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Cliente : TableBase
    {
        [Required, StringLength(20)]
        public string Nombre { get; set; }
        [Required, StringLength(20)]
        public string Apellidos { get; set; }
        [Required, StringLength(13, MinimumLength =11)]
        public string Cedula { get; set; }
        [StringLength(12, MinimumLength = 10)]
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        [Required]
        public bool Balance { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int TipoClienteId { get; set; }
        public int EstatusId { get; set; }
        public TipoCliente TipoCliente { get; set; }
        public Estatus Estatus { get; set; }
    }
}
