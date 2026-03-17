using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DuckStudio.CatFood.Functions
{
    public class Files
    {
        /// <summary>
        /// 在 Linux 上打开文件的一些工具
        /// </summary>
        private static readonly string[] LINUX_TOOLS = ["xdg-open", "nano", "vim"];

        /// <summary>
        /// 打开指定路径的文件（夹）
        /// </summary>
        /// <param name="file">需要打开的文件（夹）的路径</param>
        /// <returns>成功打开返回 <c>0</c>，失败返回 <c>1</c></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public static int OpenFile(string file)
        {
            try
            {
                if (!File.Exists(file) && !Directory.Exists(file))
                {
                    throw new FileNotFoundException($"未找到 {file}");
                }

                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Directory.Exists(file))
                {
                    throw new PlatformNotSupportedException("只能在 Windows 上打开目录");
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // 使用 shell 打开
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = file,
                        UseShellExecute = true
                    });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    if (LINUX_TOOLS.All(tool => Terminal.RunCommand([tool, file]) != 0))
                    {
                        throw new Exception("我实在不知道该用什么来打开它...");
                    }
                }
                else
                {
                    throw new PlatformNotSupportedException($"很抱歉，作者见识太少，不清楚如何在 {RuntimeInformation.OSDescription} 上打开文件...");
                }

                return 0;
            }
            catch (Exception ex)
            {
                Print.PrintMultilineWithPrefix(["打开文件时发生异常", ex.Message], Print.MSHead.Error);
                return 1;
            }
        }
    }
}
