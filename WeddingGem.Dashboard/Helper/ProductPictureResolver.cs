using AutoMapper;
using WeddingGem.API.DTOs.Services;
using WeddingGem.Data.Entites.services;

namespace WeddingGem.Dashboard.Helper
{
    public class ProductPictureResolver<T,TEntity>: IValueResolver<T, TEntity, string>  where T : WeddingGem.Data.Entites.services.Items
    {
        private readonly IConfiguration _configuration;

        public ProductPictureResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(T source, TEntity destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImgUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.ImgUrl}";
            }
            return string.Empty ;
        }
    }
}
