using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class TipoCliente : TableBase
    {
        public string Descripcion { get; set; }
        public ICollection<Cliente> Clientes { get; set; }
        public Estatus Estatus { get; set; }
    }
}
