
using Proyecto_Final.DAL.Entities;
using Proyecto_Final.DAL;
using Proyecto_Final.Enum;
using Proyecto_Final.Helpers;
using Proyecto_Final.Models;
using Proyecto_Final.Common;
using Azure;

namespace Proyecto_Final.Services
{
    public class TrainingUserHelper : ITrainingUserHelper
    {
        private readonly DataBaseContext _context;

        public TrainingUserHelper(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<Response2> ProcessTrainingUserAsync(AddTrainingUserViewModel addTrainingUserViewModel)
        {
            

            TrainingUser TrainingUser = new()
            {
                CreatedDate = DateTime.Now,
                User = addTrainingUserViewModel.User,
                Training = addTrainingUserViewModel.Training,
                ClassDate = addTrainingUserViewModel.DateClass,
                

            };

         
                
            

            _context.TrainingsUser.Add(TrainingUser);
            await _context.SaveChangesAsync();
            return TrainingUser;
        }

       

    }
}
