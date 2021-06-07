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
using System.Text;

namespace WebApi.Controllers
{
    public class ClienteController : ApiController
    {
        private readonly GenericRepository<Cliente> genericRepository;
        private HttpResponseMessage response;
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
        public async Task<HttpResponseMessage> GetClientById(int id)
        {
            
            OperationResult<Cliente> OResult = await genericRepository.GetResourceByIdAsync(id);
            if (OResult.Status == (int)Bussiness.StatusResult.StatusCode.Success) 
            {
                response = Request.CreateResponse(HttpStatusCode.OK, OResult);
                return response;
            }
            response = Request.CreateResponse(HttpStatusCode.NotFound,OResult);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateClient ([FromBody] Cliente cliente) 
        {
            if (!ModelState.IsValid) 
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest,ModelState);
                return response;
            }
            else
            {
                OperationResult<Cliente> result = await genericRepository.CreateResourceAsync(cliente);
                if (result.Status == (int) Bussiness.StatusResult.StatusCode.BadRequest) 
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    return response;
                }
                response = Request.CreateResponse(HttpStatusCode.OK,result);
                return response;
            }
            
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateClient(int Id, [FromBody] Cliente cliente) 
        {
            if (cliente.Id == Id) 
            {
                OperationResult<Cliente> result = await genericRepository.UpdateResourceAsync(cliente);
                if (result.Status == (int) Bussiness.StatusResult.StatusCode.Success) 
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, result);
                    return response;
                }

                response = Request.CreateResponse(HttpStatusCode.BadRequest, result);
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteClient (int Id) 
        {
            OperationResult<Cliente> result = await genericRepository.DeleteResourceAsync(Id);
            if (result.Status == (int) Bussiness.StatusResult.StatusCode.Success) 
            {
                response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
            response = Request.CreateResponse(HttpStatusCode.NotFound, result);
            return response;
        }
       
    }
}
