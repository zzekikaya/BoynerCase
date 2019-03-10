using Boyner.Data;
using Boyner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boyner.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ConfigContext _context { get; }
        public UnitOfWork(ConfigContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Delete<TModel>(TModel model) where TModel : BaseModel
        {
            _context.Remove(model);
        }

        public void Delete<TModel>(int id) where TModel : BaseModel
        {
            throw new NotImplementedException();
        }

        public void DeleteRange<TModel>(IEnumerable<TModel> models) where TModel : BaseModel
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TModel Get<TModel>(int? id) where TModel : BaseModel
        {
            throw new NotImplementedException();
        }

        public TDestination GetAs<TModel, TDestination>(int? id) where TModel : BaseModel
        {
            throw new NotImplementedException();
        }

        public void Insert<TModel>(TModel model) where TModel : BaseModel
        {
            _context.Add(model);
        }

        public Task InsertAsync<TModel>(TModel model) where TModel : BaseModel
        {
            throw new NotImplementedException();
        }

        public void InsertRange<TModel>(IEnumerable<TModel> models) where TModel : BaseModel
        {
            throw new NotImplementedException();
        }

        public TDestination To<TDestination>(object source)
        {
            throw new NotImplementedException();
        }

        public void Update<TModel>(TModel model) where TModel : BaseModel
        {
            EntityEntry<TModel> entry = _context.Entry(model);
            if (entry.State != EntityState.Modified && entry.State != EntityState.Unchanged)
                entry.State = EntityState.Modified;
        }

        public IQuery<TModel> Select<TModel>() where TModel : BaseModel
        {
            return new Query<TModel>(_context.Set<TModel>());
        }

        public TModel GetValue<TModel>(string key)
        {
            var type = typeof(TModel).Name.ToLower();
            var result = _context.Config.Where(x => x.ApplicationName == key).FirstOrDefault();
            string config = result.Value;
            if (type != config.GetType().Name.ToLower()) throw new Exception("Type can not be same");

            switch (config.GetType().Name.ToLower())
            {
                case "string": return (TModel)(object)config;
                case "int": return (TModel)(object)int.Parse(config);
                case "bool": return (TModel)(object)(config == "True");
                default: return default(TModel);
            }
        }
    }
}
