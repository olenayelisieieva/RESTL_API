using BankOperations.API.Controllers.services;
using BankOperations.ApplicationService.Services;
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
        private readonly AccountsService _accountsService;


        private readonly ScopedService _scoped;
        private readonly TransientService _transietn;
        private readonly SingletonService _singleton;

        public AccountsController(
            AccountsService accountsService,
            ScopedService scoped,
            TransientService transietn,
            SingletonService singleton)
        {
            _accountsService = accountsService;
            _scoped = scoped;
            _transietn = transietn;
            _singleton = singleton;
        }




        // GET: api/<AccountsController>
        [HttpGet]
        public ActionResult<IEnumerable<AccountResponse>> GetAllAccounts()
        {
            var accounts = _accountsService.GetAllAccounts();

            if (accounts == null || !accounts.Any())
            {
                return BadRequest();
            }

            var response = new List<AccountResponse>();

            foreach (var item in accounts)
            {
                var accountResponse = new AccountResponse()
                {
                    AccountName = item.Name,
                    Id = item.Id,
                };

                response.Add(accountResponse);
            }


            return Ok(response);
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public ActionResult<AccountResponse> GetById([FromRoute]int id) 
        {
            var account = _accountsService.GetAccountById(id);

            var response = new AccountResponse()
            {
                AccountName = account.Name,
                Id = account.Id
            };

            return Ok(response);
        }
      


        [HttpPost()]
        public ActionResult<AccountResponse> CreateAccount([FromBody] AccountRequest accountRequest)
        {
            Console.WriteLine("START OF REQUEST");
            Console.WriteLine("SCOPED: " + _scoped.CurrentGuid.ToString());
            Console.WriteLine("TRANSIENT: " + _transietn.CurrentGuid.ToString());
            Console.WriteLine("SINGLETON: " + _singleton.CurrentGuid.ToString());


            var accountId = _accountsService.CreateAccount(accountRequest);

            var response = new AccountResponse()
            {
                Id = accountId,
                AccountName = accountRequest.AccountName
              
            };
            Console.WriteLine("END OF REQUEST");
            Console.WriteLine();
            return Ok(response);
        }


        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AccountRequest accountRequest)
        {
            _accountsService.UpdateAccount(id, accountRequest);
           
            return NoContent();
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteAccount(int id)
        {
            bool deleted = _accountsService.DeleteAccount(id);
            if (!deleted)
            {
                return NotFound($"Аккаунт с ID {id} не найден.");
            }
            return NoContent();
        }
    }
}
