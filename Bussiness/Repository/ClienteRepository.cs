using Data.DBContext;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.StatusResult;
namespace Bussiness.Repository
{
    public class ClienteRepository 
    {
        private ClienteDbContext Db = null;
        public ClienteRepository() 
        {
            Db = new ClienteDbContext();
        }

        public async Task<List<Cliente>> GetAllResourcesAsync() 
        {
            return await Db.Clientes.ToListAsync();
        }
       
        public async Task<OperationResult<Object>> GetResourceByIdAsync(int Id) 
        {
            try
            {
                var data = await Db.Clientes
                    .Where(x => x.Id == Id)
                    .Select(x => new
                    {
                        Cliente = x,
                        TipoClienteDescripcion = x.TipoCliente.Descripcion,
                        EstatusDescripcion = x.Estatus.Descripcion
                    }).FirstOrDefaultAsync();

                return new OperationResult<Object>()
                {
                    Status = (int)(data == null ? StatusCode.NotFound : StatusCode.Success),
                    Message = (string)(data == null ? "The source was not found" : ""),
                    Data = data == null ? null : data
                };
            }
            catch (Exception E) 
            {
                return new OperationResult<Object>()
                {
                    Data = null,
                    Message = E.Message,
                    Status = (int)StatusCode.BadRequest
                };
            }
        }

        public async Task<OperationResult<Cliente>> CreateResourceAsync(Cliente Resource) 
        {
            try
            {
                Db.Entry(Resource).State = EntityState.Added;
                var Rows = await Db.SaveChangesAsync();
                return new OperationResult<Cliente>()
                {
                    Data = null,
                    Message = (string)(Rows > 0 ? "Resource added successfully" : ""),
                    Status = (int)(Rows > 0? StatusCode.Success : StatusCode.BadRequest)
                };
            }
            catch (Exception E) 
            {
                return new OperationResult<Cliente>()
                {
                    Message = E.Message,
                    Data = null,
                    Status = (int)StatusCode.BadRequest
                };
            }
        }

        public async Task<OperationResult<Cliente>> UpdateResourceAsync(Cliente Resource)
        {
            try
            {
                Db.Entry(Resource).State = EntityState.Modified;
                var Rows = await Db.SaveChangesAsync();
                return new OperationResult<Cliente>()
                {
                    Data = Resource,
                    Message = (string)(Rows > 0 ? "Resource updated successfully" : ""),
                    Status = (int)StatusCode.Success
                };
            }
            catch (Exception E)
            {
                return new OperationResult<Cliente>()
                {
                    Message = E.Message,
                    Data = null,
                    Status = (int)StatusCode.BadRequest
                };
            }
        }

        public async Task<OperationResult<Cliente>> DeleteResourceAsync(int Id)
        {
            try 
            {
                var data = await Db.Clientes.FirstOrDefaultAsync(x => x.Id == Id);
                Db.Entry(data).State = EntityState.Deleted;
                var Rows = await Db.SaveChangesAsync();
                return new OperationResult<Cliente>()
                {
                    Data = null,
                    Message = (string)(Rows > 0 ? "Resource deleted successfully" : ""),
                    Status = (int)(Rows > 0 ? StatusCode.Success : StatusCode.BadRequest)
                };
            } catch (Exception E) 
            {
                return new OperationResult<Cliente>()
                {
                    Data = null,
                    Message = E.Message,
                    Status = (int)StatusCode.BadRequest
                };
            }
        }

    }
}
