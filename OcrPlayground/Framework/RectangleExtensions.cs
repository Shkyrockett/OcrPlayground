// <copyright file="RectangleExtensions.cs" company="Shkyrockett" >
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
    /// 
    /// </summary>
    public static class RectangleExtensions
    {
        /// <summary>
        /// Tops the left.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns></returns>
        public static PointF TopLeft(this RectangleF rectangle) => new(rectangle.Left, rectangle.Top);

        /// <summary>
        /// Tops the right.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns></returns>
        public static PointF TopRight(this RectangleF rectangle) => new(rectangle.Right, rectangle.Top);

        /// <summary>
        /// Bottoms the left.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns></returns>
        public static PointF BottomLeft(this RectangleF rectangle) => new(rectangle.Left, rectangle.Bottom);

        /// <summary>
        /// Bottoms the right.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns></returns>
        public static PointF BottomRight(this RectangleF rectangle) => new(rectangle.Right, rectangle.Bottom);

        /// <summary>
        /// Tos the rectangle f.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>A RectangleF.</returns>
        public static RectangleF ToRectangleF(this Windows.Foundation.Rect rect) => RectangleF.FromLTRB((float)rect.Left, (float)rect.Top, (float)rect.Right, (float)rect.Bottom);
    }
}
