using Microsoft.AspNetCore.Mvc;
using WebCourseWorkActual.Domain.ViewModels.CheckIncomeCategory;
using WebCourseWorkActual.Domain.ViewModels.IncomeCategory;
using WebCourseWorkActual.Service.Interfaces;

namespace WebCourseWorkActual.Controllers
{
    public class IncomeCategoryController : Controller
    {
        private readonly IIncomeCategoryService _incomeCategoryService;
        private readonly ICheckIncomeCategoryService _checkIncomeCategoryService;

        public IncomeCategoryController(IIncomeCategoryService incomeCategoryService, ICheckIncomeCategoryService checkIncomeCategoryService)
        {
            _incomeCategoryService = incomeCategoryService;
            _checkIncomeCategoryService = checkIncomeCategoryService;
        }

        [HttpGet]
        public IActionResult AddNewIncomeCategory() => View();

        [HttpPost]
        public async Task<IActionResult> AddNewIncomeCategory(NewIncomeCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _incomeCategoryService.Create(model, int.Parse(User.Identity.Name));
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    var response2 = await _checkIncomeCategoryService.Create(new CheckIncomeCategoryViewModel { IdСчёта = int.Parse(User.Identity.Name), IdКатегорииДохода = response.Data.Id });
                    if (response2.StatusCode == Domain.Enum.StatusCode.OK)
                        return RedirectToAction("GetIncomeCategories", "IncomeCategory");
                    ModelState.AddModelError("", response2.Description);
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomeCategories()
        {
            var response = await _incomeCategoryService.GetIncomeCategories(int.Parse(User.Identity.Name));
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIncomeCategory(string name)
        {

            var response = await _incomeCategoryService.DeleteIncomeCategory(name, int.Parse(User.Identity.Name));
            return View();
        }

        [HttpGet]
        public IActionResult ChangeIncomeCategory(string oldName)
        {
            ChangeIncomeCategoryViewModel model = new ChangeIncomeCategoryViewModel { Name = oldName };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeIncomeCategory(ChangeIncomeCategoryViewModel model, string oldName)
        {
            if (ModelState.IsValid)
            {
                var response = await _incomeCategoryService.ChangeIncomeCategory(model, int.Parse(User.Identity.Name), oldName);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetIncomeCategories", "IncomeCategory");
                    //var response2 = await _incomeCategoryService.Update(new CheckIncomeCategoryViewModel { IdСчёта = int.Parse(User.Identity.Name), IdКатегорииРасхода = response.Data.Id });
                    //if (response2.StatusCode == Domain.Enum.StatusCode.OK)
                    //    return RedirectToAction("GetIncomeCategories", "IncomeCategory");
                    ModelState.AddModelError("", response.Description);
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
            //var response = await _incomeCategoryService.ChangeIncomeCategory(name, int.Parse(User.Identity.Name));
            //return View();
        }

        //public async Task<Check> ChangeBalance(decimal newValue)
        //{
        //    int userId = int.Parse(User.Identity.Name);
        //    var response = await _checkService.GetCheck(userId);
        //    Check c = response;
        //    c.Баланс = newValue;
        //    return await _checkService.ChangeBalance(c);
        //    //return View(newResponse);//response.Data);
        //}
    }
}
