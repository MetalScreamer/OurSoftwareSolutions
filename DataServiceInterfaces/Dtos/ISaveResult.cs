using System.Collections.Generic;

namespace Oss.Dal.Dtos
{
    public interface ISaveResult
    {
        IEnumerable<IClassDalDto> ClassDtos { get; }
        IEnumerable<IPropertyDalDto> PropertyDtos { get; }
    }
}