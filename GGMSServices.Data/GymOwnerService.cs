namespace GGMSServices.Data
{
    using GGMS.Data;
    using GGMS.Data.Models;
    using GGMSServices.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GymOwnerService : IGymOwnerService
    {
        private readonly ApplicationDbContext data;

        public GymOwnerService( ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task BecomeGymOwner(Guid id)
        {
            GymOwner gymOwner = new GymOwner();

            gymOwner.Id = id;

            data.GymOwners.Add(gymOwner);

            await data.SaveChangesAsync();
        }

        public async Task<bool> IsGymOwner(Guid id)
        {
            return await data.GymOwners.AnyAsync(x => x.Id == id);
        }
    }
}
