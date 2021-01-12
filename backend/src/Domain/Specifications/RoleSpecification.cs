using Core.Specifications.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Specifications
{
    public class RoleSpecification : BaseSpecification<Role>
    {
        public RoleSpecification() : base(null)
        {
        }

        public RoleSpecification(IEnumerable<long> ids)
            : base(x => ids.Contains(x.Id))
        {
        }

    }
}
