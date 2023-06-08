
using Proyecto_Final.Common;
using Proyecto_Final.Models;



namespace Proyecto_Final.Helpers
{
    public interface IOrderHelper
    {
        Task<Response> ProcessOrderAsync(ShowCartViewModel showCartViewModel);
        Task<Response> CancelOrderAsync(Guid orderId);
    }
}
