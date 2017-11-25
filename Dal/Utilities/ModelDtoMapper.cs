using System;
using Oss.Dal.Dtos;
using Oss.Dal.Models;
using System.Collections.Generic;
using System.Linq;

namespace Oss.Dal.Utilities
{
    internal static class ModelDtoMapper
    {
        internal static IClassDalDto MapClassToDto(this ClassDefinition cls, bool includeProperties)
        {
            var result = new ClassDalDto(cls.ClassDefinitionId)
            {
                Name = cls.Name
            };

            if (includeProperties)
            {
                var properties = new List<IPropertyDalDto>();
                foreach (var prop in cls.Properties)
                {
                    properties.Add(MapPropertyToDto(prop));
                }

                result.Properties = properties.AsEnumerable();
            }

            return result;
        }

        internal static IPropertyDalDto MapPropertyToDto(this PropertyDefinition prop)
        {
            return new PropertyDalDto()
            {
                Id = prop.PropertyDefinitionId,
                Name = prop.Name,
                TypeId = prop.TypeId,
                IsReadOnly = prop.IsReadOnly,
                ReadOnlyFormula = prop.ReadOnlyFormula
            };
        }

        internal static ClassDefinition MapClassToModel(this IClassDalDto classDto)
        {
            return new ClassDefinition(classDto.Id) { Name = classDto.Name };
        }
    }
}