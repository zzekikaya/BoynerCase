﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks; 
using Boyner.Domain.Entities;

namespace Boyner.Core.Repository
{
    public interface IUnitOfWork: IDisposable
    { 
        TDestination GetAs<TModel, TDestination>(Int32? id) where TModel : BaseModel;
        TModel Get<TModel>(Int32? id) where TModel : BaseModel;
        TDestination To<TDestination>(Object source);

        IQuery<TModel> Select<TModel>() where TModel : BaseModel;

        void InsertRange<TModel>(IEnumerable<TModel> models) where TModel : BaseModel;
        void Insert<TModel>(TModel model) where TModel : BaseModel;
        Task InsertAsync<TModel>(TModel model) where TModel : BaseModel;
        void Update<TModel>(TModel model) where TModel : BaseModel;

        void DeleteRange<TModel>(IEnumerable<TModel> models) where TModel : BaseModel;
        void Delete<TModel>(TModel model) where TModel : BaseModel;
        void Delete<TModel>(Int32 id) where TModel : BaseModel;

        TModel GetValue<TModel>(string key);

        void Commit();

    }
}
