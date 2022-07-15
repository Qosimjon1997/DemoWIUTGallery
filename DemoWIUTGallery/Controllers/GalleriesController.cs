using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoWIUTGallery.Data;
using DemoWIUTGallery.Models;
using DemoWIUTGallery.Interfaces;
using Microsoft.AspNetCore.Hosting;
using DemoWIUTGallery.ViewModels;
using System.IO;

namespace DemoWIUTGallery.Controllers
{
    public class GalleriesController : Controller
    {
        private readonly ICreate<Gallery> _create;
        private readonly IDelete<Gallery> _delete;
        private readonly IRead<Gallery> _read;
        private readonly IReadRange<Gallery> _readRange;
        private readonly IUpdate<Gallery> _update;
        private readonly IWebHostEnvironment _environment;

        public GalleriesController(IWebHostEnvironment environment, ICreate<Gallery> create, IDelete<Gallery> delete, IRead<Gallery> read, IReadRange<Gallery> readRange, IUpdate<Gallery> update)
        {
            _create = create;
            _delete = delete;
            _read = read;
            _readRange = readRange;
            _update = update;
            _environment = environment;
        }

        // GET: Galleries
        public async Task<IActionResult> Index()
        {
            return View(await _readRange.GetAllAsync());
        }

        // GET: Galleries/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _read.GetByIdAsync(id);

            var galleryImageViewModel = new GalleryImageViewModel()
            {
                Id = gallery.Id,
                Name = gallery.Name,
                ExistingImage = gallery.PathFile,
                AddedDate = gallery.AddedDate
            };
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // GET: Galleries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GalleryImageViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string uniqueFileName = ProcessUploadedFile(model);
                    Gallery gallery = new Gallery()
                    {
                        AddedDate = model.AddedDate,
                        Name = model.Name,
                        PathFile = uniqueFileName
                    };
                    await _create.CreateAsync(gallery);
                    
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }

        // GET: Galleries/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _read.GetByIdAsync(id);

            var galleryImageViewModel = new GalleryImageViewModel()
            {
                Id = gallery.Id,
                Name = gallery.Name,
                ExistingImage = gallery.PathFile,
                AddedDate = gallery.AddedDate
            };

            if (gallery == null)
            {
                return NotFound();
            }
            return View(galleryImageViewModel);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,GalleryImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var gallery = await _read.GetByIdAsync(id);

                gallery.AddedDate = model.AddedDate;
                gallery.Name = model.Name;

                if (model.Picture != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(_environment.WebRootPath, "Uploads", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    gallery.PathFile = ProcessUploadedFile(model);
                }
                await _update.Update(gallery);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Galleries/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _read.GetByIdAsync(id);

            var galleryImageViewModel = new GalleryImageViewModel()
            {
                Id = gallery.Id,
                Name = gallery.Name,
                ExistingImage = gallery.PathFile,
                AddedDate = gallery.AddedDate
            };

            if (gallery == null)
            {
                return NotFound();
            }

            return View(galleryImageViewModel);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gallery = await _read.GetByIdAsync(id);

            string deleteFileFromFolder = Path.Combine(_environment.WebRootPath, "Uploads");
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFileFromFolder, gallery.PathFile);
            _delete.Delete(gallery);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            }
            return RedirectToAction(nameof(Index));
        }

        private string ProcessUploadedFile(GalleryImageViewModel model)
        {
            string uniqueFileName = null;
            string path = Path.Combine(_environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
