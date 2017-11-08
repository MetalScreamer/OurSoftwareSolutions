﻿using System;

namespace Oss.Common.ViewDtos
{
    public interface IDynamicClassPropertyDefinition
    {
        Guid Id { get; }
        string Name { get; set; }
        IType Type { get; set; }
        string Formula { get; set; }
        bool IsReadonly { get; set; }
    }
}