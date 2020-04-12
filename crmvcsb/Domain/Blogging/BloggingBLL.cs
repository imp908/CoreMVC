
namespace crmvcsb.Domain.Blogging.BLL
{
    
    using System.Collections.Generic;

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
}
