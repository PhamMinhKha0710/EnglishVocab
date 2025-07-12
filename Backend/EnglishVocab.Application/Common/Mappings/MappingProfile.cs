using AutoMapper;

namespace EnglishVocab.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Đăng ký các profile riêng biệt
            ApplyMappingsFromAssembly();
        }

        private void ApplyMappingsFromAssembly()
        {
            // Các profile riêng biệt sẽ được tự động đăng ký bởi AutoMapper
            // khi chúng kế thừa từ Profile
        }
    }
} 