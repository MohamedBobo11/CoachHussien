using CoachHussien.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoachHussien.MVC.Models
{
    public class FoodCalculatorViewModel
    {
        public int BaseFoodId { get; set; }
        public int AlternativeFoodId { get; set; }
        public decimal BaseWeight { get; set; }
        public string SelectedMacro { get; set; } = "Protein"; // Default

        public SelectList? Foods { get; set; }
        
        public decimal? ResultWeight { get; set; }
        public Food? BaseFoodDetails { get; set; }
        public Food? AlternativeFoodDetails { get; set; }
    }
}
