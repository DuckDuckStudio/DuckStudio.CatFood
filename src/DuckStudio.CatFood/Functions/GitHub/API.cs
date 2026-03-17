using Octokit;
using System.Text;

namespace DuckStudio.CatFood.Functions.GitHub
{
    public class API
    {
        /// <summary>
        /// 获取指定仓库中指定文件的文本内容。
        /// </summary>
        /// <param name="repo">文件所在的仓库，应为 <c>owner/repo</c> 的格式</param>
        /// <param name="path">需要获取的文件在仓库中的相对路径</param>
        /// <param name="token">请求时附带的 GitHub Token</param>
        /// <returns>UTF-8 编码解码后的文本文件字符串；获取失败返回 <c>null</c></returns>
        public static async Task<string?> GetGitHubFileContentAsync(string repo, string path, string? token = null)
        {
            string[] repoPaths = repo.Split('/');
            if (repoPaths.Length !=  2)
            {
                return null;
            }

            try
            {
                IReadOnlyList<RepositoryContent> content = await CreateGitHubClient(token).Repository.Content.GetAllContents(
                    owner: repoPaths[0],
                    name: repoPaths[1],
                    path: path.Replace('\\', '/')
                );

                RepositoryContent? fileContent = (content.Count > 0) ? content[0] : null;

                if (fileContent?.Content == null)
                {
                    return null;
                }

                return Encoding.UTF8.GetString(Convert.FromBase64String(fileContent.Content));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定 GitHub Token 的所有者
        /// </summary>
        /// <param name="token">指定的 GitHub Token</param>
        /// <returns>Token 的所有者的用户名；获取失败返回 <c>null</c></returns>
        public static async Task<string?> GetTokenOwnerAsync(string? token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            try
            {
                GitHubClient client = CreateGitHubClient(token);
                User user = await client.User.Current();
                return user.Login;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 创建并配置 <c>GitHubClient</c> 实例
        /// </summary>
        /// <param name="token">可选的请求 Token</param>
        /// <returns>配置好的 <c>GitHubClient</c></returns>
        private static GitHubClient CreateGitHubClient(string? token = null)
        {
            var client = new GitHubClient(new ProductHeaderValue("DuckDuckStudio/catfood .NET 实现 (aka DuckStudio.CatFood)"));
            if (!string.IsNullOrEmpty(token))
            {
                client.Credentials = new Credentials(token);
            }
            return client;
        }
    }
}
