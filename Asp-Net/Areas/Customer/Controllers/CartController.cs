using Asp_Net.DataAcess.Repository.IRepository;
using Asp_Net.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Asp_Net.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IJednostka _unitOfWork;

        public ViewModelKoszyk Koszyk { get; set;}
        public CartController(IJednostka unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            Koszyk = new ViewModelKoszyk()
            {
                Koszyczek = _unitOfWork.KoszykRI.GetAll(u=>u.IdUzytkownik==claim.Value,
                includeProperties:  "Produkt" ),
            };
            foreach (var cart in Koszyk.Koszyczek)
            {
                cart.Cena = getPrice(cart.Ilosc, cart.Produkt.Cena, cart.Produkt.Od50, cart.Produkt.Od100);

                Koszyk.Total += (cart.Cena * cart.Ilosc);
            }


            return View(Koszyk);
        }
        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.KoszykRI.GetFirstOrDefault(u=>u.Id==cartId);
            _unitOfWork.KoszykRI.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Minus(int cartId)
        {           
            var cart = _unitOfWork.KoszykRI.GetFirstOrDefault(u => u.Id == cartId);

            if (cart.Ilosc <= 1)
            {
                _unitOfWork.KoszykRI.Remove(cart);
            }
            else
            {
                _unitOfWork.KoszykRI.DecrementCount(cart, 1);               
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.KoszykRI.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.KoszykRI.Remove(cart);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        private double getPrice(double quantity, double price, double price50, double price100)
        {
            if(quantity <= 50)
            {
                return price;
            }
            else
            {
                if(quantity <= 100)
                {
                    return price50;
                }
                return price100;
            }
        }
    }
}
