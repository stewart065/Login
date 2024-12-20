using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using login.Entities;
using login.Identity;

namespace login.Profiles
{
    public class SampleAppProfile : Profile
    {
        public SampleAppProfile()
        {
            CreateMap<AspNetUser, ApplicationUser>();
        }
    }
}