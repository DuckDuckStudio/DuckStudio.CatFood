using DuckStudio.CatFood.Functions.GitHub;

namespace DuckStudio.CatFood.Tests.Functions.GitHub
{
    // ReSharper disable once InconsistentNaming - 和对应类命名保持一致
    public class APITests
    {
        [Theory]
        [InlineData("Muelsyse")]
        [InlineData("owner/repo/extra")]
        public async Task GetGitHubFileContentReturnsNullForInvalidRepository(string repo)
        {
            Assert.Null(await API.GetGitHubFileContentAsync(repo, "README.md"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public async Task GetTokenOwnerReturnsNullForMissingToken(string? token)
        {
            Assert.Null(await API.GetTokenOwnerAsync(token));
        }
    }
}