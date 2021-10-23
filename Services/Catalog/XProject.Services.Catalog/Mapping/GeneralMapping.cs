using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XProject.Services.Catalog.Dtos;
using XProject.Services.Catalog.Model;

namespace XProject.Services.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            // Course Mapping
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();

            // Category Mapping
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            // Feature Mapping
            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<Feature, FeatureCreateDto>().ReverseMap();
            CreateMap<Feature, FeatureUpdateDto>().ReverseMap();
        }
    }
}
