using EntepriseArchitecture.Data.GenericRepository;
using EntepriseArchitecture.Data.UnitOfWork;
using EnterpriseArchitecture.Core;
using EnterpriseArchitecture.DTO.EEntity;
using EnterpriseArchitecture.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseArchitecture.Services.Concrete
{
    /// <summary>
    /// Business logic is implemented in the Service Layer, also called the Business Logic Layer.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _uow;

        public CategoryService(UnitOfWork uow)
        {
            _uow = uow;
            _categoryRepository = uow.GetRepository<Category>();
        }

        public void Delete(int id)
        {
            var categoryEntity = _categoryRepository.Find(id);
            _categoryRepository.Delete(categoryEntity);
        }

        public List<ECategoryDTO> GetCategories()
        {
            var result = (from c in _categoryRepository.GetAll()
                          select new ECategoryDTO()
                          {
                              Color = c.Color,
                              ID = c.ID,
                              Name = c.Name,
                              Icon = c.Icon
                          }).ToList();
            return result;
        }

        public ECategoryDTO GetCategoryDetailByCategoryID(int categoryID)
        {
            var result = (from c in _categoryRepository.GetAll()
                          where c.ID == categoryID
                          select new ECategoryDTO()
                          {
                              Color = c.Color,
                              ID = c.ID,
                              Name = c.Name,
                              Icon = c.Icon
                          }).SingleOrDefault();
            return result;
        }

        public void Insert(ECategoryDTO category)
        {
            var categoryEntity = AutoMapper.Mapper.DynamicMap<Category>(category);
            _categoryRepository.Insert(categoryEntity);
        }

        public void Update(ECategoryDTO category)
        {
            var categoryEntity = _categoryRepository.Find(category.ID);
            AutoMapper.Mapper.DynamicMap(category, categoryEntity);
            _categoryRepository.Update(categoryEntity);
        }
    }
}
