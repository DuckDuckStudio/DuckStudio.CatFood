# DuckStudio.CatFood

该库是 Python 库 [catfood](https://pypi.org/project/catfood/) 在 C# 中的实现，包含一些控制台应用程序的常用代码。

## 和 Python 库的区别

> 本包的当前实现对应 Python 库的 2.0.0 版本。

| 功能 | Python | C# |
|-----|-----|-----|
| `functions.exceptions.*` | :white_check_mark: | :white_check_mark: |
| `functions.format.github` | :white_check_mark: | :white_check_mark:[*](#functionsformatgithub) |
| `functions.github.api` | :white_check_mark: | [部分支持](#functionsgithubapi) |
| `functions.constant` | :white_check_mark: | :white_check_mark: |
| `functions.files` | :white_check_mark: | :white_check_mark: |
| `functions.print` | :white_check_mark: | :white_check_mark:[*](#functionsprint) |
| `functions.terminal` | :white_check_mark: | :white_check_mark: |

### 命名变化 和 实现区别

<span id="functionsformatgithub"></span>
#### `Functions.Format.GitHub`

1、`ResolvesIssue()` 方法还支持使用 `int` 值作为输入参数，Python 包在未来也将支持（[DuckDuckStudio/catfood#15](https://github.com/DuckDuckStudio/catfood/issues/15)）。  

---

<span id="functionsgithubapi"></span>
#### `Functions.GitHub.Api`

1、`获取GitHub文件内容` 函数名称改为 `GetGitHubFileContentAsync()` 异步方法。  
2、`请求GitHubAPI` 函数在本包中没有对应的实现，建议改用 [Octokit](https://www.nuget.org/packages/Octokit/) 实现。  
3、`这是谁的Token` 函数名称改为 `GetTokenOwnerAsync()` 异步方法。  

---

#### `Functions.Files`

1、`open_file` 函数名称改为 `OpenFile()` 方法。

---

<span id="functionsprint"></span>
#### `Functions.Print`

1、`消息头` 类名称改为 `MSHead`。  

2、`MSHead` 类中的成员名称改为英文，对应如下:  

| Python 库 | C# 包 |
|-----|-----|
| `消息头.消息` | `MSHead.Message` |
| `消息头.问题` | `MSHead.Question` |
| `消息头.可选问题` | `MSHead.OptionalQuestion` |
| `消息头.信息` | `MSHead.Information` |
| `消息头.成功` | `MSHead.Success` |
| `消息头.错误` | `MSHead.Error` |
| `消息头.警告` | `MSHead.Warning` |
| `消息头.调试` | `MSHead.Debug` |
| `消息头.提示` | `MSHead.Hint` |
| `消息头.内部警告` | `MSHead.InternalWarning` |
| `消息头.内部错误` | `MSHead.InternalError` |

3、`MSHead.OptionalQuestion` (可选问题) 和 `MSHead.Internal*` (内部xx) 支持本地化。  
有关本地化信息请参见 `Resources/Strings(.*).resx`。  

4、`多行带头输出` 函数在本包中对应的方法是 `PrintMultilineWithPrefix`，该方法的两个重载都和 Python 中的实现有略微区别。  

```csharp
// 重载 1 - content 为 string 的重载
public static void PrintMultilineWithPrefix(string content, string prefix, bool markuped = false, string? newLine = "\n")

// 重载 2 - content 为 string[] 的重载
public static void PrintMultilineWithPrefix(string[] content, string prefix, bool markuped = false)
```

重载 1 相比 Python 包中的函数，还允许指定换行符。  
重载 2 中的 `content` 参数接受一个字符串数组，而 Python 包中的函数的此参数则接受一个字符串。  
这两个重载相比 Python 包中的函数，都还允许通过 `markuped` 参数指定是否格式化内容。  

和 Python 包中的函数行为最像的是:  
```csharp
using DuckStudio.CatFood.Functions;

string content = "line1\n[red]line2[/]\nline3";
Print.PrintMultilineWithPrefix(content: content, prefix: Print.MSHead.Debug, markuped: true);
```

5、此外，本包还新增了 `PrintWithPrefix()` 方法。
```csharp
public static void PrintWithPrefix(string content, string prefix, bool markuped = false)
```
此方法适用于单行输出。  

---

#### `Functions.Terminal`

1、`calculateCharactersDisplayed` 函数名称改为 `CalculateCharactersDisplayed()` 方法（首字母大写）。  

## 依赖
该库的实现离不开这些项目，感谢开源社区！
- [Spectre.Console](https://www.nuget.org/packages/Spectre.Console/) - MIT
- [Octokit](https://www.nuget.org/packages/Octokit/) - MIT

有关这些依赖的许可文件，请参见 [NOTICE.md](./NOTICE.md)。

## 许可
本包和对应的 Python 库一样，使用 [Apche-2.0](https://github.com/DuckDuckStudio/catfood/blob/main/LICENSE) 许可证。  
本包的图标使用 [CC BY-NC-SA 4.0](https://github.com/DuckDuckStudio/DuckStudio.CatFood/blob/main/icon/LICENSE.md) 许可证。
