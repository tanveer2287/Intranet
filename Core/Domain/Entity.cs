using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Interface;

namespace Core.Domain
{
    public abstract class Entity : Entity<Guid>, IAuditable
    {
    }
    public abstract class Entity<TId> : EntityBase<TId>, IAuditable
    {
        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
