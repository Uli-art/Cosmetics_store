using Microsoft.AspNetCore.Mvc;

namespace WEB_153502_Sidorova.Views.Home.Components
{
    public class Cart : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Cart");
        }
    }
}
