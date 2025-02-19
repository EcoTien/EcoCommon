using UnityEngine;

namespace EcoMine.Common.Extensions
{
    internal static class ColorExtension
    {
        public static void SetAlphaSelf(this ref Color color, float alpha)
        {
            color.a = alpha;
        }

        public static void SetRGBSelf(this ref Color color, float r, float g, float b)
        {
            color.r = r;
            color.g = g;
            color.b = b;
        }

        public static void SetRGBASelf(this ref Color color, float r, float g, float b, float a)
        {
            color.r = r;
            color.g = g;
            color.b = b;
            color.a = a;
        }

        public static void SetRSelf(this ref Color color, float r)
        {
            color.r = r;
        }

        public static void SetGSelf(this ref Color color, float g)
        {
            color.g = g;
        }

        public static void SetBSelf(this ref Color color, float b)
        {
            color.b = b;
        }

        public static Color SetAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }

        public static Color SetRGB(this Color color, float r, float g, float b)
        {
            color.r = r;
            color.g = g;
            color.b = b;
            return color;
        }

        public static Color SetRGBA(this Color color, float r, float g, float b, float a)
        {
            color.r = r;
            color.g = g;
            color.b = b;
            color.a = a;
            return color;
        }

        public static Color SetR(this Color color, float r)
        {
            color.r = r;
            return color;
        }

        public static Color SetG(this Color color, float g)
        {
            color.g = g;
            return color;
        }

        public static Color SetB(this Color color, float b)
        {
            color.b = b;
            return color;
        }

        public static float GetAlpha(this Color color)
        {
            return color.a;
        }
    }
}