using EntepriseArchitecture.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterpriseArchitecture.WEB.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IUnitOfWork uof)
        {

        }
    }
}