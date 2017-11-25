using Oss.Dal.Database;
using Oss.Dal.Dtos;
using Oss.Dal.Models;
using Oss.Dal.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Oss.Dal.Repositories
{
    public class DynamicClassRepository : IDynamicClassRepository
    {
        public Task<IEnumerable<IClassDalDto>> Find(string nameIsLike, bool includeProperties)
        {
            return Task.Run(
                () =>
                {
                    using (var db = new OssDbContext())
                    {
                        return db.Classes
                            .IncludePropertiesIfNeeded(includeProperties)
                            .Where(c => c.Name.Contains(nameIsLike))
                            .Select(c => c.MapClassToDto(includeProperties))
                            .AsEnumerable();
                    }
                });

        }

        public async Task<IEnumerable<IClassDalDto>> GetClasses(bool includeProperties)
        {
            var classModels = await Task.Run(
                () =>
                {
                    using (var db = new OssDbContext())
                    {
                        return db.Classes
                            .IncludePropertiesIfNeeded(includeProperties)
                            .ToList();
                    }
                });

            return classModels.Select(c => c.MapClassToDto(includeProperties));
        }

        public Task<IClassDalDto> Get(long id, bool includeProperties)
        {
            using (var db = new OssDbContext())
            {
                return
                    db.Classes
                    .IncludePropertiesIfNeeded(includeProperties)
                    .FirstOrDefaultAsync(c => c.ClassDefinitionId == id)
                    .ContinueWith(t => t.Result.MapClassToDto(includeProperties));
            }
        }

        public Task<IEnumerable<IPropertyDalDto>> GetProperties(IClassDalDto classDto)
        {
            return Task.Run(
                () =>
                {
                    using (var db = new OssDbContext())
                    {
                        return
                            db.Properties
                            .Where(p => p.OwningClassId == classDto.Id)
                            .Select(p => p.MapPropertyToDto())
                            .AsEnumerable();
                    }
                });
        }

        public Task<IPropertyDalDto> GetProperty(long id)
        {
            return Task.Run(
                () =>
                {
                    using (var db = new OssDbContext())
                    {
                        return
                            (
                                from p in db.Properties
                                where p.PropertyDefinitionId == id
                                select p.MapPropertyToDto()
                            ).SingleOrDefault();
                    }

                });
        }

        public Task Save(IEnumerable<IClassDalDto> classDtos, IEnumerable<IPropertyDalDto> propertyDtos)
        {
            return Task.Run(
                () =>
                {
                    using (var db = new OssDbContext())
                    {
                        //ClassDefinition cls = ModelDtoMapper.MapClassToModel(classDto);
                        //var entry = db.Entry(cls);
                        //entry.State = EntityState.Modified;
                        db.SaveChangesAsync();
                    }
                });

        }

        public Task Delete(IEnumerable<IClassDalDto> classDtos = null, IEnumerable<IPropertyDalDto> propertyDtos = null)
        {
            return Task.Run(
                () =>
                {
                    if (classDtos != null || propertyDtos != null)
                    {

                        using (var db = new OssDbContext())
                        {
                            if (classDtos != null)
                            {
                                foreach (var classDto in classDtos)
                                {
                                    var classModel = new ClassDefinition(classDto.Id);
                                    var entry = db.Entry(classModel);
                                    entry.State = EntityState.Deleted;
                                }
                            }

                            if (propertyDtos != null)
                            {
                                foreach (var propertyDto in propertyDtos)
                                {
                                    var propertyModel = new PropertyDefinition() { PropertyDefinitionId = propertyDto.Id };
                                    var entry = db.Entry(propertyModel);
                                    entry.State = EntityState.Deleted;
                                }
                            }

                            db.SaveChangesAsync();
                        }
                    }
                });
        }
    }
}
