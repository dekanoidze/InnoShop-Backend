using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.UserService.Application.Services
{
    public interface IProductServiceClient
    {
        Task HideUserProductsAsync(Guid userId);
        Task ShowUserProductsAsync(Guid userId);
    }
}
