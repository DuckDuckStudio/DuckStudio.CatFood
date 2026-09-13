namespace DuckStudio.CatFood.Tests.Functions.Format
{
    public class GitHubTests
    {
        [Theory]
        [InlineData(123, "123")]
        [InlineData(0, null)]
        [InlineData(-1, null)]
        public void IssueNumberFromIntegerReturnsOnlyPositiveNumbers(int input, string? expected)
        {
            Assert.Equal(expected, DuckStudio.CatFood.Functions.Format.GitHub.IssueNumber(input));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("    ", null)]
        [InlineData("#000", null)]
        [InlineData("#notanumber", null)]
        [InlineData("₁", null)]
        [InlineData("abc", null)]
        [InlineData("https://github.com/owner/repo/issues/", null)]
        [InlineData("https://github.com/owner/repo/issues/₁", null)]
        [InlineData("123", "123")]
        [InlineData("#123", "123")]
        [InlineData("000123", "123")]
        [InlineData("#000123", "123")]
        [InlineData("  #000123  ", "123")]
        [InlineData("https://github.com/owner/repo/issues/456", "456")]
        [InlineData("https://github.com/owner/repo/pull/789", "789")]
        [InlineData("https://github.com/owner/repo/issues/000789", "789")]
        [InlineData("https://github.com/owner/repo/issues/123#discussion", "123")]
        [InlineData("https://github.com/owner/repo/issues/123#issuecomment-456", "123")]
        public void IssueNumberFromStringParsesSupportedForms(string input, string? expected)
        {
            Assert.Equal(expected, DuckStudio.CatFood.Functions.Format.GitHub.IssueNumber(input));
        }

        [Theory]
        [InlineData(123, "Fixes", "- Fixes #123")]
        [InlineData(-1, "Resolves", null)]
        [InlineData("123", "Resolves", "- Resolves #123")]
        [InlineData("#123", "Closes", "- Closes #123")]
        [InlineData("000123", "Fixes", "- Fixes #123")]
        [InlineData("https://github.com/owner/repo/issues/456", "Resolves", "- Resolves #456")]
        [InlineData("", "Resolves", null)]
        [InlineData("          ", "Resolves", null)]
        [InlineData("notanumber", "Resolves", null)]
        [InlineData("#000", "Resolves", null)]
        [InlineData("https://github.com/owner/repo/issues/", "Resolves", null)]
        [InlineData("https://github.com/owner/repo/issues/123#discussion", "Fixes", "- Fixes #123")]
        [InlineData("https://github.com/owner/repo/pull/789", "Closes", "- Closes #789")]
        public void ResolvesIssueFormatsValidIssueNumbers(object input, string keyword, string? expected)
        {
            string? result = input switch
            {
                int number => DuckStudio.CatFood.Functions.Format.GitHub.ResolvesIssue(number, keyword),
                string text => DuckStudio.CatFood.Functions.Format.GitHub.ResolvesIssue(text, keyword),
                // ReSharper disable once LocalizableElement
                _ => throw new ArgumentException("不支持的测试输入", nameof(input))
            };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ResolvesIssueUsesResolvesByDefault()
        {
            Assert.Equal("- Resolves #123", DuckStudio.CatFood.Functions.Format.GitHub.ResolvesIssue("123"));
        }
    }
}