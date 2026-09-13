using DuckStudio.CatFood.Functions;

namespace DuckStudio.CatFood.Tests.Functions
{
    public class TerminalTests
    {
        [Fact]
        public void RunCommandRejectsEmptyCommand()
        {
            Assert.Throws<ArgumentException>(() => Terminal.RunCommand(Array.Empty<string>()));
        }

        [Fact]
        public void RunCommandReturnsZeroForSuccessfulCommand()
        {
            Assert.Equal(0, Terminal.RunCommand(GetShellCommand("exit 0")));
        }

        [Fact]
        public void RunCommandReturnsExitCodeForFailedCommand()
        {
            Assert.Equal(7, Terminal.RunCommand(GetShellCommand("exit 7")));
        }

        [Fact]
        public void RunCommandReturnsOneForMissingExecutable()
        {
            Assert.Equal(1, Terminal.RunCommand("catfood-command-that-does-not-exist"));
        }

        [Fact]
        public void CalculateCharactersDisplayedCountsAsciiAndCjkCharacters()
        {
            if (!OperatingSystem.IsWindows())
            {
                return;
            }

            Assert.Equal(3, Terminal.CalculateCharactersDisplayed("abc"));
            Assert.Equal(6, Terminal.CalculateCharactersDisplayed("a你b好"));
            Assert.Equal(1, Terminal.CalculateCharactersDisplayed("♪"));
            Assert.Equal(4, Terminal.CalculateCharactersDisplayed("\e[31m红色\e[0m"));
        }

        private static string[] GetShellCommand(string command)
        {
            return OperatingSystem.IsWindows()
                ? ["cmd", "/c", command]
                : ["sh", "-c", command];
        }
    }
}