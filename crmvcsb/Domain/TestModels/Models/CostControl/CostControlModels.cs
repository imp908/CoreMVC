using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace crmvcsb.Domain.TestModels.Models.CostControl
{
    public interface IidEntity
    {
        int Id { get; set; }
    }

    public class IdEntity : IidEntity
    {
        public int Id { get; set; }
    }



    [Table("Business_Columns", Schema = "dic")]
    public class BusinessColumn : IdEntity, IidEntity
    {
        [Column("Id_Business_Column")]
        public new int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("ShortName")]
        public string ShortName { get; set; }
        [Column("Code")]
        public string Code { get; set; }
        [Column("Status_Id")]
        public int StatusId { get; set; }
        [Column("Code")]
        public string Comment { get; set; }
        [Column("Is_Use_In_Request")]
        public string UseInPaymentsRequest { get; set; }


        public List<BusinessLine> BusinessLines { get; set; }
    }


    [Table("Business_Lines", Schema = "dic")]
    public class BusinessLine : IdEntity, IidEntity
    {
        [Column("Id_Business_Line")]
        public new int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("ShortName")]
        public string ShortName { get; set; }
        [Column("Code")]
        public string Code { get; set; }
        [Column("Status_Id")]
        public int StatusId { get; set; }
        [Column("Code")]
        public string Comment { get; set; }
        [Column("Is_Use_In_Request")]
        public string UseInPaymentsRequest { get; set; }


        public BusinessColumn BusinessColumn { get; set; }
        public int BusinessColumnId { get; set; }
    }
}
