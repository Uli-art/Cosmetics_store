using Microsoft.AspNetCore.Mvc;
using WEB_153502_Sidorova.Models;
namespace WEB_153502_Sidorova.Controllers
{
    public class Home : Controller
    {
        List<ListDemo> SelectList;

        public IActionResult Index()
        {
            ViewData["NumberOfLab"] = "Лабораторная работа 2";
            SelectList = new List<ListDemo> {
                new ListDemo{Id = 1, Name = "Item 1"},
                new ListDemo{Id = 2, Name = "Item 2"},
                new ListDemo{Id = 3, Name = "Item 3"},
                new ListDemo{Id = 4, Name = "Item 4"},
                new ListDemo{Id = 5, Name = "Item 5"}
            };
            return View(SelectList);  
        }
    }
}
