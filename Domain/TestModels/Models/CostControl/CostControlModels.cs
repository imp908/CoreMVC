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





    [Table("Dictionary", Schema = "dic")]
    public class DictionaryDB
    {
        [Key]
        [Column("Id_Dictionary")]
        public int IdDictionary { get; set; }

        [Column("Name")]
        public string Name { get; set; }


        public List<DictionaryToDictionaryAttachment> Attachments { get; set; }
    }
    [Table("Dictionary_Attachment", Schema = "dds")]
    public class DictionaryAttachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdDictionaryAttachment { get; set; }
        [Column("Dictionary_Type")]
        public string DictionaryType { get; set; }
        [Column("Id_File")]
        public long? IdFile { get; set; }
        [Column("Rowversion")]
        public byte[] Rowversion { get; set; }
        [Column("Deleted")]
        public bool Deleted { get; set; }
        [Column("Id_Attachment_Kind")]
        public int? IdAttachmentKind { get; set; }
        [Column("Link_Url")]
        public string LinkUrl { get; set; }
        [Column("Link_Name")]
        public string LinkName { get; set; }


        public List<DictionaryToDictionaryAttachment> Attachments { get; set; }
    }
    [Table("Dictionary_Dictionary_Attachment", Schema = "dds")]
    public class DictionaryToDictionaryAttachment
    {
        [Key]
        [Column("Id_Dictionary_Dictionary_Attachment")]
        public long Id_Dictionary_Dictionary_Attachment { get; set; }


        [Column("Id_Dictinary_Type")]
        public int Id_Dictinary_Type { get; set; }
        public DictionaryDB Dictionary { get; set; }


        [Column("Id_Attachment")]
        public long Id_Attachment { get; set; }
        public DictionaryAttachment DictionaryAttachment { get; set; }
    }

}
