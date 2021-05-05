using AutoMapper;
using SC.API.CleanArchitecture.Application.Common.Mappings;
using SC.API.CleanArchitecture.Domain.Entities;

namespace SC.API.CleanArchitecture.Application.Common.Models
{
    public class FileResult : IMapFrom<Document>
    {
        public byte[] Content { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Document, FileResult>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Binary));
        }
    }
}
