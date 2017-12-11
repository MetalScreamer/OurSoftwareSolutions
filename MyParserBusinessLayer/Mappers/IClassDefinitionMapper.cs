using Oss.Common.ViewDtos;
using Oss.Dal.Dtos;
using System;

namespace Oss.BuisinessLayer.Mappers
{
    public interface IClassDefinitionMapper
    {
        IClassViewDto MapToViewDto(IClassDalDto classDefinition, Guid id);
        IClassDalDto MapToDalDto(IClassViewDto classDefinition, long id);
    }
}
