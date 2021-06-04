using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    class Estatus : TableBase
    {
        public string Descripcion { get; set; }
        public bool Estatu { get; set; }
        public ICollection<Cliente> Clientes { get; set; }
        public ICollection<TipoCliente> TipoClientes { get; set; }

    }
}
