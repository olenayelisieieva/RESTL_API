using BankOperations.ApplicationService.Services;
using BankOperations.Context.Repositories;
using BankOperations.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using REST_API.Requests;
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
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public AccounResponse GetAccountById([FromRoute] int id)
        {
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


        [HttpPost("{id}")]
        public string GetAccountsByFilters(
            [FromQuery] string[] accountNames,
            [FromBody] AccountRequest accountRequest,
            [FromRoute] int id)
        {
            return $"New Account  Created";
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
            return "Account deleted";
        }
    }
}
