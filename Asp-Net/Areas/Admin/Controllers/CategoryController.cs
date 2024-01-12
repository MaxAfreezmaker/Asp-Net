using Asp_Net.DataAcess;
using Asp_Net.DataAcess.Repository.IRepository;
using Asp_Net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Asp_Net.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IJednostka _unitOfWork;

        public CategoryController(IJednostka unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Kategoria> listaKategorii = _unitOfWork.KategoriRI.GetAll();

            return View(listaKategorii);
        }

        public IActionResult Tworzenie()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Tworzenie(Kategoria element)
        {
            if (element.Nazwa == element.KolejnoscWyswietlania.ToString())
            {
                ModelState.AddModelError("name", "Kolejność wyświetlania nie może dokładnie odpowiadać nazwie");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.KategoriRI.Add(element);
                _unitOfWork.Save();
                TempData["success"] = "Kategoria została pomyślnie utworzona";
                return RedirectToAction("Index");
            }
            return View(element);
        }

        public IActionResult Edycja(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CategoryFromDbFirst = _unitOfWork.KategoriRI.GetFirstOrDefault(x => x.Id == id);
            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(CategoryFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edycja(Kategoria element)
        {
            if (element.Nazwa == element.KolejnoscWyswietlania.ToString())
            {
                ModelState.AddModelError("Nazwa", "Kolejność wyświetlania nie może dokładnie odpowiadać nazwie");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.KategoriRI.Update(element);
                _unitOfWork.Save();
                TempData["success"] = "Kategoria została pomyślnie edytowana";
                return RedirectToAction("Index");
            }
            return View(element);
        }

        public IActionResult Usun(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryFromDb = _unitOfWork.KategoriRI.GetFirstOrDefault(x => x.Id == id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }

            return View(CategoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var element = _unitOfWork.KategoriRI.GetFirstOrDefault(x => x.Id == id);
            if (element == null)
            {
                return NotFound();
            }
            _unitOfWork.KategoriRI.Remove(element);
            _unitOfWork.Save();
            TempData["success"] = "Kategoria została pomyślnie usunięta";
            return RedirectToAction("Index");
        }
    }
}
