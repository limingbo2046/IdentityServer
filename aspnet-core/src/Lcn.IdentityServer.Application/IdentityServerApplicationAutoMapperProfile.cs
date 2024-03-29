﻿using AutoMapper;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;


namespace Lcn.IdentityServer
{
    public class IdentityServerApplicationAutoMapperProfile : Profile
    {
        public IdentityServerApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<IdentityUser, ProfileDto>().Ignore(x => x.EmployeNo);
        }
    }
}
