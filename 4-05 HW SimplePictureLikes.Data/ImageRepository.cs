using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_05_HW_SimplePictureLikes.Data
{
    public class ImageRepository
    {
        private readonly string _connectionString;

        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Image> GetAll()
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.ToList();
        }
        public void Add(Image image)
        {
            using var context = new ImageDbContext(_connectionString);
            context.Images.Add(image);
            context.SaveChanges();
        }
        public Image GetById (int id)
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.FirstOrDefault(i => i.Id == id);
        }

        public void Like(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"UPDATE Images SET Likes = Likes + 1 WHERE Id = {id}");
        }
        public int GetLikes(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.Where(i => i.Id == id).Select(i => i.Likes).FirstOrDefault();
        }
    }
}
