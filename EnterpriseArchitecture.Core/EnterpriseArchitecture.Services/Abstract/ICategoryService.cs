using EnterpriseArchitecture.DTO.EEntity;
using System.Collections.Generic;

namespace EnterpriseArchitecture.Services.Abstract
{
    /// <summary>
    /// Business logic is implemented in the Service Layer, also called the Business Logic Layer.
    /// </summary>
    public interface ICategoryService
    {
        void Delete(int id);
        void Update(ECategoryDTO category);
        void Insert(ECategoryDTO category);
        ECategoryDTO GetCategoryDetailByCategoryID(int categoryID);
        List<ECategoryDTO> GetCategories();
    }
}