﻿using Microsoft.AspNetCore.Mvc;
using Gorevcim.Core.Services;
using Gorevcim.Core.DTOs;
using Gorevcim.Core.Models;
using Gorevcim.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Gorevcim.Service.Services;

namespace Gorevcim.WEB.Controllers
{



    public class ProductsController : Controller
    {


        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductColorsService _productColorsService;
        private readonly IProductBrandsService _productBrandsService;      
        private readonly IProductProjectsService _productProjectsService;
        private readonly IProductCurrencyUnitsService _productCurrencyUnitsService;
        private readonly IProductMeasurementUnitsService _productMeasurementUnitsService;
        private readonly IProductVatUnitsService _productVatUnitsService;
        private readonly IProductWeightUnitsService _productWeightUnitsService;

        private readonly IProductFeaturesService _productFeaturesService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, ICategoryService categoryService, IProductColorsService productColorsService, IProductFeaturesService productFeaturesService, IProductBrandsService productBrandsService, IProductProjectsService productProjectsService, IProductCurrencyUnitsService productCurrencyUnitsService, IProductMeasurementUnitsService productMeasurementUnitsService, IProductVatUnitsService productVatUnitsService, IProductWeightUnitsService productWeightUnitsService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productColorsService = productColorsService;
            _productBrandsService = productBrandsService;
            _productProjectsService = productProjectsService;            
            _productCurrencyUnitsService = productCurrencyUnitsService;
            _productMeasurementUnitsService = productMeasurementUnitsService;
            _productVatUnitsService = productVatUnitsService;
            _productWeightUnitsService = productWeightUnitsService;

            _productFeaturesService = productFeaturesService;
            _mapper = mapper;   
        }

        public  async Task<IActionResult> Index()
        {
            var products = await _productService.GetWebAllProductsAsync();
            var categorys = await _categoryService.GetWebAllCategoriesAsync();
            var productsColors = await _productColorsService.GetWebAllProductColors();
            var productsBrands = await _productBrandsService.GetWebAllProductBrands();
            var productProjects= await _productProjectsService.GetWebAllProductProjects();
            var productCurrency = await _productCurrencyUnitsService.GetWebAllProductCurrency();
            var productMeasurement= await _productMeasurementUnitsService.GetWebAllProductMeasurement();
            var productVat = await _productVatUnitsService.GetWebAllProductVatUnits();
            var productWeight = await _productWeightUnitsService.GetWebAllProductWeightUnits();
            var productsFeatures = await _productFeaturesService.GetWebAllProductFeatures();



            dynamic mymodel = new ExpandoObject();
            mymodel._products = products;
            mymodel._categorys = categorys;
            mymodel._productsColors = productsColors; 
            mymodel._productsBrands = productsBrands;
            mymodel._productProjects = productProjects;
            mymodel._productCurrency = productCurrency;
            mymodel._productMeasurement = productMeasurement;
            mymodel._productVat = productVat;
            mymodel._productWeight = productWeight;
            mymodel._productsFeatures = productsFeatures;

            mymodel.activeProductsCount = products.Where(t => t.IsActive == 1).Count();
            mymodel.passiveProductsCount = products.Where(t => t.IsActive == 0).Count();

            return View(mymodel);

        }


        public async Task<IActionResult> Save()
        {
            var productscolor = await _productColorsService.GetWebAllColorsAsync();
            var productscolorDto = _mapper.Map<List<ProductColorsDto>>(productscolor.ToList());
            ViewBag.productsColor = new SelectList(productscolorDto, "Id", "Name");


            var productBrands = await _productBrandsService.GetAllAsync(); 
            var ProductBrandsDto = _mapper.Map<List<ProductBrandsDto>>(productBrands.ToList());
            ViewBag.productBrands = new SelectList(ProductBrandsDto, "Id", "BrandsName");


            var productProjects = await _productProjectsService.GetAllAsync();
            var ProductsProjectsDto = _mapper.Map<List<ProductProjectsDto>>(productProjects.ToList());
            ViewBag.productProject = new SelectList(ProductsProjectsDto, "Id", "Name");

            var productCurrency = await _productCurrencyUnitsService.GetAllAsync();
            var productCurrencyDto = _mapper.Map<List<ProductCurrencyUnitsDto>>(productCurrency.ToList());
            ViewBag.productCurrency = new SelectList(productCurrencyDto, "Id", "Name");

            var productMeasurement = await _productMeasurementUnitsService.GetAllAsync();
            var productMeasurementDto = _mapper.Map<List<ProductMeasurementUnitsDto>>(productMeasurement.ToList());
            ViewBag.productMeasurement = new SelectList(productMeasurementDto, "Id", "Name");

            var productVat = await _productVatUnitsService.GetAllAsync();
            var productVatDto = _mapper.Map<List<ProductVatUnitsDto>>(productVat.ToList());
            ViewBag.productVat = new SelectList(productVatDto, "Id", "Name");

            var productWeight = await _productWeightUnitsService.GetAllAsync();
            var productWeightDto = _mapper.Map<List<ProductWeightUnitsDto>>(productWeight.ToList());
            ViewBag.productWeight = new SelectList(productWeightDto, "Id", "Name");


            var categoryies =  await _categoryService.GetAllAsync();
            var categoryiesDto = _mapper.Map<List<CategoryDto>>(categoryies.ToList());           
            ViewBag.categories = new SelectList(categoryiesDto,"Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productsDto, ProductFeaturesDto productFeaturesDto)
        {
            if (ModelState.IsValid)
            { 
                var _productId = await _productService.AddAsync(_mapper.Map<Products>(productsDto));
                productFeaturesDto.ProductId = _productId.Id;
                await _productFeaturesService.AddAsync(_mapper.Map<ProductFeatures>(productFeaturesDto));
                TempData.Add("Success", "Ürün Özellikleriyle birlikte eklenmiştir.");
                return RedirectToAction(nameof(Index));
            }
            TempData.Add("Error", "Hata Oluştu.ProductController|Save|59");
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());   
            ViewBag.categories = new SelectList(categoriesDto,"Id", "Name");
            return View();
        }



         public async Task<IActionResult> Update(int Id)
         {
            var products = await _productService.GetByIdAsync(Id);
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto,"Id", "Name", products.CategoryId);
            return View(_mapper.Map<ProductDto>(products));
         }


        public async Task<IActionResult> Details(int Id)
        {
            var products = await _productService.GetByIdAsync(Id);
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", products.CategoryId);
            return View(_mapper.Map<ProductDto>(products));
        }


        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productsDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(_mapper.Map<Products>(productsDto));
                TempData.Add("Info", "Ürün Güncellenmiştir.");
                return RedirectToAction(nameof(Index));
            }
            TempData.Add("info", "Hata Oluştu.ProductController|Update|89");
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productsDto.CategoryId);
            return View(productsDto);
        }





        public async Task<IActionResult> Delete(int Id)
        {
            var products = await _productService.GetByIdAsync(Id);
            await _productService.RemoveAsync(products);
            TempData.Add("info", "Ürün başarılı şekilde silinmiştir.");
            return RedirectToAction(nameof(Index));
        }



    }
}
