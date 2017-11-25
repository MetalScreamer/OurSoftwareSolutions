using System;
using Oss.Common.ViewDtos;
using Oss.Dal.Dtos;

namespace Oss.BuisinessLayer.Mappers
{
    public class ClassDefinitionMapper : IClassDefinitionMapper
    {
        private Func<IClassDalDto> classDalDtoFactory;
        private Func<IClassViewDto> classViewDtoFactory;

        public ClassDefinitionMapper(Func<IClassDalDto> classDalDtoFactory, Func<IClassViewDto> classViewDtoFactory)
        {
            this.classDalDtoFactory = classDalDtoFactory;
            this.classViewDtoFactory = classViewDtoFactory;
        }

        public IClassDalDto MapToDalDto(IClassViewDto classDefinition)
        {
            var result = classDalDtoFactory();

            result.Name = classDefinition.Name;
            

            return result;
        }

        public IClassViewDto MapToViewDto(IClassDalDto classDefinition)
        {
            var result = classViewDtoFactory();

            return result;
        }      
    }
}
