using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    class Estatus
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool State { get; set; }
        public ICollection<TipoCliente> tipoClientes { get; set; }
        public ICollection<Cliente> Clientes { get; set; }
    }
}
