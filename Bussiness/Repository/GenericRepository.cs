using Data.DBContext;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Bussiness.StatusResult;
using System.Threading.Tasks;

namespace Bussiness.Repository
{
    public class GenericRepository<T> where T : TableBase
    {
        private ClienteDbContext clienteDbContext = null;
        private DbSet<T> Table;
        public GenericRepository() 
        {
            clienteDbContext = new ClienteDbContext();
            Table = clienteDbContext.Set<T>();
        }

        public async System.Threading.Tasks.Task<List<T>> GetAllResourcesAsync() 
        {
            return await Table.ToListAsync();
        }

        public async Task<OperationResult<T>> GetResourceByIdAsync(int Id) 
        {
            try
            {
                var data = await Table.FirstOrDefaultAsync(x => x.Id == Id);

                return new OperationResult<T>()
                {
                    Status = (int)(data == null ? StatusCode.NotFound : StatusCode.Success),
                    Message = (string)(data == null ? "The source was not found" : ""),
                    Data = data == null ? null : data
                };
            }
            catch (Exception E) 
            {
                return new OperationResult<T>()
                {
                    Data = null,
                    Message = E.Message,
                    Status = (int)StatusCode.BadRequest
                };
            }
        }

        public async Task<OperationResult<T>> CreateResourceAsync(T Resource) 
        {
            try
            {
                clienteDbContext.Entry(Resource).State = EntityState.Added;
                var Rows = await clienteDbContext.SaveChangesAsync();
                return new OperationResult<T>()
                {
                    Data = null,
                    Message = (string)(Rows > 0 ? "Resource added successfully" : ""),
                    Status = (int)(Rows > 0? StatusCode.Success : StatusCode.BadRequest)
                };
            }
            catch (Exception E) 
            {
                return new OperationResult<T>()
                {
                    Message = E.Message,
                    Data = null,
                    Status = (int) StatusCode.BadRequest
                };
            }
        }

        public async Task<OperationResult<T>> UpdateResourceAsync(T Resource)
        {
            try
            {
                clienteDbContext.Entry(Resource).State = EntityState.Modified;
                var Rows = await clienteDbContext.SaveChangesAsync();
                return new OperationResult<T>()
                {
                    Data = Resource,
                    Message = (string)(Rows > 0 ? "Resource updated successfully" : ""),
                    Status = (int)StatusCode.Success
                };
            }
            catch (Exception E)
            {
                return new OperationResult<T>()
                {
                    Message = E.Message,
                    Data = null,
                    Status = (int)StatusCode.BadRequest
                };
            }
        }

        public async Task<OperationResult<T>> DeleteResourceAsync(int Id)
        {
            try 
            {
                var data = await Table.FirstOrDefaultAsync(x => x.Id == Id);
                clienteDbContext.Entry(data).State = EntityState.Deleted;
                var Rows = await clienteDbContext.SaveChangesAsync();
                return new OperationResult<T>()
                {
                    Data = null,
                    Message = (string)(Rows > 0 ? "Resource deleted successfully" : ""),
                    Status = (int)(Rows > 0 ? StatusCode.Success : StatusCode.BadRequest)
                };
            } catch (Exception E) 
            {
                return new OperationResult<T>()
                {
                    Data = null,
                    Message = E.Message,
                    Status = (int)StatusCode.BadRequest
                };
            }
        }

    }
}
