using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.Extensions.AzureStorage.Tests
{
    public class DependencyInjectionExtensionsTests
    {
        [Fact]
        public void It_should_add_service()
        {
            //Arrange
            var services = new ServiceCollection();
            var option = new AzureStorageOptions()
            {

            };

            //Act
            services.AddAzureStorage(option);

            //Assert
            services.Should().HaveCount(1);
        }
    }
}