using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Interface;

namespace Core.Domain
{
    public abstract class EntityBase<TId> :IEntity<TId>
    {
        public TId Id { get; set; }
    }
}
