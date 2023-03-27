using AutoMapper;
using Isracard.Auth.Application.Services.Dtos;
using Isracard.Auth.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isracard.Auth.Application.Services.MappingProfile
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<LoginDTO, Login>().ReverseMap();
        }
    }
}
