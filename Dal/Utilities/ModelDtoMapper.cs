using System;
using Oss.Dal.Dtos;
using Oss.Dal.Models;
using System.Collections.Generic;
using System.Linq;

namespace Oss.Dal.Utilities
{
    internal static class ModelDtoMapper
    {
        internal static IClassDefinition MapClassToDto(this ClassDefinition cls, bool includeProperties)
        {
            var result = new ClassDefinitionDto()
            {
                Id = cls.ClassDefinitionId,
                Name = cls.Name
            };

            if (includeProperties)
            {
                var properties = new List<IPropertyDefinition>();
                foreach (var prop in cls.Properties)
                {
                    properties.Add(MapPropertyToDto(prop));
                }

                result.Properties = properties.AsEnumerable();
            }

            return result;
        }

        internal static IPropertyDefinition MapPropertyToDto(this PropertyDefinition prop)
        {
            return new PropertyDefinitionDto()
            {
                Id = prop.PropertyDefinitionId,
                Name = prop.Name,
                TypeId = prop.TypeId,
                IsReadOnly = prop.IsReadOnly,
                ReadOnlyFormula = prop.ReadOnlyFormula
            };
        }

        internal static ClassDefinition MapClassToModel(this IClassDefinition classDto)
        {
            return new ClassDefinition(classDto.Id) { Name = classDto.Name };
        }
    }
}