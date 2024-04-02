using MediportaZadRek.Models;
using MediportaZadRek.QCRS.Tag;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace MediportaZadRek.Tests.Controllers.Tags
{
    [TestClass]
    public class IndexTest
    {

        [TestMethod]
        public async Task TagsIndex_ReturnsTagsWithPageDetails()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.GetAsync("tags");
            var result = await response.Content.ReadFromJsonAsync<TagsWithPaginationDetails>();

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.CurrentPage);
            Assert.AreEqual(10, result.PageSize);
            Assert.AreEqual(1000, result.Total);
            Assert.AreEqual(10, result.Items.Count());
        }

        [TestMethod]
        public async Task TagsIndexWithChangedQueryParams_ReturnsTagsWithPageDetails()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var currentPage = 1;
            var pageSize = 15;
            var sortParam = "PercentagePopulation";
            var sortOrder = SortOrder.desc;

            var response = await httpClient.GetAsync($"tags?currentPage={currentPage}&pageSize={pageSize}&sortParam={sortParam}&sortOrder={sortOrder}");
            var result = await response.Content.ReadFromJsonAsync<TagsWithPaginationDetails>();

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(result);
            Assert.AreEqual(currentPage, result.CurrentPage);
            Assert.AreEqual(pageSize, result.PageSize);
            Assert.AreEqual(1000, result.Total);
            Assert.AreEqual(pageSize, result.Items.Count());
        }


        [TestMethod]
        public async Task TagsIndexWithImproperSortParams_ReturnsBadRequest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var sortParam = "testSortParam";
            var sortOrder = SortOrder.desc;

            var response = await httpClient.GetAsync($"tags?sortParam={sortParam}&sortOrder={sortOrder}");

            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }


        [TestMethod]
        public async Task TagsIndexWithOutOfRangePage_ReturnsEmptyTagsCollectionWithPageDetails()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var currentPage = 50;
            var pageSize = 50;

            var response = await httpClient.GetAsync($"tags?currentPage={currentPage}&pageSize={pageSize}");
            var result = await response.Content.ReadFromJsonAsync<TagsWithPaginationDetails>();

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(result);
            Assert.AreEqual(currentPage, result.CurrentPage);
            Assert.AreEqual(pageSize, result.PageSize);
            Assert.AreEqual(1000, result.Total);
            Assert.AreEqual(0, result.Items.Count());
        }

    }
}
