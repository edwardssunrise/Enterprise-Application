using EntepriseArchitecture.Data.UnitOfWork;
using EnterpriseArchitecture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterpriseArchitecture.WEB.Controllers
{
    public class SharedController : BaseController
    {
        private readonly IUserService _userService;

        public SharedController(IUnitOfWork uow, IUserService userService) : base(uow)
        {
            _userService = userService;
        }

        public FileContentResult ImageView(int id)
        {
            return new FileContentResult(_userService.GetUserImage(id), "image/png");
        }
    }
}