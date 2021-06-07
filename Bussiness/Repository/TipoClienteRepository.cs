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
    public class TipoClienteRepository 
    {
        private ClienteDbContext Db = null;
        public TipoClienteRepository() 
        {
            Db = new ClienteDbContext();
        }

        public async Task<List<TipoCliente>> GetAllResourcesAsync() 
        {
            return await Db.TipoClientes.ToListAsync();
        }
       
        public async Task<OperationResult<Object>> GetResourceByIdAsync(int Id) 
        {
            try
            {
                var data = await Db.TipoClientes.
                    Where(x => x.Id == Id)
                    .Select(x => new
                    {
                        TipoCliente = x,
                        EstatusDescripcion = x.Estatus.Descripcion
                    })
                    .FirstOrDefaultAsync();

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

        public async Task<OperationResult<TipoCliente>> CreateResourceAsync(TipoCliente Resource) 
        {
            try
            {
                Db.Entry(Resource).State = EntityState.Added;
                var Rows = await Db.SaveChangesAsync();
                return new OperationResult<TipoCliente>()
                {
                    Data = null,
                    Message = (string)(Rows > 0 ? "Resource added successfully" : ""),
                    Status = (int)(Rows > 0? StatusCode.Success : StatusCode.BadRequest)
                };
            }
            catch (Exception E) 
            {
                return new OperationResult<TipoCliente>()
                {
                    Message = E.Message,
                    Data = null,
                    Status = (int)StatusCode.BadRequest
                };
            }
        }

        public async Task<OperationResult<TipoCliente>> UpdateResourceAsync(TipoCliente Resource)
        {
            try
            {
                Db.Entry(Resource).State = EntityState.Modified;
                var Rows = await Db.SaveChangesAsync();
                return new OperationResult<TipoCliente>()
                {
                    Data = Resource,
                    Message = (string)(Rows > 0 ? "Resource updated successfully" : ""),
                    Status = (int)StatusCode.Success
                };
            }
            catch (Exception E)
            {
                return new OperationResult<TipoCliente>()
                {
                    Message = E.Message,
                    Data = null,
                    Status = (int)StatusCode.BadRequest
                };
            }
        }

        public async Task<OperationResult<TipoCliente>> DeleteResourceAsync(int Id)
        {
            try 
            {
                var data = await Db.TipoClientes.FirstOrDefaultAsync(x => x.Id == Id);
                Db.Entry(data).State = EntityState.Deleted;
                var Rows = await Db.SaveChangesAsync();
                return new OperationResult<TipoCliente>()
                {
                    Data = null,
                    Message = (string)(Rows > 0 ? "Resource deleted successfully" : ""),
                    Status = (int)(Rows > 0 ? StatusCode.Success : StatusCode.BadRequest)
                };
            } catch (Exception E) 
            {
                return new OperationResult<TipoCliente>()
                {
                    Data = null,
                    Message = E.Message,
                    Status = (int)StatusCode.BadRequest
                };
            }
        }

    }
}
