using data_access.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Data
{
    internal static class DefaultData
    {

        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(Users);
            modelBuilder.Entity<Category>().HasData(Categories);

        }

        public static readonly User[] Users =
        {
            new User(){ Id = 1, NikName = "Fagot" , PasswordHash = "19513fdc9da4fb72a4a05eb66917548d3c90ff94d5419e1f2363eea89dfee1dd"},     //Password1
            new User(){ Id = 2, NikName = "Veronica" , PasswordHash = "1be0222750aaf3889ab95b5d593ba12e4ff1046474702d6b4779f4b527305b23"},  //Password2
            new User(){ Id = 3, NikName = "Alex" , PasswordHash = "2538f153f36161c45c3c90afaa3f9ccc5b0fa5554c7c582efe67193abb2d5202"}       //Password3

        };

        public static readonly Category[] Categories =
        {

        };
    }
}
