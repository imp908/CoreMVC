

namespace crmvcsb.Infrastructure.EF.Blogging
{

    using System;
    using System.Collections.Generic;    
    using System.ComponentModel.DataAnnotations;
    using crmvcsb.Domain.DomainSpecific.Blogging.DAL;

    /**Specific EF concrete type realizations */
    //one-to-many 0->8
    public class BlogEF : IBlogDAL
    {
        [Key]
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }

        public DateTime Created { get; set; }


        //Ignore via Fluent API exclude from DB
        public bool LoadedFromDatabase { get; set; }

        public BlogImage BlogImage { get; set; }

        public IList<PostEF> Posts { get; set; }
    }
    public class BlogImage
    {
        public int BlogImageId { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }

        //foreign key
        public int BlogId { get; set; }

        public BlogEF Blog { get; set; }
    }

    public class ExludeFromDBentity
    {
        public Guid Id { get; set; }
        public string Prop { get; set; }
    }

    public class PostEF : IPostDAL
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public BlogEF Blog { get; set; }

        public Guid AuthorId { get; set; }
        public PersonEF Author { get; set; }


        public List<PostTagEF> PostTags { get; set; }
    }

    //Many-to-Many
    //oo<->oo
    public class TagEF
    {
        public string TagId { get; set; }

        public List<PostTagEF> PostTags { get; set; }
    }

    public class PostTagEF
    {
        public int PostId { get; set; }
        public PostEF Post { get; set; }

        public string TagId { get; set; }
        public TagEF Tag { get; set; }
    }

    public class PersonEF
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public IList<PostEF> Posts { get; set; }

    }

    //entity for TPH inheritance implementation via discriminator column
    public class StudentEF : PersonEF
    {
        public DateTime EnrollmentDate { get; set; }
    }

    /**TPT and TPC strategies are not implemented in EF core yet and ToTable not works for TPC */
    public class InstructorEF
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int QualityGrade { get; set; }
    }

    public class ServiceType
    {
        public Guid ServiceId { get; set; }
    }

}
