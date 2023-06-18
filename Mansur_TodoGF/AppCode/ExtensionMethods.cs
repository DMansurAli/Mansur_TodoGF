namespace Mansur_TodoGF
{
    public static class ExtensionMethods
    {
        public static int ToInt<T>(this T value)
        {
            int retVal = 0;
            int.TryParse(value == null ? "0" : value.ToStringEmpty(), out retVal);
            return retVal;
        }
        public static string ToStringEmpty<T>(this T value)
        {
            if (value == null)
            {
                return "";
            }
            return value.ToString();
        }
    }
}
