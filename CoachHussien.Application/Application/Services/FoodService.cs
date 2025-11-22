using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoachHussien.Domain.Entities;
using CoachHussien.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using CoachHussien.Application.Application.Contracts;

namespace CoachHussien.Application.Application.Services
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext _context;

        public FoodService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Food>> GetAllFoodsAsync()
        {
            return await _context.Foods.OrderBy(f => f.Name).ToListAsync();
        }

        public async Task<List<Food>> GetFoodsByCategoryAsync(int categoryId)
        {
            return await _context.Foods.Where(f => f.CategoryId == categoryId).OrderBy(f => f.Name).ToListAsync();
        }

        public async Task<Food> GetFoodByIdAsync(int id)
        {
            return await _context.Foods.FindAsync(id);
        }
    }
}
