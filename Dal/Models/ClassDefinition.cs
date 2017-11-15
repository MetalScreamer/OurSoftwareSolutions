using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Models
{
    class ClassDefinition
    {
        public long ClassDefinitionId { get; private set; }
        public string Name { get; set; }
        public virtual List<PropertyDefinition> Properties { get; private set; } = new List<PropertyDefinition>();

        public ClassDefinition(long id) { ClassDefinitionId = id; }
    }
}
