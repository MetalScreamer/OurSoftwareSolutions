using Oss.Dal.Models;
using System.Data.Entity;

namespace Oss.Dal.Database
{
    class OssDbContext : DbContext
    {
        public virtual DbSet<ClassDefinition> Classes { get; set; }
        public virtual DbSet<PropertyDefinition> Properties { get; set; }

        public OssDbContext() : base("Oss") { }
    }
}
