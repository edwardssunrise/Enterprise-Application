using EntepriseArchitecture.Data.UnitOfWork;
using EnterpriseArchitecture.DTO.EEntity;
using EnterpriseArchitecture.Services.Abstract;
using EnterpriseArchitecture.Utilities.SessionOperations;
using EnterpriseArchitecture.Utilities.PasswordOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterpriseArchitecture.WEB.Controllers
{
    public class LoginController : PublicController
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _uow;
        private SessionContext _sessionContext;

        public LoginController(IUnitOfWork uow, IUserService userService) : base(uow)
        {
            _uow = uow;
            _userService = userService;
            _sessionContext = new SessionContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginControl(ELoginDTO login)
        {
            login.Password = CryptoManager.Base64Encrypt(login.Password);

            var result = _userService.GetUserByUserName(login.UserName, login.Password);

            AutoMapper.Mapper.DynamicMap(result, _sessionContext);
            Session["SessionContext"] = _sessionContext;

            return Json("/profile", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// When logging out, it is aimed to clear all cached sessions.
        /// Session.Abandon() ensures that all cached information is cleared when logout is made.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            /* It is used to redirect from the "Manager" page to the "Login" page when logged out. */
            Response.Redirect("/Login");
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}