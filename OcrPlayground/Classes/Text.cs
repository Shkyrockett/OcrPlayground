// <copyright file="Text.cs" company="Shkyrockett" >
//     Copyright © 2022 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

using System.ComponentModel;

namespace OCRPlayground
{
    /// <summary>
    /// The text.
    /// </summary>
    public class Text
    {
        /// <summary>
        /// Gets or sets the rectangle.
        /// </summary>
        /// <value>
        /// The rectangle.
        /// </value>
        [TypeConverter(typeof(RectangleFConverter))]
        public RectangleF? Rectangle { get; set; }

        /// <summary>
        /// Gets or sets the rectangle string.
        /// </summary>
        /// <value>
        /// The rectangle string.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string RectangleString
        {
            get { return Rectangle.ToString(); }
            set { Rectangle = (RectangleF)new RectangleFConverter().ConvertFromString(new RectangleFConverter().ConvertToString(value)); }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string TextEntry { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => TextEntry;
    }
}
