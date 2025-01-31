using BankOperations.ApplicationService.Services;
using BankOperations.Context.Repositories;
using BankOperations.Infrastructure.Entities;
using BankOperations.Shared.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using REST_API.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        // GET: api/<AccountsController>
        [HttpGet]
        public IEnumerable<string> GetAllAccounts()
        {

            // TODO: Подключить сервис
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public AccounResponse GetAccountById([FromRoute] int id)
        {
            // TODO: Подключить сервис

            var response = new AccounResponse()
            {
                AccountId = Guid.NewGuid(),
                Name = "TestName",
            };
            return response;
        }


        [HttpPost()]
        public string CreateAccount([FromBody] AccountRequest accountRequest)
        {

            AccountsService.CreateAccount(accountRequest);

            return "New Account Created";
        }


        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] AccountRequest accountRequest)
        {
            return $" Account Updated";
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {

            // TODO: подключить сервис
            return "Account deleted";
        }
    }
}
