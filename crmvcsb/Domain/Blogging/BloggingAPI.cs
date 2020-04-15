
namespace crmvcsb.Domain.Blogging.API
{

    using crmvcsb.Domain.Blogging.BLL;

    using System.Collections.Generic;
    using System;

    public class AddPostAPI : IAddPostBLL
    {
        public Guid PersonId { get; set; }
        public int BlogId { get; set; }
        public IPostBLL PostPayload { get; set; }
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
        public Guid PersonId { get; set; }
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


}
