using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Dtos.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetCallingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            List<Type> list = (from t in assembly.GetExportedTypes()
                               where t.GetInterfaces().Any((i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                               select t).ToList();
            foreach (Type item in list)
            {
                object obj = Activator.CreateInstance(item);
                List<MethodInfo> list2 = (from a in item.GetInterfaces()
                                          where a.Name == "IMapFrom`1"
                                          select a into b
                                          select b.GetMethod("Mapping")).ToList();
                MethodInfo method = item.GetMethod("Mapping");
                if (method != null)
                {
                    list2.Add(method);
                }

                foreach (MethodInfo item2 in list2)
                {
                    item2?.Invoke(obj, new object[1] { this });
                }
            }
        }
    }
}
