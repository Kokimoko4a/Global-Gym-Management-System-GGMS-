namespace GGMSServices.Data
{
    using GGMS.Data;
    using GGMS.Data.Models;
    using GGMS.Web.ViewModels.GymOwner;
    using GGMSServices.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
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

                    string fileName = Guid.NewGuid().ToString() + photo.FileName;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    sb.Append(fileName + ",");
                }
            }

            gym.PhotosPaths = sb.ToString();

            await data.AddAsync(gym);

            gymOwnerRecord.Gyms.Add(gym);

            await data.SaveChangesAsync();
        }

        public IEnumerable<GymSmallViewModel> GetAllGyms()
        {
            return data.Gyms.Select(x => new GymSmallViewModel()
            {
                Addrress = x.Address,
                PhotosPaths = x.PhotosPaths,
                Name = x.Name,
                Id = x.Id
            });
        }

        public GymBigViewModel GetGymAsBigViewModel(Guid id)
        {

            var gym = data.Gyms.Where(x => x.Id == id).Select(x => new GymBigViewModel()
            {
                Description = x.Description,
                Address = x.Address,
                Id = x.Id,
                PhotoPaths = x.PhotosPaths,
                Title = x.Name,
            }).First();

            return gym;

        }

        public async Task<GymQueryModel> GetGymsWithQueryModel(GymQueryModel gymQueryModel)
        {



            int recordsToSkip = Math.Abs((gymQueryModel.CurrentPage - 1) * 6);

            if (!string.IsNullOrEmpty(gymQueryModel.KeyWords))
            {

                List<Gym> gymsFromDbFiltered = await data.Gyms.
                    Where(x => x.Description.Contains(gymQueryModel.KeyWords) || x.Name.Contains(gymQueryModel.KeyWords) || x.Address.Contains(gymQueryModel.KeyWords)).Skip(recordsToSkip)
                    .ToListAsync();

                gymQueryModel.GymSmallViewModels = gymsFromDbFiltered.Select(x => new GymSmallViewModel()
                {
                    Addrress = x.Address,
                    Name = x.Name,
                    PhotosPaths = x.PhotosPaths,
                    Id = x.Id
                }).ToHashSet();

                return gymQueryModel;

            }


            List<Gym> gymsFromDb = await data.Gyms.Skip(recordsToSkip).Take(gymQueryModel.GymsPerPage).ToListAsync();



            gymQueryModel.GymSmallViewModels = gymsFromDb.Select(x => new GymSmallViewModel()
            {
                Addrress = x.Address,
                Name = x.Name,
                PhotosPaths = x.PhotosPaths,
                Id = x.Id
            }).ToHashSet();

            return gymQueryModel;

        }

        public async Task<string[]> CreateGymCard(Guid userId, Guid gymId)
        {
            string[] output = new string[5];

            ApplicationUser applicationUser = await data.Users.FirstOrDefaultAsync(x => x.Id == userId)!;

            Gym gym = await data.Gyms.FirstOrDefaultAsync(x => x.Id == gymId)!;

            FitnessCard fitnessCard = new FitnessCard();

            fitnessCard.UserId = userId;
            fitnessCard.User = applicationUser;
            fitnessCard.Gym = gym;
            fitnessCard.GymId = gymId;
            fitnessCard.ExpiringDate = DateTime.UtcNow.AddDays(30);

           await data.AddAsync(fitnessCard);
            await data.SaveChangesAsync();

            gym.FitnessCards.Add(fitnessCard);
            await data.SaveChangesAsync();

            output[0] = applicationUser!.FirstName;
            output[1] = applicationUser.LastName;
            output[2] = gym.Name;
            output[3] = fitnessCard.IssueDate.ToString();
            output[4] = fitnessCard.ExpiringDate.ToString();


            return output;
        }
    }
}
