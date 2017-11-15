using Oss.Dal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Utilities
{
    static class Extensions
    {
        public static IQueryable<ClassDefinition> IncludePropertiesIfNeeded(this IQueryable<ClassDefinition> classes, bool includeProperties)
        {
            return includeProperties ? classes.Include(c => c.Properties) : classes;
        }
    }
}
