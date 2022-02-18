using EntepriseArchitecture.Data.UnitOfWork;
using EnterpriseArchitecture.DTO.EEntity;
using EnterpriseArchitecture.Services.Abstract;
using EnterpriseArchitecture.Utilities.SessionOperations;
using EnterpriseArchitecture.Utilities.StringOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterpriseArchitecture.WEB.Controllers
{
    public class PostController : AdminController
    {
        private readonly IPostService _postService;
        private readonly IUnitOfWork _uow;

        public PostController(IUnitOfWork uow, IPostService postService) : base(uow)
        {
            _uow = uow;
            _postService = postService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            Session["TemporaryImage"] = null;
            return View(_postService.GetPostsByFilterParams(((SessionContext)Session["SessionContext"]).ID, 0, null, null));
        }

        [HttpGet]
        public ActionResult GetPostsByFilterParams(int pageNumber, string title, int? categoryID)
        {
            title = (title == "null") ? title = null : title;
            return Json(_postService.GetPostsByFilterParams(((SessionContext)Session["SessionContext"]).ID, pageNumber, title, categoryID), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPostCount()
        {
            return Json(_postService.GetPostCount(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPostDetailByPostID(int postId)
        {
            var result = _postService.GetPostDetailByPostID(postId);
            result.PostContent = HttpUtility.HtmlDecode(result.PostContent);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(EIdDTO model)
        {
            _postService.Delete(model.ID);
            _uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(EPostDTO post)
        {
            post.Slug = _postService.GetSlugAnyPost(StringManager.ToSlug(post.Title));
            post.UserID = ((SessionContext)Session["SessionContext"]).ID;
            post.PostContent = HttpUtility.HtmlEncode(post.PostContent);

            if (Session["TemporaryImage"] != null)
                post.Image = (byte[])Session["TemporaryImage"];

            _postService.Update(post);
            _uow.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Insert(EPostDTO post)
        {
            try
            {
                _uow.BeginTransaction();
                post.Slug = _postService.GetSlugAnyPost(StringManager.ToSlug(post.Title));
                post.UserID = ((SessionContext)Session["SessionContext"]).ID;
                post.PostContent = HttpUtility.HtmlEncode(post.PostContent);

                if (Session["TemporaryImage"] != null)
                    post.Image = (byte[])Session["TemporaryImage"];

                _postService.Insert(post);
                _uow.SaveChanges();
                _uow.Commit();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                _uow.Rollback();
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}