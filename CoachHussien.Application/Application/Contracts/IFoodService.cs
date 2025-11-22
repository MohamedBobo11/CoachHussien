using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoachHussien.Domain.Entities;

namespace CoachHussien.Application.Application.Contracts
{
    public interface IFoodService
    {
        Task<List<Food>> GetAllFoodsAsync();
        Task<List<Food>> GetFoodsByCategoryAsync(int categoryId);
        Task<Food> GetFoodByIdAsync(int id);
    }
}
