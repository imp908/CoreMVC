
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Newtonsoft.Json;

using crmvcsb.Infrastructure.EF;
using crmvcsb.Infrastructure.EF.NewOrder;
using crmvcsb.Domain.Interfaces;
using AutoMapper;

using crmvcsb.Domain.TestModels;
using crmvcsb.Domain.Currencies;

namespace crmvcsb.API.Areas.TestArea.FolderControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {

        private ITestManager _testManager;
        private INewOrderService _newOrderManager;

        public TestController(ITestManager testManager, INewOrderService newOrderManager) 
        {
            _testManager = testManager;
            _newOrderManager = newOrderManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {               
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetId")]
        public async Task<IActionResult> GetId()
        {
            try
            {
                return Ok(1);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ConnStr")]
        public async Task<IActionResult> EfContextsRegistrationCheck()
        {
            try 
            {
                var testDbConnStr = _testManager.GetDbName();
                var newOrderDbConnStr = _newOrderManager.GetDbName();

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}