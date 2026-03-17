namespace DuckStudio.CatFood.Functions
{
    public class Constant
    {
        /// <summary>
        /// DuckStudio.CatFood 的版本
        /// </summary>
        public const string VERSION = "1.0.0";

        /// <summary>
        /// 代表“yes”的一些输入
        /// </summary>
        public static readonly string[] YES = [
            "yes", "y",
            "true", "t",
            "要",
            "是"
        ];

        /// <summary>
        /// 代表“no”的一些输入
        /// </summary>
        public static readonly string[] NO = [
            "no", "n",
            "false", "f",
            "否",
            "不",
            "不要", "不是"
        ];
    }
}
