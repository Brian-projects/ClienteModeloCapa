using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    class TipoCliente : TableBase
    {
        [Required,StringLength(maximumLength: 200)]
        public string Descripcion { get; set; }
        public int  EstatusId { get; set; }
        public ICollection<Cliente> Clientes { get; set; }
        public Estatus Estatus { get; set; }
    }
}
