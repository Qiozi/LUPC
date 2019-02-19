
namespace LUComputers.Helper
{
    public class CharHelper
    {
        public static string SQLEscape(string str)
        {
            return string.IsNullOrEmpty(str) ? "" : str.Replace("'", "\\'");
        }
    }
}
