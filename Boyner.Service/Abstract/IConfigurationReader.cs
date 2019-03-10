using Boyner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boyner.Service.Abstract
{
    public interface IConfigurationReader
    {
        T GetValue<T>(string key);
    }

}
