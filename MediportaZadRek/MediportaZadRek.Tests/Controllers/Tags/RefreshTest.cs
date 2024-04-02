using Microsoft.AspNetCore.Mvc.Testing;

namespace MediportaZadRek.Tests.Controllers.Tags
{
    [TestClass]
    public class RefreshTest
    {

        [TestMethod]
        public async Task TagsRefresh_ReturnsTagsWithPageDetails()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.PutAsync("tags", null);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
