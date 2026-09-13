using DuckStudio.CatFood.Functions;

namespace DuckStudio.CatFood.Tests.Functions
{
    public class ConstantTests
    {
        [Fact]
        public void ConstantsHaveExpectedTypesAndValues()
        {
            Assert.IsType<string>(Constant.VERSION);
            Assert.NotEmpty(Constant.YES);
            Assert.NotEmpty(Constant.NO);
            Assert.All(Constant.YES, item => Assert.Equal(item.ToLowerInvariant(), item));
            Assert.All(Constant.NO, item => Assert.Equal(item.ToLowerInvariant(), item));
        }
    }
}