
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Newtonsoft.Json;

using crmvcsb.Infrastructure.EF;
using crmvcsb.Infrastructure.EF.newOrder;
using crmvcsb.Domain.Interfaces;
using AutoMapper;
using crmvcsb.Domain.NewOrder.API;

using crmvcsb.Domain.NewOrder;


namespace crmvcsb.API.Areas.TestArea.FolderControllers
{
    [Area("NewOrder")]
    [Route("Currency")]
    public class NewOrderController
    {

        private INewOrdermanager _manager;

        public NewOrderController(INewOrdermanager manager) {
            _manager = manager;
        }

        [HttpGet("Get")]
        public async ActionResult<Task<IList<ICrossCurrenciesAPI>>> GetCurrency(GetCurrencyCommand command){
            try{
                return await _manager.GetCurrencyCrossRates(command);
            }catch(Exception e){
                
            }
        }
    }
}