using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Dtos
{
    public class ClassDalDto : IClassDalDto
    {
        public long Id { get; }
        public string Name { get; set; }
        public IEnumerable<IPropertyDalDto> Properties { get; set; }

        public ClassDalDto(long id) { Id = id; }
    }
}
