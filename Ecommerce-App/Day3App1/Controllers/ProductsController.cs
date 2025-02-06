using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Controllers;

public class ProductsController : Controller
{
    private MyAppContext _context;
    public ProductsController(MyAppContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var products = _context.Products.ToList(); // Fetch all products
        return View(products);
    }

    public IActionResult Category(int id)
    {
        var products = _context.Products.Where(p => p.CategoriesId == id).ToList();
        return View("Index", products);
    }


    // GET: Create Product
    public IActionResult Create()
    {
        var categories = _context.Categories.ToList();
        ViewBag.Category = new SelectList(categories, "Id", "CategoryName");
        return View();
    }

    // POST: Create Product
    [HttpPost]
    public async Task<IActionResult> Create(Products product, IFormFile? imageFile, IFormFileCollection? imageFiles)
    {
        if (ModelState.IsValid)
        {
            // Handle main image upload
            if (imageFile != null && imageFile.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imageFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                product.Image = imageFile.FileName;
            }

            // Handle additional images upload
            if (imageFiles != null && imageFiles.Count > 0)
            {
                var imagePaths = new List<string>();

                foreach (var file in imageFiles)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    imagePaths.Add(file.FileName);
                }

                product.Images = string.Join("|", imagePaths);  // Save image file names in a pipe-separated string
            }

            // Add the new product to the database
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var categories = _context.Categories.ToList();
        ViewBag.Category = new SelectList(categories, "Id", "CategoryName");
        return View(product);
    }

    public IActionResult Details(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

}

  
