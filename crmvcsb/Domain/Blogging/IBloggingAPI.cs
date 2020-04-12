
namespace crmvcsb.Domain.Blogging.API
{
    using System;

    using crmvcsb.Domain.Blogging.BLL;

    /*Commands layer */
    /*Object parameter / command object to pass to CQRRS */
    /*flattering needed */
    public interface IAddPostAPI
    {
        Guid PersonId { get; set; }
        int BlogId { get; set; }
        PostBLL PostPayload { get; set; }
    }

}