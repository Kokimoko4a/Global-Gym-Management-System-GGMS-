namespace GGMSServices.Data.Interfaces
{
    public interface IGymOwnerService
    {
        public Task<bool> IsGymOwner(Guid id);

        public Task BecomeGymOwner(Guid id);
            
    }
}
