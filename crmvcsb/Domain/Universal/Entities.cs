
/// <summary>
/// reusable entities for repositories
/// </summary>
namespace crmvcsb.Domain.Universal.Entities
{
    using crmvcsb.Domain.Universal.IEntities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EntityGuidIdDAL : IEntityGuidIdDAL
    {
        public Guid Id { get; set; }
    }
    public class EntityIntIdDAL : IEntityIntIdDAL
    {
        public int Id { get; set; }
    }
    public class EntityStringIdDAL : IEntityStringIdDAL
    {
        public string Id { get; set; }
    }


    public class EntityDateDAL : IDateEntityDAL
    {
        public DateTime Date { get; set; }
    }
    public class EntityDateRangeDAL : IDateRangeEntityDAL
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}