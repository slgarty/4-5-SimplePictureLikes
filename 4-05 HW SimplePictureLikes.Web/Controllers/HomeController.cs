using _4_05_HW_SimplePictureLikes.Data;
using _4_05_HW_SimplePictureLikes.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace _4_05_HW_SimplePictureLikes.Web.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            var vm = new ImageViewModel();
            var db = new ImageRepository(_connectionString);
            vm.Images = db.GetAll();
            return View(vm);
        }
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(string title, IFormFile imageFile)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            string fullPath = Path.Combine(_environment.WebRootPath, "uploads", fileName);
            using (var stream = new FileStream(fullPath, FileMode.CreateNew))
            {
                imageFile.CopyTo(stream);
            }
            var image = new Image
            {
                Title = title,
                FileName = fileName,
                TimePosted = DateTime.Now
            };
            var db = new ImageRepository(_connectionString);
            db.Add(image);
            return RedirectToAction("Index");
        }
        public IActionResult ViewImage(int id)
        {

            var db = new ImageRepository(_connectionString);          
            var image = db.GetById(id);
            var vm = new ImageViewModel
            {
                Image = image
            };
            if (HttpContext.Session.GetString("likedids") != null)
            {
                var likedIds = HttpContext.Session.Get<List<int>>("likedids");;
                if (likedIds.Find(i => i != id) != 0)
                {
                    vm.Likable = false;
                }
            }
            else
            {
                vm.Likable = true;
            }
            return View(vm);
        }
        [HttpPost]
        public void Like(int id)
        {
            var db = new ImageRepository(_connectionString);
            db.Like(id);
            List<int> likedIds = HttpContext.Session.Get<List<int>>("likedids") ?? new List<int>();

            likedIds.Add(id);

            HttpContext.Session.Set("likedids", likedIds);
            
        }
        public ActionResult GetLikes(int id)
        {
            var db = new ImageRepository(_connectionString);
            return Json(new { Likes = db.GetLikes(id) });
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
