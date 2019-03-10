using System;
using System.Collections.Generic;
using System.Text;
using Boyner.Data;
using Boyner.Domain.Entities;
using Boyner.Test.TestEntities;
using Microsoft.EntityFrameworkCore;

namespace Boyner.Test
{
   public class TestingContext:ConfigContext
    {
        protected DbSet<TestEntities.ConfigEntityTest> TestModel { get; set; }
       

        private String DatabaseName { get; }

        public TestingContext()
        {
            DatabaseName = Guid.NewGuid().ToString();
        }
        public TestingContext(TestingContext context)
        {
            DatabaseName = context.DatabaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase(DatabaseName); 
        }
    }
}
