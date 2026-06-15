using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDevHub.Models;
using MyDevHub.Services;
using System.Threading.Tasks;

namespace MyDevHub.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IImageService _imageService;

        public GalleryController(IImageService imageService)
        {
            _imageService = imageService;
        }
        // GET: GalleryController
        public async Task<ActionResult> ViewGallery()
        {
            var images = await _imageService.GetImages();
            return View(images);
        }

        // GET: GalleryController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GalleryController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GalleryController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(GalleryViewModel model)
        {
            //if (!ModelState.IsValid)
            //    return View(model);

            if (model.File == null)
            {
                return View(model);
            }

            var result = await _imageService.UploadImage(model);

            if (!result)
            {
                ModelState.AddModelError("", "Upload failed");
                return View(model);
            }

            return RedirectToAction(nameof(ViewGallery));
        }

        // GET: GalleryController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GalleryController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(ViewGallery));
            }
            catch
            {
                return View();
            }
        }

        // GET: GalleryController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GalleryController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(ViewGallery));
            }
            catch
            {
                return View();
            }
        }
    }
}
