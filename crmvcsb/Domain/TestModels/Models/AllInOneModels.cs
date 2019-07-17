

namespace crmvcsb.Domain.TestModels
{

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System;

    public interface IBlogEF
    {
        int BlogId { get; set; }
        string Url { get; set; }
        int Rating { get; set; }
    }
    public interface IPostEF
    {
        int PostId { get; set; }
        string Title { get; set; }
        string Content { get; set; }

        int BlogId { get; set; }
    }


    /**BLL layer models */
    public interface IBlogBLL
    {
        int Id { get; set; }
        string Url { get; set; }
        int Rating { get; set; }

        IList<IPostBLL> Posts { get; set; }
    }
    public interface IPostBLL
    {
        int PostId { get; set; }
        string Title { get; set; }
        string Content { get; set; }

        IBlogBLL Blog { get; set; }

    }




    /*Commands layer */
    /*Object parameter / command object to pass to CQRRS */
    /*flattering needed */
    public interface IAddPostBLL
    {
        Guid PersonId { get; set; }
        int BlogId { get; set; }
        PostBLL PostPayload { get; set; }
    }

    public class AddPostAPI : IAddPostBLL
    {
        public Guid PersonId { get; set; }
        public int BlogId { get; set; }
        public PostBLL PostPayload { get; set; }
    }



    /* Model for post addition without flattering */
    public class PersonAdsPostCommand
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }

        public Guid PersonId { get; set; }

    }
    
  
    public class GetPostsByPerson
    {
        public Guid PersonId {get;set;}
    }
    public class GetPostsByBlog
    {
        public int BlogId { get; set; }
    }
    public class GetBlogsByPerson
    {
        public Guid PersonId { get; set; }
    }

    public class PersonDeletesPost
    {
        public Guid PersonId { get; set; }
        public int PostId { get; set; }
    }
    public class PersonUpdatesBlog
    {
        public Guid PersonId { get; set; }
        public PostAPI Post { get; set; }
    }


    /*API level models */
    public class PersonAPI
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class BlogAPI
    {
        public string Url { get; set; }
        public int Rating { get; set; }

    }
    public class PostAPI
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public BlogAPI Blog { get; set; }

        public PersonAPI Author { get; set; }
    }



    public class BlogBLL : IBlogBLL
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }

        public IList<IPostBLL> Posts { get; set; }
    }

    public class PostBLL : IPostBLL
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public IBlogBLL Blog { get; set; }

    }



    /**Specific EF concrete type realizations */
    //one-to-many 0->8
    public class BlogEF : IBlogEF
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

    public class PostEF : IPostEF
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
        public Guid ServiceId{get;set;}
    }

}
