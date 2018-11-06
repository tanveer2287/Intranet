using System;

namespace Core.Domain.Interface
{
    public interface IAuditable
    {
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}