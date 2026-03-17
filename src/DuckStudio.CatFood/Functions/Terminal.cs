using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DuckStudio.CatFood.Functions
{
    public class Terminal
    {
        /// <summary>
        /// Git 的一些网络错误提示消息
        /// </summary>
        private static readonly string[] GIT_NETWORK_ERRORS = [
            "unable to access", "could not resolve host",
            "failed to connect", "operation timed out",
            "early eof", "rpc failed"
        ];

        /// <summary>
        /// <para>运行指定命令，并允许设置自动重试。</para>
        /// <para>拒绝重试 Git 因非网络错误导致的失败。</para>
        /// </summary>
        /// <param name="command">需要运行的命令</param>
        /// <param name="retry">重试前的等待时间，<c>-1</c> 表示不重试</param>
        /// <returns>退出代码</returns>
        public static int RunCommand(string command, int retry = -1)
        {
            return RunCommand(command.Split(' ', StringSplitOptions.RemoveEmptyEntries), retry);
        }

        /// <summary>
        /// <para>运行指定命令，并允许设置自动重试。</para>
        /// <para>拒绝重试 Git 因非网络错误导致的失败。</para>
        /// </summary>
        /// <param name="command">需要运行的命令</param>
        /// <param name="retry">重试前的等待时间，<c>-1</c> 表示不重试</param>
        /// <returns>退出代码</returns>
        public static int RunCommand(string[] command, int retry = -1)
        {
            if (command.Length == 0)
            {
                throw new ArgumentException("命令不能为空", nameof(command));
            }

            while (true)
            {
                try
                {
                    ProcessStartInfo processStartInfo = new()
                    {
                        FileName = command[0],
                        Arguments = string.Join(" ", command.Skip(1)),
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using var process = new Process { StartInfo = processStartInfo };
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (!string.IsNullOrWhiteSpace(output))
                    {
                        Console.WriteLine(output.Trim());
                    }

                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        Console.WriteLine(error.Trim());
                    }

                    if (process.ExitCode == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        Print.PrintWithPrefix(
                            $"运行 [blue]{string.Join(" ", command)}[/] 失败，[blue]{command[0]}[/] 返回非零退出代码 [blue]{process.ExitCode}[/]",
                            Print.MSHead.Error
                        );

                        if (retry < 0) // 不重试
                        {
                            return process.ExitCode;
                        }
                        else
                        {
                            if (command[0].Equals("git", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!GIT_NETWORK_ERRORS.Any(keyword => error.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                                {
                                    Print.PrintWithPrefix("这看起来像是 Git 遇到了网络之外的问题，拒绝重试", Print.MSHead.Warning);

                                    return process.ExitCode;
                                }
                            }

                            if (retry > 0)
                            {
                                try
                                {
                                    for (int i = retry; i > 0; i--)
                                    {
                                        Console.Write($"\r{i}秒后重试...");
                                        Thread.Sleep(1000);
                                    }
                                }
                                catch (ThreadInterruptedException)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Console.Write("\r");
                                }
                            }
                        }
                    }
                }
                catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 2) // ERROR_FILE_NOT_FOUND
                {
                    Print.PrintWithPrefix($"未找到 {command[0]}", Print.MSHead.Error);
                    return 1;
                }
                catch (OperationCanceledException)
                {
                    Print.PrintWithPrefix($"终止运行命令 [blue]{string.Join(" ", command)}[/]，因为收到了 Ctrl + C (OperationCanceledException)", Print.MSHead.Error);
                    throw;
                }

                Print.PrintWithPrefix("正在重试 ...", Print.MSHead.Information);
            }
        }

        /// <summary>
        /// <para>（支持比较局限，不推荐使用）</para>
        /// <para>计算内容在 Windows 终端上显示占多少字符的位置。</para>
        /// <para>方法请参阅我的文章: https://duckduckstudio.github.io/Articles/#/信息速查/Python/输出/计算输出的内容在Windows终端上的显示占多少字符</para>
        /// </summary>
        /// <param name="content">指定的内容</param>
        /// <returns>显示所占的字数</returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public static int CalculateCharactersDisplayed(string content)
        {
            if (!OperatingSystem.IsWindows())
            {
                throw new PlatformNotSupportedException("CalculateCharactersDisplayed 仅在 Windows 终端中可用");
            }

            content = Regex.Replace(content, @"\x1b\[[0-9;]*m", "");

            int total = 0;
            foreach (char c in content)
            {
                total++;
                if (!((c < 128) || (c == '♪')))
                {
                    total += 1;
                }
            }

            return total;
        }
    }
}
