using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Boyner.Domain.Entities;
using Boyner.Web.UI.Models;

namespace Boyner.Web.UI.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Config, ConfigModel>();
            CreateMap< ConfigModel, Config>();
        }
    }
}
