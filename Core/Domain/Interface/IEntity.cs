using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Interface
{
    public interface IEntity
    {
    }
    public interface IEntity<out TId> : IEntity
    {
        TId Id { get; }
    }
}
