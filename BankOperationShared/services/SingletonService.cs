namespace BankOperations.API.Controllers.services
{
    public class SingletonService
    {
        public Guid CurrentGuid { get; set; }
        public SingletonService()
        {
            CurrentGuid = Guid.NewGuid();
        }
    }
}
