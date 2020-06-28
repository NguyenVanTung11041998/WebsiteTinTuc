using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Models.TokenAuth;
using WebsiteTinTuc.Admin.Web.Controllers;
using Shouldly;
using Xunit;

namespace WebsiteTinTuc.Admin.Web.Tests.Controllers
{
    public class HomeController_Tests: AdminWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}