
using Asp_Net.DataAcess;
using Asp_Net.DataAcess.Repository.IRepository;
using Asp_Net.Models;
using Asp_Net.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp_Net.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IJednostka _unitOfWork;
        private readonly IWebHostEnvironment _HostEnviroment;

        public ProductController(IJednostka unitOfWork, IWebHostEnvironment hostEnviroment)
        {
            _unitOfWork = unitOfWork;
            _HostEnviroment = hostEnviroment;
        }

        public IActionResult Index()
        {
            
                var productList = _unitOfWork.ProduktRI.GetAll(includeProperties: "Kategoria");
                

            foreach (var produkt in productList)
            {
                Console.WriteLine($"Nazwa: {produkt.Nazwa}");
            }
            return View(productList);

        }

        public IActionResult Upsert(int? id)
        {
            
            
                ViewModelProdukt productVM = new()
                {
                    Produkt = new(),
                    listaKategorii = _unitOfWork.KategoriRI.GetAll().Select(i => new SelectListItem
                    {
                        Text = i.Nazwa,
                        Value = i.Id.ToString()
                    })
                };

                if (id == null || id == 0)
                {
                    return View(productVM);
                }
                else
                {
                    productVM.Produkt = _unitOfWork.ProduktRI.GetFirstOrDefault(u => u.Id == id);
                    return View(productVM);
                }
            
            

        }
        [HttpGet]
        public IActionResult CheckIfExists(int productId)
        {
            var exists = _unitOfWork.ProduktRI.GetFirstOrDefault(u => u.Id == productId) != null;
            return Json(new { exists = exists });
        }


        public void UpdateProduct(ViewModelProdukt item)
        {
            _unitOfWork.ProduktRI.Update(item.Produkt);
            _unitOfWork.Save();
        }


        public void AddNewProduct(ViewModelProdukt item)
        {
            _unitOfWork.ProduktRI.Add(item.Produkt);
            _unitOfWork.Save();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ViewModelProdukt item, IFormFile file)
        {
            Console.WriteLine("Akcja Upsert została wywołana.");
            if (ModelState.IsValid)
            {
                string rootPath = _HostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(rootPath, @"img\produkty");
                    var newPath = Path.GetExtension(file.FileName);

                    if (item.Produkt.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(rootPath, item.Produkt.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fs = new FileStream(Path.Combine(uploads, fileName + newPath), FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }
                    item.Produkt.ImageUrl = @"\img\produkty\" + fileName + newPath;
                }
                if (item.Produkt.KategoriaId == 1)
                {
                    _unitOfWork.ProduktRI.Add(item.Produkt);
                }
                else
                {
                    _unitOfWork.ProduktRI.Update(item.Produkt);
                }
                _unitOfWork.Save();
                Console.WriteLine("Rekord został dodany lub zaktualizowany w bazie danych.");

                //return Json(new { success = true, message = "Aktualizacja poprawna", redirectTo = Url.Action("Index", "Product") });
                return RedirectToAction("Index", "Product");
            }

            Console.WriteLine("Błąd ModelState: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))));
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewProduct(ViewModelProdukt item, IFormFile file)
        {
            {
                if (ModelState.IsValid)
                {
                    string rootPath = _HostEnviroment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(rootPath, @"img\produkty");
                        var newPath = Path.GetExtension(file.FileName);

                        if (item.Produkt.ImageUrl != null)
                        {
                            var oldImagePath = Path.Combine(rootPath, item.Produkt.ImageUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        using (var fs = new FileStream(Path.Combine(uploads, fileName + newPath), FileMode.Create))
                        {
                            file.CopyTo(fs);
                        }
                        item.Produkt.ImageUrl = @"\img\produkty\" + fileName + newPath;
                    }
                    if (item.Produkt.KategoriaId == 0)
                    {
                        _unitOfWork.ProduktRI.Add(item.Produkt);
                    }
                    else
                    {
                        _unitOfWork.ProduktRI.Update(item.Produkt);
                    }
                    _unitOfWork.Save();
                    Console.WriteLine("Rekord został dodany lub zaktualizowany w bazie danych.");

                    return Json(new { success = true, message = "Aktualizacja poprawna", redirectTo = Url.Action("Index", "Product") });

                }

                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
            }
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.ProduktRI.GetAll(includeProperties:"Kategoria");
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var item = _unitOfWork.ProduktRI.GetFirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return Json(new { success = false, message = "Błąd usuwania" });
            }
            var oldImagePath = Path.Combine(_HostEnviroment.WebRootPath, item.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ProduktRI.Remove(item);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Usunięte prawidłowo" });
        }
        #endregion
    }
}