using System.Collections.Generic;
using System.Linq;

namespace Oss.Dal.Dtos
{
    class SaveResult : ISaveResult
    {
        IEnumerable<IClassDalDto> classes;
        IEnumerable<IPropertyDalDto> properties;

        public IEnumerable<IClassDalDto> ClassDtos => classes;
        public IEnumerable<IPropertyDalDto> PropertyDtos => properties;

        public SaveResult(IEnumerable<IClassDalDto> classes, IEnumerable<IPropertyDalDto> properties)
        {
            this.classes = classes;
            this.properties = properties;
        }
    }
}
