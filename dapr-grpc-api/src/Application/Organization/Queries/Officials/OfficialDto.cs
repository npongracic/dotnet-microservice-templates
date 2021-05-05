using AutoMapper;
using SC.API.CleanArchitecture.Application.Common.Mappings;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SC.API.CleanArchitecture.Application.Organization.Queries.Officials
{
    public class OfficialDto : IMapFrom<PartyRole>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.PartyRole, OfficialDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.JobTitle))
                .ForMember(d => d.Code, o => o.MapFrom(s => s.JobCode));
        }
    }
}
