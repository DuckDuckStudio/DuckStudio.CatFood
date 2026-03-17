using Spectre.Console;
using DuckStudio.CatFood.Resources;

namespace DuckStudio.CatFood.Functions
{
    public class Print
    {
        /// <summary>
        /// 消息头类
        /// </summary>
        public class MSHead
        {
            // 特殊
            public const string Message = "[blue][[!]][/]";
            public const string Question = "[blue]?[/]";
            public static readonly string OptionalQuestion = $"[blue]? ({Strings.可选})[/]";
            // 日志输出
            public const string Information = "[blue]INFO[/]";
            public const string Success = "[green]✓[/]";
            public const string Error = "[red]✕[/]";
            public const string Warning = "[yellow]WARN[/]";
            public const string Debug = "[cyan]DEBUG[/]";
            public const string Hint = "[yellow]Hint[/]";
            // 内部
            public static readonly string InternalWarning = $"[yellow]WARN ({Strings.内部})[/]";
            public static readonly string InternalError = $"[red]✕ ({Strings.内部})[/]";
        }

        /// <summary>
        /// <para>带前缀输出内容。</para>
        /// <para>输出前缀使用 <see cref="AnsiConsole.Markup(string)"/>；输出内容使用方法依照 <paramref name="markuped"/> 参数定义</para>
        /// <para>如果你需要带头输出多行内容，请使用 <see cref="PrintMultilineWithPrefix(string, string, string?)"/> (以及它的其他重载)</para>
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="prefix">前缀</param>
        /// <param name="markuped">
        /// <para>是否对内容启用标记。</para>
        /// <para>对于内容，如果启用标记则使用 <see cref="AnsiConsole.MarkupLine(string)"/> 输出，禁用标记则使用 <see cref="Console.WriteLine(string?)"/> 输出</para>
        /// </param>
        public static void PrintWithPrefix(string content, string prefix, bool markuped = false)
        {
            AnsiConsole.Markup(prefix);
            if (markuped)
            {
                AnsiConsole.MarkupLine(content);
            }
            else
            {
                Console.WriteLine($" {content}");
            }
        }

        /// <summary>
        /// 输出多行带前缀的内容
        /// </summary>
        /// <param name="content">多行内容</param>
        /// <param name="prefix">前缀</param>
        /// <param name="markuped">对于内容，如果启用标记则使用 <see cref="AnsiConsole.MarkupLine(string)"/> 输出，禁用标记则使用 <see cref="Console.WriteLine(string?)"/> 输出</param>
        /// <param name="newLine">换行符，默认为 <c>\n</c></param>
        public static void PrintMultilineWithPrefix(string content, string prefix, bool markuped = false, string? newLine = "\n")
        {
            PrintMultilineWithPrefix(content.Split(newLine), prefix, markuped);
        }

        /// <summary>
        /// 输出多行带前缀的内容
        /// </summary>
        /// <param name="content">每项为一行的数组</param>
        /// <param name="prefix">前缀</param>
        /// <param name="markuped">对于内容，如果启用标记则使用 <see cref="AnsiConsole.MarkupLine(string)"/> 输出，禁用标记则使用 <see cref="Console.WriteLine(string?)"/> 输出</param>
        public static void PrintMultilineWithPrefix(string[] content, string prefix, bool markuped = false)
        {
            foreach (string line in content)
            {
                PrintWithPrefix(line, prefix, markuped);
            }
        }
    }
}
