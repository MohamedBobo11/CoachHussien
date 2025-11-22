using CoachHussien.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CoachHussien.MVC.Models
{
    public class FoodCalculatorViewModel
    {
        [Required(ErrorMessage = "يرجى اختيار الطعام الأساسي")]
        public int? BaseFoodId { get; set; }

        [Required(ErrorMessage = "يرجى اختيار الطعام البديل")]
        public int? AlternativeFoodId { get; set; }

        [Required(ErrorMessage = "يرجى إدخال الوزن")]
        [Range(0.01, 10000, ErrorMessage = "الوزن يجب أن يكون أكبر من 0")]
        public decimal? BaseWeight { get; set; }

        public string SelectedMacro { get; set; } = "Protein"; // Default

        public SelectList? Foods { get; set; }
        
        public decimal? ResultWeight { get; set; }
        public Food? BaseFoodDetails { get; set; }
        public Food? AlternativeFoodDetails { get; set; }
    }
}
