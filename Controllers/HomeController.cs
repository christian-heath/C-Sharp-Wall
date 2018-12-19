using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using wall.Models;

namespace wall.Controllers
{
    public class HomeController : Controller
    {
        private wallContext dbContext;

        public HomeController(wallContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    dbContext.Add(user);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    return RedirectToAction("Wall");
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
                if (userInDb == null)
                {
                    ModelState.AddModelError("Email", "The email you entered did not match our records.");
                    return View("Index");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.Password);
                if (result == 0)
                {
                    ModelState.AddModelError("Password", "The password you entered did not match our records.");
                    return View("Login");
                }
                else
                {
                    HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                    return Redirect("Wall");
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("Wall")]
        public IActionResult Account()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index");
            }
            int UserId = HttpContext.Session.GetInt32("UserId") ?? default(int);
            ViewBag.User = dbContext.Users.SingleOrDefault(user => user.UserId == UserId);
            ViewBag.MessagesComments = dbContext.Users.Include(x => x.Messages).ThenInclude(y => y.Comments).ThenInclude(z => z.User).ToList();
            return View();
        }

        [HttpPost("Message")]
        public IActionResult NewMessage(Message message)
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? default(int);
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {

                message.UserId = UserId;
                dbContext.Add(message);
                dbContext.SaveChanges();
                return RedirectToAction("Wall");
            }
            ViewBag.User = dbContext.Users.FirstOrDefault(x => x.UserId == UserId);
            ViewBag.MessagesComments = dbContext.Users.Include(x => x.Messages).ThenInclude(y => y.Comments).ToList();
            return View("Wall");
        }

        [HttpPost("Comment/{messageId}")]
        public IActionResult NewComment(Comment comment, int messageId)
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? default(int);
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index");
            }
            comment.MessageId = messageId;
            if (ModelState.IsValid)
            {

                comment.UserId = UserId;
                dbContext.Add(comment);
                dbContext.SaveChanges();
                return RedirectToAction("Wall");
            }
            ViewBag.User = dbContext.Users.FirstOrDefault(x => x.UserId == UserId);

            ViewBag.MessagesComments = dbContext.Users.Include(x => x.Messages).ThenInclude(y => y.Comments).ToList();
            return View("Wall");
        }

        [HttpPost("Delete")]
        public IActionResult Delete(int messageId, int UserId)
        {
            DbContext.Remove()
        }
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}