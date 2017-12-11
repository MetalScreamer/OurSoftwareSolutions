using Oss.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oss.Common.ViewDtos;
using Oss.Dal.Repositories;
using Oss.BuisinessLayer.ViewDtos;
using Oss.Common.DataStructures;
using Oss.BuisinessLayer.Mappers;

namespace Oss.BuisinessLayer.Services
{
    public class DynamicClassService : IClassService
    {
        private readonly ThreadSafeList<IClassViewDto> removedClassList = new ThreadSafeList<IClassViewDto>();
        private readonly IDynamicClassRepository repo;
        private readonly IClassDefinitionMapper mapper;
        private ThreadSafeList<IClassViewDto> classes = new ThreadSafeList<IClassViewDto>();

        private object dirtyLock = new object();
        private bool isCollectionDirty = false;

        public DynamicClassService(IDynamicClassRepository repo, IClassDefinitionMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public bool IsDirty
        {
            get
            {
                lock (dirtyLock)
                {
                    if (isCollectionDirty) return true;
                }


                return (classes?.Any(c => c.IsDirty)).GetValueOrDefault();
            }
        }

        public async Task<IEnumerable<IClassViewDto>> GetClasses()
        {
            await Refresh(false);

            return classes.ToList();
        }

        public async Task<IClassViewDto> AddClass()
        {
            const string DEFAULT_CLASS_NAME_PREFIX = "NewClass";

            IEnumerable<IClassViewDto> classesSnapShot = classes.ToList();

            return await Task.Run(
                () =>
                {
                    int counter = (classesSnapShot?.Any()).GetValueOrDefault() ? 0 : 1;
                    while (classesSnapShot.Any(c => c.Name.Equals($"{DEFAULT_CLASS_NAME_PREFIX}{++counter}"))) ;

                    var newClass = new ClassViewDto() { Name = $"{DEFAULT_CLASS_NAME_PREFIX}{counter}" };

                    //add the new class to the collection
                    classes.Add(newClass);

                    lock (dirtyLock) isCollectionDirty = true;

                    return newClass;
                });
        }

        public async Task RemoveClass(Guid classId)
        {
            await Task.Run(
                () =>
                {
                    var cls = classes.FirstOrDefault(c => c.Id == classId);
                    if (cls != null)
                    {
                        classes.Remove(cls);
                        removedClassList.Add(cls);
                        lock (dirtyLock) isCollectionDirty = true;
                    }
                });
        }

        public async Task Refresh(bool ignoreChanges)
        {
            if (!ignoreChanges && IsDirty)
            {
                throw new Exception("There are unsaved changes.  Please save or cancel changes.");
            }

            var classesQuery = await GetMappedClassesFromRepo();
            classes.ReLoad(classesQuery);

            lock (dirtyLock)
            {
                isCollectionDirty = false;
            }
        }

        private async Task<IEnumerable<IClassViewDto>> GetMappedClassesFromRepo()
        {
            return
                from cls in await repo.GetClasses()
                select mapper.MapToViewDto(cls, Guid.Empty);
        }

        public async Task SaveChanges()
        {
            if (IsDirty)
            {
                var dirtyClasses = new List<IClassViewDto>();

                foreach (var cls in classes.Where(c => c.IsDirty))
                {
                    dirtyClasses.Add(cls);
                }

                //await repo.Save(dirtyClasses.Select(c => mapper.MapToDalDto(c)));
            }
        }

        public Task UpdateClass(IClassViewDto cls)
        {
           throw new NotImplementedException();
        }

        public Task<IEnumerable<IPropertyDefinition>> GetProperties(Guid classId)
        {
            throw new NotImplementedException();
        }

        public Task<IPropertyDefinition> AddProperty(Guid classId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProperty(IPropertyDefinition property)
        {
            throw new NotImplementedException();
        }

        public Task RemoveProperty(Guid classId, Guid propertyId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IType>> GetTypes()
        {
            return Task.Run(() => PropertyType.Types);
        }

        public Task<IType> GetType(Guid id)
        {
            return Task.Run(() => PropertyType.Types.FirstOrDefault(t => t.Id == id));
        }
    }
}
