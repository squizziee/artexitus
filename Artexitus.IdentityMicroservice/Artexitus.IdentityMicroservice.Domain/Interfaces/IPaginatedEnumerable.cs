﻿using Artexitus.IdentityMicroservice.Domain.Entities;

namespace Artexitus.IdentityMicroservice.Domain.Interfaces
{
    public interface IPaginatedEnumerable<TEntity> where TEntity : EntityBase
    {
        IEnumerable<TEntity> Items { get; set; }
        int PageNumber { get; set; }
        int TotalPages { get; set; }
    }
}
