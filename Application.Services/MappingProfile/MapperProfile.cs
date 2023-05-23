using AutoMapper;
using RWS.Authentication.Application.Services.Dtos;
using RWS.Authentication.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWS.Authentication.Application.Services.MappingProfile
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<LoginDTO, Login>().ReverseMap();
        }
    }
}
