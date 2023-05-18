using Microsoft.EntityFrameworkCore;
using RothschildHouse.Domain.Core.Entities;

namespace RothschildHouse.Application.Core.Common.Contracts
{
    public interface IRothschildHouseDbContext
    {
        DbSet<Country> Country { get; set; }
    }
}
