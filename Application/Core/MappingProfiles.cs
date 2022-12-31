using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity,Activity>(); //Activity pertama untuk get Activity kedua untuk passing back
        }
    }
}