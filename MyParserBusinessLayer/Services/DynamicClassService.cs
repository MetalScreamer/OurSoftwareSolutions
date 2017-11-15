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
        private readonly List<IDynamicClassDefinition> removedClassList = new List<IDynamicClassDefinition>();
        private readonly object classesLock = new object();
        private readonly IDynamicClassRepository repo;
        private List<IDynamicClassDefinition> classes;

        private bool isCollectionDirty = false;

        public DynamicClassService(IDynamicClassRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<IDynamicClassDefinition> Classes
        {
            get
            {
                if (classes == null)
                {
                    Refresh(false).Wait();
                }

                lock (classesLock)
                {
                    return classes.AsEnumerable();
                }
            }
        }

        public bool IsDirty
        {
            get
            {
                if (isCollectionDirty) return true;
                lock (classesLock)
                {
                    return (classes?.Any(c => c.IsDirty)).GetValueOrDefault();
                }
            }
        }

        public async Task<IDynamicClassDefinition> AddClass()
        {
            const string DEFAULT_CLASS_NAME_PREFIX = "NewClass";

            await Refresh(false);

            return await Task.Run(
                () =>
                {
                    lock (classesLock)
                    {
                        var classesSnapShot = Classes;
                        int counter = 0;
                        while (classesSnapShot.Any(c => c.Name.Equals($"{DEFAULT_CLASS_NAME_PREFIX}{++counter}"))) ;
                        IDynamicClassDefinition newClass =
                            new DynamicClassDefinition() { Name = $"{DEFAULT_CLASS_NAME_PREFIX}{counter}" };

                        //add the new class to the collection
                        classes.Add(newClass);

                        isCollectionDirty = true;

                        return newClass;
                    }
                });
        }

        public async Task RemoveClass(IDynamicClassDefinition cls)
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

            lock (classesQuery)
            {
                classes = new List<IDynamicClassDefinition>(classesQuery);
                isCollectionDirty = false;
            }
        }

        public async Task SaveChanges()
        {
            if (IsDirty)
            {

            }
        }
    }
}
