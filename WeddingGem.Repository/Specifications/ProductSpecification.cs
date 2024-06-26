using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites.services;
using WeddingGem.Repository.Parames;
using WeddingGem.Repository.Params;

namespace WeddingGem.Repository.Specifications
{
    public class ProductSpecification<T>:BaseSpecification<T> where T:Items
    {
        public ProductSpecification(Expression<Func<T, T>> selector = null) :base()
        {
            Includes.Add(p => p.UserService);
            Includes.Add(p => p.Biddings);
            if (selector != null)
            {
                ApplySelect(selector);
            }
        }
        public ProductSpecification(BaseProductParams specs)
           : base(p =>
                   !string.IsNullOrEmpty(specs.search)
                    ? p.Name.ToLower().Contains(specs.search.ToLower())
                    : true)
        {
            Includes.Add(p => p.UserService);
            Includes.Add(p => p.Biddings);
            if (specs.OrderBy == "PriceAsc") { addOrderBy(p => p.Price); }
            else if (specs.OrderByDesc == "PriceDesc") { addOrderByDesc(p => p.Price); }
            else { addOrderBy(p => p.Name); };
        }
        public ProductSpecification(WeddingSpecsParams specs)
           : base(p =>
                   !string.IsNullOrEmpty(specs.HallType)
                    ? ((WeddingHall)(object)p).HallType == specs.HallType
                    : true &&
                    !string.IsNullOrEmpty(specs.search)
                    ? ((WeddingHall)(object)p).Name.ToLower().Contains(specs.search)
                    : true
           )
        {
            Includes.Add(p => p.UserService);
            Includes.Add(p => p.Biddings);
            if (specs.OrderBy == "PriceAsc") { addOrderBy(p => p.Price); }
            else if (specs.OrderByDesc == "PriceDesc") { addOrderByDesc(p => p.Price); }
            else{ addOrderBy(p => p.Name); };
            if (specs.Capacity != null)
            {
                if (specs.Capacity == "CapacityAsc") { addOrderBy(p => ((WeddingHall)(object)p).Capacity); }
                else if (specs.Capacity == "CapacityDesc") { addOrderByDesc(p => ((WeddingHall)(object)p).Capacity); }
            }
        }
        public ProductSpecification(HoneyMoonSepcsParam specs)
           : base(p =>
                   !string.IsNullOrEmpty(specs.search)
                    ? p.Name.ToLower().Contains(specs.search.ToLower())
                    : true && 
                    !string.IsNullOrEmpty(specs.Distination) ?
                    ((HoneyMoon)(object)p).Destination.ToLower().Contains(specs.Distination)
                    : true
           )
        {
            Includes.Add(p => p.UserService);
            Includes.Add(p => p.Biddings);
            if (specs.OrderBy == "PriceAsc") { addOrderBy(p => p.Price); }
            else if (specs.OrderByDesc == "PriceDesc") { addOrderByDesc(p => p.Price); }
            else{ addOrderBy(p => p.Name); };
        }
        public ProductSpecification(EntertainmentSpecsParam specs)
           : base(p =>
                   !string.IsNullOrEmpty(specs.search)
                    ? p.Name.ToLower().Contains(specs.search.ToLower())
                    : true && 
                    !string.IsNullOrEmpty(specs.TypeBand) ?
                    ((Entertainment)(object)p).TypeBand.ToLower().Contains(specs.TypeBand)
                    : true
           )
        {
            Includes.Add(p => p.UserService);
            Includes.Add(p => p.Biddings);
            if (specs.OrderBy == "PriceAsc") { addOrderBy(p => p.Price); }
            else if (specs.OrderByDesc == "PriceDesc") { addOrderByDesc(p => p.Price); }
            else{ addOrderBy(p => p.Name); };
        }
        public ProductSpecification(SelfCareSpecsParams specs)
           : base(p =>
                   !string.IsNullOrEmpty(specs.search)
                    ? p.Name.ToLower().Contains(specs.search.ToLower())
                    : true &&
                    !string.IsNullOrEmpty(specs.Type) ?
                    ((SelfCare)(object)p).Type.ToLower().Contains(specs.Type)
                    : true
           )
        {
            Includes.Add(p => p.UserService);
            Includes.Add(p => p.Biddings);
            if (specs.OrderBy == "PriceAsc") { addOrderBy(p => p.Price); }
            else if (specs.OrderByDesc == "PriceDesc") { addOrderByDesc(p => p.Price); }
            else { addOrderBy(p => p.Name); };
        }
        public ProductSpecification(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.UserService);
            Includes.Add(p => p.Biddings);
        }
        public ProductSpecification(Expression<Func<T, bool>> criteria)
        : base(criteria)
        {
        }
    }
}
