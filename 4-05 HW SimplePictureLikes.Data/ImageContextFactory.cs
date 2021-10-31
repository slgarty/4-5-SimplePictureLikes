﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;



namespace _4_05_HW_SimplePictureLikes.Data
{
    public class ImageContextFactory : IDesignTimeDbContextFactory<ImageDbContext>
    {
        public ImageDbContext CreateDbContext(string[] args)
        {

            var config = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}4-05 HW SimplePictureLikes.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new ImageDbContext(config.GetConnectionString("ConStr"));
        }
    }
}