using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentAssertions;

using mvccoresb;
using KATAS;

namespace integrationtests
{

    
    public class ControllerTests
        : IClassFixture<WebApplicationFactory<mvccoresb.Startup>>
    {
        private readonly WebApplicationFactory<mvccoresb.Startup> _factory;

        public ControllerTests(WebApplicationFactory<mvccoresb.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]        
        [InlineData("/TestArea")]
        [InlineData("/TestArea/Home/")]
        [InlineData("/TestArea/Home/NewHomeIndex")]
        [InlineData("/Scaffolded/home/index")]        
        [InlineData("/TestArea/JScheck/CheckAppOne")]        
        [InlineData("/TestArea/React/CheckShoppingList")]
        [InlineData("/TestArea/SignalR/hub")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();                        

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
        
        [Theory]
        [InlineData("/api/values")]
        public async Task Get_ApiEndpointsReturnSuccessAndJsonContentType(string url){

            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
        
    }
    
    
    public class InfrastructureTests
    {
        [Fact]
        public void KATAS_heapSortTest(){
            
            //arrange
            List<int> arr = new List<int>(){4,5,3,2,1};
            List<int> expexcted = new List<int>() { 4, 5, 3, 2, 1 };

            //act
            var actual = Miscellaneous.HeapSort.GO(arr);

            //assert
            actual.Should().Equal(expexcted);

        }
    }

}