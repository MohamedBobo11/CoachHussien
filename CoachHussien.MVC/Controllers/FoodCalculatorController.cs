using CoachHussien.Application.Application.Contracts;
using CoachHussien.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoachHussien.MVC.Controllers
{
    public class FoodCalculatorController : Controller
    {
        private readonly IFoodService _foodService;

        public FoodCalculatorController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Default to Protein (Id = 1)
            var foods = await _foodService.GetFoodsByCategoryAsync(1);
            var model = new FoodCalculatorViewModel
            {
                Foods = new SelectList(foods, "Id", "Name"),
                SelectedMacro = "Protein"
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetFoodsByCategory(int categoryId)
        {
            var foods = await _foodService.GetFoodsByCategoryAsync(categoryId);
            return Json(foods.Select(f => new { id = f.Id, name = f.Name }));
        }

        [HttpPost]
        public async Task<IActionResult> Index(FoodCalculatorViewModel model)
        {
            // Reload foods based on selected macro
            int categoryId = 1; // Default Protein
            switch (model.SelectedMacro)
            {
                case "Protein": categoryId = 1; break;
                case "Carbohydrates": categoryId = 2; break;
                case "Fats": categoryId = 3; break;
            }

            var foods = await _foodService.GetFoodsByCategoryAsync(categoryId);
            model.Foods = new SelectList(foods, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var baseFood = await _foodService.GetFoodByIdAsync(model.BaseFoodId.Value);
            var altFood = await _foodService.GetFoodByIdAsync(model.AlternativeFoodId.Value);

            if (baseFood == null || altFood == null)
            {
                ModelState.AddModelError("", "عفوا، الطعام غير موجود.");
                return View(model);
            }

            model.BaseFoodDetails = baseFood;
            model.AlternativeFoodDetails = altFood;

            decimal baseMacroValue = 0;
            decimal altMacroValue = 0;

            switch (model.SelectedMacro)
            {
                case "Protein":
                    baseMacroValue = baseFood.Protein;
                    altMacroValue = altFood.Protein;
                    break;
                case "Carbohydrates":
                    baseMacroValue = baseFood.Carbs;
                    altMacroValue = altFood.Carbs;
                    break;
                case "Fats":
                    baseMacroValue = baseFood.Fat;
                    altMacroValue = altFood.Fat;
                    break;
                default:
                    baseMacroValue = baseFood.Protein;
                    altMacroValue = altFood.Protein;
                    break;
            }

            if (altMacroValue == 0)
            {
                ModelState.AddModelError("", "الطعام البديل لا يحتوي على العنصر الغذائي المختار، لا يمكن الحساب.");
                return View(model);
            }

            // Calculation: (Base Weight * Base Macro) / Alt Macro
            // Example: 100g Chicken (20g P) = Xg Beef (25g P) -> 100 * 20 / 25 = 80g
            // Wait, user request says: "الحسابه بتاعت الاكل و البديل بتاعه"
            // Usually: I want to replace 100g of BaseFood with AlternativeFood to get SAME amount of Macro.
            // Total Macro in Base = BaseWeight * (BaseMacro / 100)  (assuming macros are per 100g)
            // We want AltWeight * (AltMacro / 100) = Total Macro in Base
            // AltWeight = (BaseWeight * BaseMacro) / AltMacro
            
            // Note: The entities store macros per 100g (usually). 
            // If macros are per 100g, the formula is:
            // (BaseWeight / 100 * BaseMacro) = TotalMacro
            // (AltWeight / 100 * AltMacro) = TotalMacro
            // AltWeight = (BaseWeight * BaseMacro) / AltMacro
            
            model.ResultWeight = (model.BaseWeight * baseMacroValue) / altMacroValue;

            return View(model);
        }
    }
}
