using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;
        private readonly PizzaService _service;
        public IList<Pizza> PizzaList { get; set; } = default!;

        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }

        public IActionResult OnPost()
        {
            //驗證規則是從 Models\Pizza.cs 中 Pizza 類別上的屬性 (例如 Required 和 Range) 推斷而來。
            //如果使用者的輸入無效，則會呼叫 Page 方法來重新轉譯頁面。
            if (!ModelState.IsValid || NewPizza == null)
            {
                return Page();
            }

            _service.AddPizza(NewPizza);

            //RedirectToAction 方法是用來將使用者重新導向至 Get 頁面處理常式，這會使用更新的披薩清單重新轉譯頁面。
            return RedirectToAction("Get");
        }
        public IActionResult OnPostDelete(int id)
        {
            _service.DeletePizza(id);

            return RedirectToAction("Get");
        }
    }
}
