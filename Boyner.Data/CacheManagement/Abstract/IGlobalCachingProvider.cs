using System;

namespace Boyner.Core.CacheManagement.Abstract
{
    public interface IGlobalCachingProvider
    {
      
        void AddItem(string key, object value, int refreshTimerIntervalInMs); 

        object GetItem(string key); 
     
    }
}
