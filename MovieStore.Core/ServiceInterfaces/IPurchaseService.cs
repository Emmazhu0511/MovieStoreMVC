using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IPurchaseService
    {
        Task<bool> IsMoviePurchased(int userId, int MovieId);
        Task<Purchase> PlaceOrder(UserPurchaseRequestModel userPurchaseRequestModel);

        Task<IEnumerable<Movie>> GetPurchases(int id);

        


    }
}
