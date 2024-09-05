namespace GGMSServices.Data
{
    using GGMS.Data;
    using GGMS.Data.Models;
    using GGMS.Web.ViewModels.GymOwner;
    using GGMSServices.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Text;

    public class GymOwnerService : IGymOwnerService
    {
        private readonly ApplicationDbContext data;

        public GymOwnerService(ApplicationDbContext data)
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

        public async Task CreateGym(GymFormModel formModel, Guid creatorId)
        {
            StringBuilder sb = new StringBuilder();

            ApplicationUser user = data.Users.FirstOrDefault(x => x.Id == creatorId)!;
            GymOwner gymOwnerRecord = data.GymOwners.FirstOrDefault(x => x.Id == creatorId)!;


            Gym gym = new Gym();

            gym.Address = formModel.Address;
            gym.Name = formModel.Name;
            gym.Description = formModel.Description;

            foreach (var photo in formModel.Photos)
            {
                if (photo != null && photo.Length > 0)
                {

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", photo.FileName + Guid.NewGuid().ToString());

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    sb.Append(filePath + ",");
                }
            }

            gym.PhotosPaths = sb.ToString();

            await data.AddAsync(gym);

            gymOwnerRecord.Gyms.Add(gym);

            await data.SaveChangesAsync();
        }
    }
}
