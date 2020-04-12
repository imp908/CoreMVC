
namespace crmvcsb.Domain.Blogging.DAL
{

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System;

    public interface IBlogDAL
    {
        int BlogId { get; set; }
        string Url { get; set; }
        int Rating { get; set; }
    }
    public interface IPostDAL
    {
        int PostId { get; set; }
        string Title { get; set; }
        string Content { get; set; }

        int BlogId { get; set; }
    }


}
