using System;
using Oss.Dal.Dtos;
using Oss.Dal.Models;
using System.Collections.Generic;
using System.Linq;
using Oss.Common.Interfaces;

namespace Oss.Dal.Utilities
{
    public class ModelDtoMapper : 
        IMapper<IClassDalDto, ClassDefinition>,
        IMapper<IPropertyDalDto, PropertyDefinition>
    {
        public IClassDalDto Map(ClassDefinition classDefinition)
        {
            return new ClassDalDto(classDefinition.ClassDefinitionId)
            {
                Name = classDefinition.Name
            };
        }

        public ClassDefinition Map(IClassDalDto classDto)
        {
            return new ClassDefinition(classDto.Id)
            {
                Name = classDto.Name
            };
        }

        public IPropertyDalDto Map(PropertyDefinition propertyDefinition)
        {
            return new PropertyDalDto()
            {
                Id = propertyDefinition.PropertyDefinitionId,
                Name = propertyDefinition.Name,
                TypeId = propertyDefinition.TypeId,
                ReadOnlyFormula=propertyDefinition.ReadOnlyFormula,
                IsReadOnly=propertyDefinition.IsReadOnly
            };
        }

        public PropertyDefinition Map(IPropertyDalDto propertyDto)
        {
            return new PropertyDefinition()
            {
                PropertyDefinitionId = propertyDto.Id,
                Name = propertyDto.Name,
                TypeId = propertyDto.TypeId,
                ReadOnlyFormula = propertyDto.ReadOnlyFormula,
                IsReadOnly = propertyDto.IsReadOnly
            };
        }
    }

    //internal static class ModelDtoMapper
    //{
    //    internal static IClassDalDto MapClassToDto(this ClassDefinitionModel cls, bool includeProperties)
    //    {
    //        var result = new ClassDalDto(cls.ClassDefinitionId)
    //        {
    //            Name = cls.Name
    //        };

    //        if (includeProperties)
    //        {
    //            var properties = new List<IPropertyDalDto>();
    //            foreach (var prop in cls.Properties)
    //            {
    //                properties.Add(MapPropertyToDto(prop));
    //            }

    //            result.Properties = properties.AsEnumerable();
    //        }

    //        return result;
    //    }

    //    internal static IPropertyDalDto MapPropertyToDto(this PropertyDefinition prop)
    //    {
    //        return new PropertyDalDto()
    //        {
    //            Id = prop.PropertyDefinitionId,
    //            Name = prop.Name,
    //            TypeId = prop.TypeId,
    //            IsReadOnly = prop.IsReadOnly,
    //            ReadOnlyFormula = prop.ReadOnlyFormula
    //        };
    //    }

    //    internal static ClassDefinitionModel MapClassToModel(this IClassDalDto classDto)
    //    {
    //        return new ClassDefinitionModel(classDto.Id) { Name = classDto.Name };
    //    }
    //}
}