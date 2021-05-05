using AutoMapper;
using SC.API.CleanArchitecture.Application.Common.Mappings;
using SC.API.CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Application.Organization.Queries.Organizations
{
    public class OrganizationDto : IMapFrom<PartyRole>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.PartyRole, OrganizationDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Code, o => o.MapFrom(s => s.JobCode))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.JobTitle));
        }
    }
}
