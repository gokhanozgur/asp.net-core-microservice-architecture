using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using XProject.Services.Catalog.Dtos;
using XProject.Services.Catalog.Model;
using XProject.Services.Catalog.Settings;
using XProject.Shared.Dtos;

namespace XProject.Services.Catalog.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);


            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), (int)HttpStatusCode.OK);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var newCategory = _mapper.Map<Category>(categoryCreateDto);
            await _categoryCollection.InsertOneAsync(newCategory);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(newCategory), (int)HttpStatusCode.Created); // or (int)HttpStatusCode.OK
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var updateCategory = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (updateCategory == null)
            {
                return Response<CategoryDto>.Fail("Not found", (int)HttpStatusCode.NotFound);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(updateCategory), (int)HttpStatusCode.OK);
        }
    }
}
