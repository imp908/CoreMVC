using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Interfaces for generic IRepository entities
/// </summary>
namespace crmvcsb.Domain.IEntities
{
    public interface IEntityGuidIdDAL
    {
        Guid Id { get; set; }
    }
    public interface IEntityIntIdDAL
    {
        int Id { get; set; }
    }
    public interface IEntityStringIdDAL
    {
        string Id { get; set; }
    }

    public interface IDateEntityDAL
    {
        DateTime Date { get; set; }
    }
    public interface IDateRangeEntityDAL
    {
        DateTime DateFrom { get; set; }
        DateTime DateTo { get; set; }
    }

}