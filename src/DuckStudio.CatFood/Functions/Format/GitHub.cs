namespace DuckStudio.CatFood.Functions.Format
{
    public class GitHub
    {
        /// <summary>
        /// 从给定的整数（<c>int</c>）中获取 Issue 或 PR 的编号。
        /// </summary>
        /// <param name="input">给定的整数或 <c>null</c></param>
        /// <returns>获取到的编号（<c>string</c>），整数对应的编号无效时返回 <c>null</c></returns>
        public static string? IssueNumber(int input)
        {
            return input > 0 ? input.ToString() : null;
        }

        /// <summary>
        /// 从给定的字符串（<c>string</c>）中获取 Issue 或 PR 的编号。
        /// </summary>
        /// <param name="input">给定的字符串或 <c>null</c></param>
        /// <returns>获取到的编号（<c>string</c>），没获取到有效编号时返回 <c>null</c></returns>
        public static string? IssueNumber(string input)
        {
            input = input.Trim().TrimStart('#', '0');
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            if (input.All(char.IsDigit))
            {
                return input;
            }

            if (input.StartsWith("https://"))
            {
                string[] segments = input.Split('#')[0].Split('/');
                for (int i = segments.Length - 1; i >= 0; i--)
                {
                    if (segments[i].All(char.IsDigit))
                    {
                        return segments[i];
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// <para>将给定的字符串格式化为 GitHub PR 的 Resolves Issue 格式。</para>
        /// <para>GitHub Docs: https://docs.github.com/zh/issues/tracking-your-work-with-issues/using-issues/linking-a-pull-request-to-an-issue#linking-a-pull-request-to-an-issue-using-a-keyword</para>
        /// </summary>
        /// <param name="input">给定的字符串</param>
        /// <param name="keyword">链接议题时使用的关键词</param>
        /// <returns>格式化成功返回字符串结果，失败返回 <c>null</c>。</returns>
        public static string? ResolvesIssue(string input, string keyword = "Resolves")
        {
            string? num = IssueNumber(input);
            return string.IsNullOrWhiteSpace(num) ? null : $"- {keyword} #{num}";
        }

        /// <summary>
        /// <para>将给定的整数议题编号格式化为 GitHub PR 的 Resolves Issue 格式。</para>
        /// <para>GitHub Docs: https://docs.github.com/zh/issues/tracking-your-work-with-issues/using-issues/linking-a-pull-request-to-an-issue#linking-a-pull-request-to-an-issue-using-a-keyword</para>
        /// </summary>
        /// <param name="input">给定的整数议题编号</param>
        /// <param name="keyword">链接议题时使用的关键词</param>
        /// <returns>格式化成功返回字符串结果，失败返回 <c>null</c>。</returns>
        public static string? ResolvesIssue(int input, string keyword = "Resolves")
        {
            string? num = IssueNumber(input);
            return string.IsNullOrWhiteSpace(num) ? null : $"- {keyword} #{num}";
        }
    }
}
