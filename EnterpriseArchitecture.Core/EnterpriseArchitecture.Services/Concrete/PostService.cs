using EntepriseArchitecture.Data.GenericRepository;
using EntepriseArchitecture.Data.UnitOfWork;
using EnterpriseArchitecture.Core;
using EnterpriseArchitecture.Core.Constants;
using EnterpriseArchitecture.DTO.EEntity;
using EnterpriseArchitecture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseArchitecture.Services.Concrete
{
    /// <summary>
    /// Business logic is implemented in the Service Layer, also called the Business Logic Layer.
    /// </summary>
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Category> _categoryRepository;

        public PostService(UnitOfWork uow)
        {
            _uow = uow;
            _postRepository = uow.GetRepository<Post>();
            _userRepository = uow.GetRepository<User>();
            _categoryRepository = uow.GetRepository<Category>();
        }

        public bool AnyPostByCategoryID(int categoryID)
        {
            return _postRepository.GetAll().Any(p => p.CategoryID == categoryID);
        }

        public void Delete(int id)
        {
            var postEntity = _postRepository.Find(id);
            _postRepository.Delete(postEntity);
        }

        public List<EPostUserDTO> GetPostAll(int? categoryID, int pageNumber)
        {
            var post = (from p in _postRepository.GetAll()
                        join u in _userRepository.GetAll() on p.UserID equals u.ID
                        join c in _categoryRepository.GetAll() on p.CategoryID equals c.ID
                        where p.IsActive == true && (categoryID != null ? p.CategoryID == categoryID : 1 == 1)
                        orderby p.CreatedOn descending
                        select new EPostUserDTO
                        {
                            CategoryID = p.CategoryID,
                            CategoryName = c.Name,
                            Title = p.Title,
                            Slug = p.Slug,
                            ShortDescription = p.ShortDescription,
                            PostImageURL = "/postimageview/" + p.ID,
                            UserImageURL = "/profileimageview/" + u.ID,
                            FullName = u.FullName,
                            Job = u.Job,
                            Color = c.Color
                        }).Skip(pageNumber * ConstantTypes.SitePostCount).Take(ConstantTypes.SitePostCount).ToList();

            return post;
        }

        public int GetPostCount()
        {
            var count = _postRepository.GetAll().Count();
            var result = count / ConstantTypes.listCount;

            if (count % ConstantTypes.listCount == 0)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return Convert.ToInt32(result + 1);
            }
        }

        public EPostDTO GetPostDetailByPostID(int postID)
        {
            var post = (from p in _postRepository.GetAll()
                        where p.ID == postID
                        select new EPostDTO()
                        {
                            ID = p.ID,
                            CategoryID = p.CategoryID,
                            IsActive = p.IsActive,
                            ModifiedOn = p.ModifiedOn,
                            PostContent = p.PostContent,
                            ShortDescription = p.ShortDescription,
                            Slug = p.Slug,
                            Title = p.Title,
                            ImageURL = "/PostImageView/" + p.ID + "/60/60"
                        }).SingleOrDefault();

            post.ModifiedOnString = post.ModifiedOn.ToString("yyyy-MM-ddThh:mm");
            
            return post;
        }

        public EPostDetailDTO GetPostDetailBySlug(string slug)
        {
            var post = (from p in _postRepository.GetAll()
                        join u in _userRepository.GetAll() on p.UserID equals u.ID
                        join c in _categoryRepository.GetAll() on p.CategoryID equals c.ID
                        where p.IsActive == true && p.Slug == slug
                        select new EPostDetailDTO
                        {
                            Title = p.Title,
                            PostImageUrl = "/postimageview/" + p.ID,
                            UserImageUrl = "/profileimageview/" + u.ID,
                            FullName = u.FullName,
                            Job = u.Job,
                            PostContent = p.PostContent,
                            CreatedOn = p.CreatedOn,
                        }).SingleOrDefault();

            post.CreatedOnString = post.CreatedOn.ToString("dd MMMM yyyy hh:mm");
            return post;
        }

        public byte[] GetPostImageByID(int id)
        {
            var result = _postRepository.GetAll().Where(p => p.ID == id).SingleOrDefault();

            return (result == null) ? null : result.Image;
        }

        public List<EGetPostsByUserIdDTO> GetPostsByFilterParams(int userID, int pageNumber, string title, int? categoryID)
        {
            var List = (from p in _postRepository.GetAll()
                        where p.UserID == userID && (categoryID != null ? (p.CategoryID == categoryID.Value) : true) && (title != null ? (p.Title.Contains(title)) : true)
                        orderby p.ID descending
                        select new EGetPostsByUserIdDTO
                        {
                            PostID = p.ID,
                            Title = p.Title,
                            CreatedOn = p.CreatedOn,
                            IsActive = p.IsActive,
                        }).Skip(pageNumber * ConstantTypes.listCount).Take(ConstantTypes.listCount).ToList();
            
            foreach (var item in List)
            {
                item.CreatedOnString = item.CreatedOn.ToString("dd.MM.yyyy hh:mm");
            }

            return List;
        }

        public string GetSlugAnyPost(string slug)
        {
            int count = 0;
            string orgSlug = slug;

            while (_postRepository.GetAll().Where(p => p.Slug == slug).SingleOrDefault() != null)
            {
                count++;
                var result = _postRepository.GetAll().Where(p => p.Slug == slug).SingleOrDefault();
                slug = orgSlug + "-" + count;
            }

            return slug;
        }

        public void Insert(EPostDTO post)
        {
            var postEntity = AutoMapper.Mapper.DynamicMap<Post>(post);
            postEntity.CreatedOn = DateTime.Now;
            _postRepository.Insert(postEntity);
        }

        public void Update(EPostDTO post)
        {
            var postEntity = _postRepository.Find(post.ID);
            AutoMapper.Mapper.DynamicMap(post, postEntity);
            _postRepository.Update(postEntity);
        }
    }
}