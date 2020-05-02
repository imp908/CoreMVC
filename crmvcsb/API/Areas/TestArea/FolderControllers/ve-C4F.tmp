

namespace crmvcsb.Default.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    using crmvcsb.Domain.Universal.IRepository;
    using crmvcsb.Domain.Blogging.API;
    using crmvcsb.Infrastructure.EF.Blogging;

    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        ICQRSBloggingWrite _cqrs;
        
        ICQRSBloggingRead _cqrsRead;

        public BlogController(ICQRSBloggingWrite cqrs,ICQRSBloggingRead cqrsRead)
        {
            _cqrs = cqrs;
            _cqrsRead = cqrsRead;
        }


        [HttpPost("GetPostsByPerson")]
        public JsonResult GetByQuery([FromBody] GetPostsByPerson query)
        {
            var result = _cqrsRead.Get(query);
            return Json(result);
        }
        [HttpPost("GetPostsByBlog")]
        public JsonResult GetByQuery([FromBody] GetPostsByBlog query)
        {
            var result = _cqrsRead.Get(query);
            return Json(result);
        }
        [HttpPost("GetBlogsByPerson")]
        public JsonResult GetByQuery([FromBody] GetBlogsByPerson query)
        {
            var result = _cqrsRead.Get(query);
            return Json(result);
        }        



        /*Adding post returning Ok(result) and Json(result)*/
        [HttpPost("AddPost")]
        public ActionResult<PostEF> AddPost([FromBody] PersonAdsPostCommand value)
        {
            var result = _cqrs.PersonAdsPostToBlog(value);
            return Ok(result);
        }

        [HttpPost("AddPostJSON")]
        public JsonResult AddPostJSON([FromBody] PersonAdsPostCommand value)
        {
            var result = _cqrs.PersonAdsPostToBlog(value);
            return Json(result);
        }

        [HttpPut("UpdatePost")]
        public JsonResult Put([FromBody] PersonUpdatesBlog command)
        {
            var result = _cqrs.PersonUpdatesPost(command);
            return Json(result);
        }
        [HttpPost("DeletePost")]
        public JsonResult Post([FromBody] PersonDeletesPost command)
        {
            var result = _cqrs.PersonDeletesPostFromBlog(command);
            if(result){
                return Json(new {Removed = true});
            }
            return Json(new {Removed = false});
        }
    }
}
