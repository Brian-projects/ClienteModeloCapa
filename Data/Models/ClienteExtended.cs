using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ClienteExtended 
    {
        public Cliente cliente { get; set; }
        public string TipoClienteDescripcion { get; set; }
        public string EstatusDescripcion { get; set; }
    }
}
