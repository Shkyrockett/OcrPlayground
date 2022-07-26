// <copyright file="GraphicsExtensions.cs" company="Shkyrockett" >
//     Copyright © 2022 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

namespace OCRPlayground
{
    /// <summary>
    /// The graphics extensions.
    /// </summary>
    public static class GraphicsExtensions
    {
        /// <summary>
        /// Convert from one type of unit to another.
        /// http://csharphelper.com/blog/2014/08/get-font-metrics-in-c/
        /// https://github.com/tracyacai/sharpmap/blob/master/SharpMap/Rendering/Symbolizer/Utility.cs
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="value">The value.</param>
        /// <param name="fromUnit">From unit.</param>
        /// <param name="toUnit">To unit.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Unknown input unit {fromUnit} in FontInfo.ConvertUnits
        /// or
        /// Unknown output unit {toUnit} in FontInfo.ConvertUnits
        /// </exception>
        public static float ConvertGraphicsUnits(this Graphics graphics, float value, GraphicsUnit fromUnit, GraphicsUnit toUnit)
        {
            if (fromUnit == toUnit) return value;

            // Convert to pixels. 
            value *= fromUnit switch
            {
                GraphicsUnit.World => graphics.DpiY / graphics.PageScale,
                GraphicsUnit.Display => graphics.DpiY / (graphics.DpiY < 100f ? 72f : 100f),
                GraphicsUnit.Pixel => 1f,
                GraphicsUnit.Point => graphics.DpiY / 72f,
                GraphicsUnit.Inch => graphics.DpiY,
                GraphicsUnit.Document => graphics.DpiY / 300f,
                GraphicsUnit.Millimeter => graphics.DpiY / 25.4F,
                _ => throw new Exception($"Unknown input unit {fromUnit} in {nameof(ConvertGraphicsUnits)}"),
            };

            // Convert from pixels to the new units.
            value /= (float)(toUnit switch
            {
                GraphicsUnit.World => graphics.DpiY / graphics.PageScale,
                GraphicsUnit.Display => graphics.DpiY / (graphics.DpiY < 100f ? 72f : 100f),
                GraphicsUnit.Pixel => 1f,
                GraphicsUnit.Point => graphics.DpiY / 72f,
                GraphicsUnit.Inch => graphics.DpiY,
                GraphicsUnit.Document => graphics.DpiY / 300f,
                GraphicsUnit.Millimeter => graphics.DpiY / 25.4F,
                _ => throw new Exception($"Unknown input unit {fromUnit} in {nameof(ConvertGraphicsUnits)}"),
            });

            return value;
        }
    }
}
