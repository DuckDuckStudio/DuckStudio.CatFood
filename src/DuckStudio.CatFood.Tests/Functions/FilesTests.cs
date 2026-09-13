using DuckStudio.CatFood.Functions;

namespace DuckStudio.CatFood.Tests.Functions
{
    public class FilesTests
    {
        [Fact]
        public void OpenFileReturnsFailureForMissingPath()
        {
            Assert.Equal(1, Files.OpenFile(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())));
        }
    }
}