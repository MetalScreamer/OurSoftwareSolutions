using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Models
{
    class PropertyDefinition
    {
        public long PropertyDefinitionId { get; set; }
        public string Name { get; set; }
        public Guid TypeId { get; set; }
        public bool IsReadOnly { get; set; }
        public string ReadOnlyFormula { get; set; }

        [ForeignKey("OwningClass")]
        public long OwningClassId { get; set; }
        public virtual ClassDefinition OwningClass { get; set; }
    }
}
