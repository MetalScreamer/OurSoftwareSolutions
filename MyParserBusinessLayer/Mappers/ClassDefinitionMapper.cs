using System;
using Oss.Common.ViewDtos;
using Oss.Dal.Dtos;

namespace Oss.BuisinessLayer.Mappers
{
    public class ClassDefinitionMapper : IClassDefinitionMapper
    {
        private ClassDalDtoFactory classDalDtoFactory;
        private ClassViewDtoFactory classViewDtoFactory;

        public ClassDefinitionMapper(ClassDalDtoFactory classDalDtoFactory, ClassViewDtoFactory classViewDtoFactory)
        {
            this.classDalDtoFactory = classDalDtoFactory;
            this.classViewDtoFactory = classViewDtoFactory;
        }

        public IClassDalDto MapToDalDto(IClassViewDto classDefinition, long id)
        {
            var result = classDalDtoFactory(id);

            result.Name = classDefinition.Name;
            

            return result;
        }

        public IClassViewDto MapToViewDto(IClassDalDto classDefinition, Guid id)
        {
            var result = classViewDtoFactory(id);
            result.Name = classDefinition.Name;

            return result;
        }      
    }
}
