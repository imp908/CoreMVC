using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using crmvcsb.Infrastructure.EF;
using crmvcsb.Infrastructure.EF.newOrder;
using crmvcsb.Domain.Interfaces;
using AutoMapper;
using crmvcsb.Domain.NewOrder.API;

namespace crmvcsb.API.Areas.TestArea.FolderControllers
{
    [Area("NewOrder")]
    [Route("Currency")]
    public class NewOrderController
    {
     

        public NewOrderController() {

        }

        [HttpGet("Get")]
        public IActionResult GetCurrency(){
            try{

            }catch(Exception e){
                
            }
        }
    }
}