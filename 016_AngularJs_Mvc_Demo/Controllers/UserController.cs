using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _016_AngularJs_Mvc_Demo.Classes;
using _016_AngularJs_Mvc_Demo.Models;

namespace _016_AngularJs_Mvc_Demo.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        // GET: All User
        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserList()
        {
            UserDALcls userDALobjList = new UserDALcls();
            return Json(userDALobjList.getList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int AddUser(UserModels userModelObj)
        {
            UserDALcls userDALobjAdd = new UserDALcls();
            userDALobjAdd.insert(userModelObj);
            return userModelObj.UserID;
        }

        [HttpPut]
        public void UpdateUser(UserModels userModelObj, int id)
        {
            if (userModelObj.UserID == id)
            {
                UserDALcls userDALobjAdd = new UserDALcls();
                userDALobjAdd.update(userModelObj);

            }
        }

        [HttpDelete]
        public void DeleteUser(int id)
        {
            UserModels userModelObj = new UserModels();
            userModelObj.UserID = id;

            UserDALcls userDALobjAdd = new UserDALcls();
            userDALobjAdd.delete(userModelObj);
        }

    }
}
