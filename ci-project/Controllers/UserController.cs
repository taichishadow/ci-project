﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ci_project.Models;
using ci_project.DAO;
using Dapper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ci_project.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult obtain_users(UserModel user)
        {
            UserDAO userDAO = new UserDAO();
            List<UserModel> users = userDAO.obtainAllUsers();

            return Json(users);
        } 

        public IActionResult obtain_users_by_params(UserModel user)
        {
            UserDAO userDAO = new UserDAO();
            DynamicParameters parameters = new DynamicParameters();
            if(user.name != null)
            {
                parameters.Add("@name", user.name);
            }
            if(user.cashBalance >= 0)
            {
                parameters.Add("@cashBalance", user.cashBalance);
            }
            List<UserModel> users = userDAO.obtainUsersByParams(parameters);

            return Json(users);
        }

        [HttpPost]
        public void add_user([FromBody]UserModel user)
        {
            UserDAO userDAO = new UserDAO();
            userDAO.insert(user);
        }

        [HttpPost]
        public void update_user([FromBody] UserModel user)
        {
            UserDAO userDAO = new UserDAO();
            userDAO.update(user);
        }

        [HttpPost]
        public void delete_user([FromBody] UserModel user)
        {
            UserDAO userDAO = new UserDAO();
            userDAO.delete(user);
        }
    }
}
