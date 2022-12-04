using AutoMapper;
using EShop_Client_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop_Client_Shared.Helpers
{
    public static class Mapper
    {
        //od domain u DTO
        public static UserDto ToDto(this User domainModel)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });

            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<UserDto>(domainModel);
        }

        //od DTO u domain
        public static User ToDomain(this UserDto domainModel)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>();
            });

            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<User>(domainModel);
        }
    }
}
