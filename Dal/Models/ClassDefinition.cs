using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Models
{
    public class ClassDefinition
    {
        internal long ClassDefinitionId { get; private set; }
        internal string Name { get; set; }
        internal virtual List<PropertyDefinition> Properties { get; private set; } = new List<PropertyDefinition>();

        public ClassDefinition() { }
        public ClassDefinition(long id) { ClassDefinitionId = id; }
    }
}
