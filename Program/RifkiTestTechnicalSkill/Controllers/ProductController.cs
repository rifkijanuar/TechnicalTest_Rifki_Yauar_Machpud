using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RifkiTestTechnicalSkill.Constants;
using RifkiTestTechnicalSkill.Models;
using RifkiTestTechnicalSkill.Models.DTOs;
using RifkiTestTechnicalSkill.Services.Interface;

namespace RifkiTestTechnicalSkill.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IGenreService _genreService;
        private readonly IFileService _fileService;

        public ProductController(IProductService productService, IGenreService genreService, IFileService fileService)
        {
            _productService = productService;
            _genreService = genreService;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> AddProduct()
        {
            var genreSelectList = (await _genreService.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
            });
            ProductDTO bookToAdd = new() { GenreList = genreSelectList };
            return View(bookToAdd);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO productToAdd)
        {
            var genreSelectList = (await _genreService.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
            });
            productToAdd.GenreList = genreSelectList;

            if (!ModelState.IsValid)
                return View(productToAdd);

            try
            {
                if (productToAdd.ImageFile != null)
                {
                    if (productToAdd.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("Image file can not exceed 1 MB");
                    }
                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(productToAdd.ImageFile, allowedExtensions);
                    productToAdd.Image = imageName;
                }
                // manual mapping of BookDTO -> Book
                Product product = new()
                {
                    Id = productToAdd.Id,
                    ProductName = productToAdd.ProductName,
                    AuthorName = productToAdd.AuthorName,
                    Image = productToAdd.Image,
                    GenreId = productToAdd.GenreId,
                    Price = productToAdd.Price
                };
                await _productService.AddProduct(product);
                TempData["successMessage"] = "Product is added successfully";
                return RedirectToAction(nameof(AddProduct));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(productToAdd);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(productToAdd);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on saving data";
                return View(productToAdd);
            }
        }

        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                TempData["errorMessage"] = $"Book with the id: {id} does not found";
                return RedirectToAction(nameof(Index));
            }
            var genreSelectList = (await _genreService.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
                Selected = genre.Id == product.GenreId
            });
            ProductDTO bookToUpdate = new()
            {
                GenreList = genreSelectList,
                ProductName = product.ProductName,
                AuthorName = product.AuthorName,
                GenreId = product.GenreId,
                Price = product.Price,
                Image = product.Image
            };
            return View(bookToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductDTO productToUpdate)
        {
            var genreSelectList = (await _genreService.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
                Selected = genre.Id == productToUpdate.GenreId
            });
            productToUpdate.GenreList = genreSelectList;

            if (!ModelState.IsValid)
                return View(productToUpdate);

            try
            {
                string oldImage = "";
                if (productToUpdate.ImageFile != null)
                {
                    if (productToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("Image file can not exceed 1 MB");
                    }
                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(productToUpdate.ImageFile, allowedExtensions);
                    // hold the old image name. Because we will delete this image after updating the new
                    oldImage = productToUpdate.Image;
                    productToUpdate.Image = imageName;
                }
                // manual mapping of BookDTO -> Book
                Product book = new()
                {
                    Id = productToUpdate.Id,
                    ProductName = productToUpdate.ProductName,
                    AuthorName = productToUpdate.AuthorName,
                    GenreId = productToUpdate.GenreId,
                    Price = productToUpdate.Price,
                    Image = productToUpdate.Image
                };
                await _productService.UpdateProduct(book);
                // if image is updated, then delete it from the folder too
                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileService.DeleteFile(oldImage);
                }
                TempData["successMessage"] = "Product is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(productToUpdate);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(productToUpdate);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on saving data";
                return View(productToUpdate);
            }
        }

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null)
                {
                    TempData["errorMessage"] = $"Book with the id: {id} does not found";
                }
                else
                {
                    await _productService.DeleteProduct(product);
                    if (!string.IsNullOrWhiteSpace(product.Image))
                    {
                        _fileService.DeleteFile(product.Image);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on deleting the data";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
