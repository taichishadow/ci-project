using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ci_project.Models;
using ci_project.DAO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ci_project.Controllers
{
    public class BookStoreController : Controller
    {
        // GET: /<controller>/
        public IActionResult obtain_book_stores(UserModel bookStore)
        {
            BookStoreDAO bookStoreDAO = new BookStoreDAO();
            List<BookStoreModel> bookStores = bookStoreDAO.obtainAllBookStores();

            return Json(bookStores);
        }

        public IActionResult obtain_book_at_certain_datetime(UserModel bookStore)
        {
            BookStoreDAO bookStoreDAO = new BookStoreDAO();
            List<BookStoreModel> bookStores = bookStoreDAO.obtainBookStoreAtCertainDateTime();

            return Json(bookStores);
        }

        [HttpPost]
        public void add_book_store([FromBody] BookStoreModel bookStore)
        {
            BookStoreDAO bookStoreDAO = new BookStoreDAO();
            bookStoreDAO.insert(bookStore);
        }
    }
}
