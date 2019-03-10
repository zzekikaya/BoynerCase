using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Boyner.Core.Repository
{
    public interface IQuery<TModel> : IQueryable<TModel>
    {
        IQuery<TResult> Select<TResult>(Expression<Func<TModel, TResult>> selector);
        IQuery<TModel> Where(Expression<Func<TModel, Boolean>> predicate);

        //IQueryable<TView> To<TView>();
    }
}
