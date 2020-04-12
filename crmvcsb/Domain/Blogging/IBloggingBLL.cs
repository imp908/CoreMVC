
namespace crmvcsb.Domain.Blogging.BLL
{

    using System.Collections.Generic;
    using System;


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

    public interface IAddPostBLL
    {
        Guid PersonId { get; set; }
        int BlogId { get; set; }
        IPostBLL PostPayload { get; set; }
    }

}