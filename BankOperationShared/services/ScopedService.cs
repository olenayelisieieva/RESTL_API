namespace BankOperations.API.Controllers.services
{
    public class ScopedService
    {
        public Guid CurrentGuid { get; set; }
        public ScopedService()
        {
            CurrentGuid = Guid.NewGuid();
        }
    }
}
