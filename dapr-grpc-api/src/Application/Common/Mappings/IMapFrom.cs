using AutoMapper;

namespace SC.API.CleanArchitecture.Application.Common.Mappings
{
    public interface IMapFrom
    {

    }

    public interface IMapFrom<T> : IMapFrom
    {
        void Mapping(Profile profile)
        { 
            profile.CreateMap(typeof(T), GetType()); 
        }
    }
}
