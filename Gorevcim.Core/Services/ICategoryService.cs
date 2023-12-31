﻿using Gorevcim.Core.DTOs;
using Gorevcim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorevcim.Core.Services
{
    public interface ICategoryService:IGenericService<Category>
    {
        public Task<CustomResponseDto<CategoryProductDto>> GetApiCategoryIdProductsAsync(int categoryId);
        public Task<CustomResponseDto<CategoryProductDto>> GetApiAllCategoryProductsAsync();
        public Task<List<CategoryProductDto>> GetWebCategoryIdProductsAsync(int categoryId);
        public Task<List<CategoryProductDto>> GetWebAllCategoryProductsAsync();
        public Task<List<CategoryDto>> GetWebAllCategoriesAsync();

    }
}
