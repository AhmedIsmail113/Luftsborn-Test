using AutoMapper;

namespace Luftsborn.Dtos.Common
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(GetType(), typeof(T)).ReverseMap();
        }
    }
}
