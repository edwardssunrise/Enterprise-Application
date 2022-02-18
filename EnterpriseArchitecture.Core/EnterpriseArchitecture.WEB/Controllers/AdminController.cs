using EntepriseArchitecture.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterpriseArchitecture.WEB.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(IUnitOfWork uow) : base(uow)
        {
            
        }

        /// <summary>
        /// Since all Controllers are derived from the BaseController class, the method 
        /// overridden below is the area where the automatically executed code blocks are 
        /// written after the Action method is run. Such methods are known as "Action & Result 
        /// Filters" in ASP.NET MVC.
        ///
        /// When the View pages of the other Controller derived from AdminController are 
        /// desired to be displayed, the View Page must be redirected to the /Login page 
        /// for the user to login.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["SessionContext"] == null)
            {
                Response.Redirect("/Login");
            }

            base.OnActionExecuted(filterContext);
        }
    }
}