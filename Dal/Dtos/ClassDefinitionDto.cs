﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Dtos
{
    class ClassDefinitionDto : IClassDefinition
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<IPropertyDefinition> Properties { get; set; }
    }
}
