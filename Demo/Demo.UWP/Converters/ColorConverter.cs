using Windows.UI;

namespace Demo.UWP.Converters
{
    public static class ColorConverter
    {
        /// <summary>
        /// Devuelve un color a partir de un hexadecimal específico.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color GetColor(string hex)
        {
            Color brush;
            var hexString = hex;
            //remove the # at the front
            hexString = hexString.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long)
            if (hexString.Length == 8)
            {
                a = byte.Parse(hexString.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes
            r = byte.Parse(hexString.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hexString.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hexString.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            brush = Color.FromArgb(a, r, g, b);

            return brush;
        }
    }
}
