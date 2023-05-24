﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLibrary.DB
{
    public class MySQLConnDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            //optionBuilder.UseMySQL("Server=localhost;Database=medlink;UID=root;Password=");
            optionBuilder.UseMySQL("Server=169.254.30.133;Database=medlink;UID=root;Password=");
        }
    }
}
