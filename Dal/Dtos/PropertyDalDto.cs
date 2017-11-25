using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Dtos
{
    class PropertyDalDto : IPropertyDalDto
    {
        public long Id { get; set; }
        public bool IsReadOnly { get; set; }
        public string Name { get; set; }
        public string ReadOnlyFormula { get; set; }
        public Guid TypeId { get; set; }
    }
}
