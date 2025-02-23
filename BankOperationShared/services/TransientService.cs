namespace BankOperations.API.Controllers.services
{
    public class TransientService
    {
        public Guid CurrentGuid { get; set; }
        public TransientService()
        {
            CurrentGuid = Guid.NewGuid();
        }
    }
}
