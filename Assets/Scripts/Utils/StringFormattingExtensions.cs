namespace Excercise1
{
    public static class StringFormattingExtensions
    {
        public static string Colored(this string original, string hexColor)
            => $"<color=#{hexColor.Trim('#')}>{original}</color>";
    }
}