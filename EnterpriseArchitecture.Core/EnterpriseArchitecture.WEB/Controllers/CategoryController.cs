using EntepriseArchitecture.Data.UnitOfWork;
using EnterpriseArchitecture.DTO.EEntity;
using EnterpriseArchitecture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterpriseArchitecture.WEB.Controllers
{
    public class CategoryController : AdminController
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;
        private readonly IUnitOfWork _uow;
        
        public CategoryController(IUnitOfWork uow, ICategoryService categoryService, IPostService postService) : base(uow)
        {
            _uow = uow;
            _categoryService = categoryService;
            _postService = postService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_categoryService.GetCategories());
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            var list = _categoryService.GetCategories();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCategoryDetailByCategoryID(int categoryId)
        {
            var result = _categoryService.GetCategoryDetailByCategoryID(categoryId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(EIdDTO model)
        {
            if (_postService.AnyPostByCategoryID(model.ID) == false)
            {
                _categoryService.Delete(model.ID);
                _uow.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Update(ECategoryDTO category)
        {
            _categoryService.Update(category);
            _uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Insert(ECategoryDTO category)
        {
            _categoryService.Insert(category);
            _uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}