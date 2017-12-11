using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Models
{
    public class PropertyDefinition
    {
        internal long PropertyDefinitionId { get; set; }
        internal string Name { get; set; }
        internal Guid TypeId { get; set; }
        internal bool IsReadOnly { get; set; }
        internal string ReadOnlyFormula { get; set; }

        [ForeignKey("OwningClass")]
        internal long OwningClassId { get; set; }
        internal virtual ClassDefinition OwningClass { get; set; }
    }
}
