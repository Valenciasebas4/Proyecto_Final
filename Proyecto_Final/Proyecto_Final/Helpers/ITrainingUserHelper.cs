
using Proyecto_Final.Common;
using Proyecto_Final.Models;



namespace Proyecto_Final.Helpers
{
    public interface ITrainingUserHelper
    {
        Task<Response2> ProcessTrainingUserAsync(AddTrainingUserViewModel addTrainingUserViewModel);
    }
}
