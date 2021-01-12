using Core.Specifications.Base;
using Domain.Entities;

namespace Domain.Specifications
{
    public class CountrySpecification : BaseSpecification<Country>
    {

        public CountrySpecification() : base(null)
        {
        }

        public CountrySpecification(string countryId)
            : base(x => x.Id == countryId)
        {
        }
    }
}
