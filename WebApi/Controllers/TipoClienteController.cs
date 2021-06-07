using Bussiness.Repository;
using Bussiness.StatusResult;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class TipoClienteController : ApiController
    {
        public readonly GenericRepository<TipoCliente> genericRepository;
        public TipoClienteController() 
        {
            genericRepository = new GenericRepository<TipoCliente>();
        }

        [HttpGet]
        public async Task<List<TipoCliente>> GetClients()
        {
            return await genericRepository.GetAllResourcesAsync();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetClientById(int id)
        {
            OperationResult<TipoCliente> OResult = await genericRepository.GetResourceByIdAsync(id);
            if (OResult.Status == (int)Bussiness.StatusResult.StatusCode.Success)
            {
                return Ok(OResult);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateClient([FromBody] TipoCliente TipoCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                OperationResult<TipoCliente> result = await genericRepository.CreateResourceAsync(TipoCliente);
                if (result.Status == (int)Bussiness.StatusResult.StatusCode.BadRequest)
                {
                    return BadRequest();
                }
                return Ok(result);
            }

        }

        [HttpPut]
        public async System.Threading.Tasks.Task<IHttpActionResult> UpdateClient(int Id, [FromBody] TipoCliente TipoCliente)
        {
            if (TipoCliente.Id == Id)
            {
                OperationResult<TipoCliente> result = await genericRepository.UpdateResourceAsync(TipoCliente);
                if (result.Status == (int)Bussiness.StatusResult.StatusCode.Success)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteClient(int Id)
        {
            OperationResult<TipoCliente> result = await genericRepository.DeleteResourceAsync(Id);
            if (result.Status == (int)Bussiness.StatusResult.StatusCode.Success)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
