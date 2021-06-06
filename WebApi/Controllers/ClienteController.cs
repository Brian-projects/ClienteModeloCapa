using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data.Models;
using Bussiness.Repository;
using System.Threading.Tasks;
using Bussiness.StatusResult;
namespace WebApi.Controllers
{
    public class ClienteController : ApiController
    {
        private readonly GenericRepository<Cliente> genericRepository;
        public ClienteController() 
        {
            genericRepository = new GenericRepository<Cliente>();
        }
        [HttpGet]
        public async Task<List<Cliente>> GetClients() 
        {
            return await genericRepository.GetAllResourcesAsync();
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> GetClientById(int id)
        {
            OperationResult<Cliente> OResult = await genericRepository.GetResourceByIdAsync(id);
            if (OResult.Status == (int)Bussiness.StatusResult.StatusCode.Success) 
            {
                return Ok(OResult);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateClient ([FromBody] Cliente cliente) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            else
            {
                OperationResult<Cliente> result = await genericRepository.CreateResourceAsync(cliente);
                if (result.Status == (int) Bussiness.StatusResult.StatusCode.BadRequest) 
                {   
                    return BadRequest();
                }
                return Ok(result);
            }
            
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateClient(int Id, [FromBody] Cliente cliente) 
        {
            if (cliente.Id == Id) 
            {
                OperationResult<Cliente> result = await genericRepository.UpdateResourceAsync(cliente);
                if (result.Status == (int) Bussiness.StatusResult.StatusCode.Success) 
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteClient (int Id) 
        {
            OperationResult<Cliente> result = await genericRepository.DeleteResourceAsync(Id);
            if (result.Status == (int) Bussiness.StatusResult.StatusCode.Success) 
            {
                return Ok(result);
            }

            return NotFound();
        }
       
    }
}
