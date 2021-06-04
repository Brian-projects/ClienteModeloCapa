using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    class Cliente : TableBase
    {
        [Required,StringLength(30)]
        public string Nombre { get; set; }
        [Required, StringLength(30)]
        public string Apellidos { get; set; }
        [Required, StringLength(13, MinimumLength = 13)]
        public string Cedula { get; set; }
        [Required, StringLength(12, MinimumLength = 10)]
        public string Telefono { get; set; }
        [EmailAddress]
        public string Correo { get; set; }
        [StringLength(int.MaxValue)]
        public string Direccion { get; set; }
        public int TipoClienteId { get; set; }
        public double Balance { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FechaNacimiento { get; set; }
        public int EstatusId { get; set; }
        public TipoCliente TipoCliente { get; set; }
        public Estatus Estatu { get; set; }

    }
}
