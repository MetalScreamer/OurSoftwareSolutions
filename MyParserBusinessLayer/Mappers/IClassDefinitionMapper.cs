using Oss.Common.ViewDtos;
using Oss.Dal.Dtos;

namespace Oss.BuisinessLayer.Mappers
{
    public interface IClassDefinitionMapper
    {
        IClassViewDto MapToViewDto(IClassDalDto classDefinition);
        IClassDalDto MapToDalDto(IClassViewDto classDefinition);
    }
}
