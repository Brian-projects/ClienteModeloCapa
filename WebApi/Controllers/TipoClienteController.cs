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
        public readonly TipoClienteRepository tipoClienteRepository;
        public TipoClienteController() 
        {
            tipoClienteRepository = new TipoClienteRepository();
        }

        [HttpGet]
        public async Task<List<TipoCliente>> GetClients()
        {
            return await tipoClienteRepository.GetAllResourcesAsync();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetClientById(int id)
        {
            OperationResult<Object> OResult = await tipoClienteRepository.GetResourceByIdAsync(id);
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
                OperationResult<TipoCliente> result = await tipoClienteRepository.CreateResourceAsync(TipoCliente);
                if (result.Status == (int)Bussiness.StatusResult.StatusCode.BadRequest)
                {
                    return BadRequest();
                }
                return Ok(result);
            }

        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateClient(int Id, [FromBody] TipoCliente TipoCliente)
        {
            if (TipoCliente.Id == Id)
            {
                OperationResult<TipoCliente> result = await tipoClienteRepository.UpdateResourceAsync(TipoCliente);
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
            OperationResult<TipoCliente> result = await tipoClienteRepository.DeleteResourceAsync(Id);
            if (result.Status == (int)Bussiness.StatusResult.StatusCode.Success)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
