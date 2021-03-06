﻿using Oss.BuisinessLayer.SyntaxHelpers;
using Oss.Common.ViewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.BuisinessLayer.ViewDtos
{
    public class ClassViewDto : IClassViewDto
    {
        private List<IPropertyDefinition> properties = new List<IPropertyDefinition>();

        public Guid Id { get; }

        public bool IsDirty
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name { get; set; }
        public IEnumerable<IPropertyDefinition> Properties { get { return properties.AsEnumerable(); } }

        public ClassViewDto() : this(Guid.NewGuid()) { }
        public ClassViewDto(Guid id) { Id = id; }
    }
}
