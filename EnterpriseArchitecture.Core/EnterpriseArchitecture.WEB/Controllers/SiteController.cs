using EntepriseArchitecture.Data.UnitOfWork;
using EnterpriseArchitecture.DTO.EEntity;
using EnterpriseArchitecture.Services.Abstract;
using EnterpriseArchitecture.Utilities.ImageOperations;
using EnterpriseArchitecture.Utilities.StringOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterpriseArchitecture.WEB.Controllers
{
    public class SiteController : PublicController
    {
        private readonly IUnitOfWork _uow;
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        public static string html = null;

        public SiteController(IUnitOfWork uow, IPostService postService, ICategoryService categoryService, IUserService userService) : base(uow)
        {
            _uow = uow;
            _postService = postService;
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            Session["a"] = 0;
            html = null;
            var result = _categoryService.GetCategories();

            foreach (var item in result)
            {
                html += "<li><a href='/" + StringManager.ToSlug(item.Name) + "-" + item.ID + "' style='color:" + item.Color + "!important;'><i class='" + item.Icon + "'></i>" + item.Name + "</a></li>";
            }

            return View(_postService.GetPostAll(null, 0));
        }

        [HttpGet]
        public ActionResult PostList(int categoryId, string categoryName)
        {
            Session["pageNumber"] = 0;
            Session["categoryId"] = categoryId;
            Session["IsEmpty"] = _postService.GetPostAll(categoryId, 1);

            return View(_postService.GetPostAll(categoryId, 0));
        }

        [HttpGet]
        public ActionResult PostMore()
        {
            Session["pageNumber"] = (int)Session["pageNumber"] + 1;
            var list = _postService.GetPostAll((Int32)Session["categoryId"], (Int32)Session["pageNumber"]);
            Session["IsEmpty"] = _postService.GetPostAll((Int32)Session["categoryId"], (Int32)Session["pageNumber"] + 1);

            return PartialView("~/Views/Shared/PostMore.cshtml", list);
        }

        [HttpGet]
        public ActionResult PostDetail(string categoryName, string slug)
        {
            EPostDetailPageDTO pdp = new EPostDetailPageDTO();
            pdp.PostDetail = _postService.GetPostDetailBySlug(slug);
            pdp.PostList = _postService.GetPostAll(null, 0).Take(5).ToList();

            return View(pdp);
        }

        [HttpGet]
        public FileContentResult ProfileImageView(int id, int? w, int? h)
        {
            return new FileContentResult(ImageManager.ConvertToSize(_userService.GetUserImage(id), w, h), "image/png");
        }

        [HttpGet]
        public ActionResult PostImageView(int id, int? w, int? h)
        {
            return new FileContentResult(ImageManager.ConvertToSize(_postService.GetPostImageByID(id), w, h), "image/png");
        }
    }
}