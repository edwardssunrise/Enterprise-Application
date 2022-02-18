using EntepriseArchitecture.Data.UnitOfWork;
using EnterpriseArchitecture.DTO.EEntity;
using EnterpriseArchitecture.Services.Abstract;
using EnterpriseArchitecture.Utilities.SessionOperations;
using EnterpriseArchitecture.Utilities.PasswordOperations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterpriseArchitecture.WEB.Controllers
{
    public class ProfileController : AdminController
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _uow;

        public ProfileController(IUnitOfWork uow, IUserService userService) : base(uow)
        {
            _uow = uow;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            Session["TemporaryImage"] = null;

            return View(_userService.Find(((SessionContext)Session["SessionContext"]).ID));
        }

        [HttpPost]
        public ActionResult TemporaryImage(HttpPostedFileBase ImageFormData)
        {
            //HttpPostedFileBase ImageFormData = HttpContext.Request.Files[0];

            using (BinaryReader reader = new BinaryReader(ImageFormData.InputStream))
            {
                Session["TemporaryImage"] = reader.ReadBytes((Int32)ImageFormData.ContentLength);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TemporaryImageShow(HttpPostedFileBase ImageFormData)
        {
            return new FileContentResult((byte[])Session["TemporaryImage"], "image/png");
        }

        [HttpPost]
        public ActionResult Update(EUserDTO user)
        {
            try
            {
                user.ID = ((SessionContext)Session["SessionContext"]).ID;
                user.Password = CryptoManager.Base64Encrypt(user.Password);

                _userService.Update(user);
                _uow.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult UserImageUpdate()
        {
            try
            {
                _userService.Update(new EUserDTO
                {
                    WhichUpdate = "I",
                    ID = ((SessionContext)Session["SessionContext"]).ID,
                    Value = (byte[])Session["TemporaryImage"]
                    
                });
                _uow.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }
    }
}