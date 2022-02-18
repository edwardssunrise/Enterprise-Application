using EnterpriseArchitecture.DTO.EEntity;
using System.Collections.Generic;

namespace EnterpriseArchitecture.Services.Abstract
{
    /// <summary>
    /// Business logic is implemented in the Service Layer, also called the Business Logic Layer.
    /// </summary>
    public interface IPostService
    {
        int GetPostCount();
        void Delete(int id);
        void Update(EPostDTO post);
        void Insert(EPostDTO post);
        List<EGetPostsByUserIdDTO> GetPostsByFilterParams(int userID, int pageNumber, string title, int? categoryID);
        EPostDTO GetPostDetailByPostID(int postID);
        string GetSlugAnyPost(string slug);
        bool AnyPostByCategoryID(int categoryID);
        byte[] GetPostImageByID(int id);
        List<EPostUserDTO> GetPostAll(int? categoryID, int pageNumber);
        EPostDetailDTO GetPostDetailBySlug(string slug);
    }
}