using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XProject.Services.Catalog.Dtos;
using XProject.Services.Catalog.Model;
using XProject.Shared.Dtos;

namespace XProject.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
