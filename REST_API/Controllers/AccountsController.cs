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
            var accounts = AccountsService.GetAllAccounts();

            if (accounts == null || !accounts.Any())
            {
                return null;
            }


            return new string[] { "OK" };
        
            //return new string[] { "OK" };
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
      


        [HttpPost()]
        public AccounResponse CreateAccount([FromBody] AccountRequest accountRequest)
        {

            var accountId = AccountsService.CreateAccount(accountRequest);

            var response = new AccounResponse()
            {
                Id = accountId
            };

            return response;
        }


        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] AccountRequest accountRequest)
        {
            AccountsService.UpdateAccount(id, accountRequest);

            return $" Account Updated";
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        
           public IActionResult DeleteAccount(int id)
        {
            bool deleted = AccountsRepository.DeleteAccount(id);
            if (!deleted)
            {
                return NotFound($"Аккаунт с ID {id} не найден.");
            }
            return NoContent();
        }
    }
}
