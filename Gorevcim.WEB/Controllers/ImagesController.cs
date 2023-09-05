using Microsoft.AspNetCore.Mvc;
using Gorevcim.Core.Services;
using Gorevcim.Core.DTOs;
using Gorevcim.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using Gorevcim.Core.Models;
using Gorevcim.Repository.AppDbContexts.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Gorevcim.Service.Services;
using System.Web;
using System;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace Gorevcim.WEB.Controllers
{
    public class ImagesController : Controller
    {

        private readonly IImagesService _imagesService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _environment;

        public ImagesController (IImagesService imagesService, IMapper mapper,IHostingEnvironment environment)
        {
            _imagesService = imagesService;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var images = await _imagesService.GetWebAllImages();
            dynamic mymodel = new ExpandoObject();
            mymodel._images = images;
            mymodel.activeImagesCount = images.Where(t => t.IsActive == 1).Count();
            mymodel.passiveImagesCount = images.Where(t => t.IsActive == 0).Count();
            return View(mymodel);
        }

        public async Task<IActionResult> Save()
        {
            var images = await _imagesService.GetAllAsync();
            var imagesDto = _mapper.Map<List<ImagesDto>>(images.ToList());
            ViewBag.images = new SelectList(imagesDto, "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Save(ImagesDto imagesDto,IFormFile file, string base64String)
        {
            var folderPath = Path.Combine(_environment.WebRootPath, "ImagesUploads");
            var filePath= Path.Combine(folderPath,file.FileName);

            DateTime date = DateTime.Now;
            string date_str = date.ToString("dd-MM-yyyy-HH-mm-ss-");
            var _imagesName = date_str + file.FileName;
            if (file.Length >0)
            {
                using (var stream = new FileStream(filePath,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                byte[] bytes_array = System.IO.File.ReadAllBytes(filePath);
                base64String = Convert.ToBase64String(bytes_array);

                var _imagesPath = filePath;
                var _imagesBase64 = base64String;

                imagesDto.LogoBase64 = _imagesBase64;
                //imagesDto.ImageName = _imagesName;
                imagesDto.FilePath = _imagesPath;

                await _imagesService.AddAsync(_mapper.Map<Images>(imagesDto));
                TempData.Add("Success", "Resim başarıyla eklenmiştir.");
                return RedirectToAction(nameof(Index));

            }

            else
            {
                TempData.Add("Error", "Hata Oluştu. ImagesController|Save|83");
                return View();
            }

        }
    



     




    }





}

