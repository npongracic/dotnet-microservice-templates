using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace SC.API.CleanArchitecture.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public MappingProfile(Assembly assembly)
        {
            ApplyMappingsFromAssembly(assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types) {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping");

                if (methodInfo != null) {
                    methodInfo?.Invoke(instance, new object[] { this });
                }
                else {
                    var methodInfos = type.GetInterfaces().Where(t => t.Name == "IMapFrom`1").Select(t => t.GetMethod("Mapping"));

                    foreach (var mi in methodInfos) {
                        mi?.Invoke(instance, new object[] { this });
                    }
                }               
            }
        }
    }
}