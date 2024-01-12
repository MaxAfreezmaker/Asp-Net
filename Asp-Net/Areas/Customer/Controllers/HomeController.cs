using Asp_Net.DataAcess.Repository.IRepository;
using Asp_Net.Models;
using Asp_Net.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Asp_Net.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJednostka _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IJednostka unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Produkt> productList = _unitOfWork.ProduktRI.GetAll(includeProperties:"Kategoria");
            return View(productList);
        }

        public IActionResult Details(int productId)
        {
            Koszyk cartItem = new()
            {
                Ilosc = 1,
                ProduktId = productId,
                Produkt = _unitOfWork.ProduktRI.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Kategoria"),
            };
            return View(cartItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(Koszyk shoppingCart)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.IdUzytkownik = claim.Value;

            Koszyk cartDb = _unitOfWork.KoszykRI.GetFirstOrDefault(
                u => u.IdUzytkownik == claim.Value && u.Produkt == shoppingCart.Produkt);

            if(cartDb == null)
            {
                _unitOfWork.KoszykRI.Add(shoppingCart);
            }
            else
            {
                _unitOfWork.KoszykRI.IncrementCount(cartDb, shoppingCart.Ilosc);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}