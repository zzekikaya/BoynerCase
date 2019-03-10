using Boyner.Core.Repository;
using Boyner.Test.TestEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Boyner.Test
{

    public class ConfigTest
    {
        TestEntities.ConfigEntityTest config = new TestEntities.ConfigEntityTest()
        {
            Id = 1,
            IsActive = false,
            ApplicationName = "SERVICE-A",
            Name = "SiteName",
            Type = "String",
            Value = "Boyner.com.tr"
        };
        //config.Id=new Guid();
        //config.IsActive = false;
        //config.ApplicationName = "SERVICE-A";
        //config.Name = "SiteName";
        //config.Type = "String";
        //config.Value = "Boyner.com.tr"; 

        private TestingContext context;
        private UnitOfWork unitOfWork;



        public ConfigTest()
        {
            context = new TestingContext();
            unitOfWork = new UnitOfWork(context);

        }

        public void Dispose()
        {
            unitOfWork.Dispose();
            context.Dispose();
        }

        [Fact]
        public async Task GetConfigirationTest()
        {
            context.Add(config);
            context.SaveChanges();
            string actual = unitOfWork.GetValue<string>("SERVICE-A");
            string expected = "Boyner.com.tr";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task InValidGetConfigTest()
        {
            context.Add(config);
            context.SaveChanges();
            string actual = unitOfWork.GetValue<string>("SERVICE-B");
            string expected = "Boyner.com.tr";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task InsertConfigTest()
        {
            TestEntities.ConfigEntityTest configInsert = new TestEntities.ConfigEntityTest()
            {
                //Id = 12,
                IsActive = false,
                ApplicationName = "SERVICE-A",
                Name = "SiteName",
                Type = "String",
                Value = "Boyner.com.tr"
            };
            unitOfWork.Insert(configInsert);


            Object actual = context.ChangeTracker.Entries<ConfigEntityTest>().Single().Entity;
            Object expected = configInsert;

            Assert.Equal(EntityState.Added, context.Entry(configInsert).State);
            Assert.Same(expected, actual);

        }

        [Fact]
        public void Select_FromSet()
        {
            context.Add(config);
            context.SaveChanges();

            IEnumerable<TestEntities.ConfigEntityTest> actual = unitOfWork.Select<TestEntities.ConfigEntityTest>();
            IEnumerable<TestEntities.ConfigEntityTest> expected = context.Set<TestEntities.ConfigEntityTest>();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(EntityState.Added, EntityState.Modified)]
        [InlineData(EntityState.Deleted, EntityState.Modified)]
        [InlineData(EntityState.Detached, EntityState.Modified)]
        [InlineData(EntityState.Modified, EntityState.Modified)]
        [InlineData(EntityState.Unchanged, EntityState.Unchanged)]
        public void Update_Entry(EntityState initialState, EntityState state)
        {
            EntityEntry<ConfigEntityTest> entry = context.Entry(config);
            entry.State = initialState;
          
            unitOfWork.Update(config);

            EntityEntry<ConfigEntityTest> actual = entry;

            Assert.Equal(state, actual.State);
            Assert.False(actual.Property(prop => prop.ApplicationName).IsModified);
        }


        [Fact]
        public void Delete_Model_Test()
        {

            context.Add(config);
            context.SaveChanges();

            unitOfWork.Delete<ConfigEntityTest>(config);
            unitOfWork.Commit();

            Assert.Empty(context.Set<ConfigEntityTest>());
        }
        [Fact]
        public void Commit_SavesChanges()
        {
            TestingContext testingContext = Substitute.For<TestingContext>();

            new UnitOfWork(testingContext).Commit();

            testingContext.Received().SaveChanges();
        }



    }
}
