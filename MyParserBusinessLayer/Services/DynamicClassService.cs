using Oss.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oss.Common.ViewDtos;
using Oss.Dal.Repositories;
using Oss.BuisinessLayer.Extentions;
using Oss.BuisinessLayer.ViewDtos;

namespace Oss.BuisinessLayer.Services
{
    public class DynamicClassService : IDynamicClassService
    {
        private readonly List<IClassDefinition> removedClassList = new List<IClassDefinition>();
        private readonly object classesLock = new object();
        private readonly IDynamicClassRepository repo;
        private List<IClassDefinition> classes;

        private object dirtyLock = new object();
        private bool isCollectionDirty = false;

        public DynamicClassService(IDynamicClassRepository repo)
        {
            this.repo = repo;
        }

        //Do I need/want this?
        //public IEnumerable<IClassDefinition> Classes
        //{
        //    get
        //    {
        //       return GetClasses().Result;                
        //    }
        //}

        public bool IsDirty
        {
            get
            {
                lock (dirtyLock)
                {
                    if (isCollectionDirty) return true;
                }

                lock (classesLock)
                {
                    return (classes?.Any(c => c.IsDirty)).GetValueOrDefault();
                }
            }
        }

        public async Task<IEnumerable<IClassDefinition>> GetClasses()
        {
            await Refresh(false);

            lock (classesLock)
            {
                return classes.AsEnumerable();
            }
        }

        public async Task<IClassDefinition> AddClass()
        {
            const string DEFAULT_CLASS_NAME_PREFIX = "NewClass";

            await Refresh(false);

            return await Task.Run(
                () =>
                {

                    lock (classesLock)
                    {
                        var classesSnapShot = classes;
                        int counter = 0;
                        while (classesSnapShot.Any(c => c.Name.Equals($"{DEFAULT_CLASS_NAME_PREFIX}{++counter}"))) ;
                        IClassDefinition newClass =
                            new DynamicClassDefinition() { Name = $"{DEFAULT_CLASS_NAME_PREFIX}{counter}" };

                        //add the new class to the collection
                        classes.Add(newClass);

                        isCollectionDirty = true;

                        return newClass;
                    }
                });
        }

        public async Task RemoveClass(IClassDefinition cls)
        {
            await Task.Run(
                () =>
                {
                    lock (classesLock)
                    {
                        classes.Remove(cls);
                        removedClassList.Add(cls);
                        isCollectionDirty = true;
                    }
                });
        }

        public async Task Refresh(bool ignoreChanges)
        {
            if (!ignoreChanges && IsDirty)
            {
                throw new Exception("There are unsaved changes.  Please save or cancel changes.");
            }

            var classesQuery =
                        from cls in await repo.GetClasses()
                        select cls.Map();

            lock (classesLock)
            lock (dirtyLock)
            {
                classes = new List<IClassDefinition>(classesQuery);
                isCollectionDirty = false;
            }
        }

        public async Task SaveChanges()
        {
            if (IsDirty)
            {
                lock (classesLock)
                {
                    foreach(var cls in classes)
                    {
                        if (cls.IsDirty)
                        {
                            repo.Save();
                        }
                    }
                }
                
            }
        }

        public Task UpdateClass(IClassDefinition cls)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClass(Guid classId)
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
    }
}
