// <copyright file="Metadata.cs" company="Shkyrockett" >
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
    /// The metadata.
    /// </summary>
    [PropertyGridInitialExpanded(true)]
    public class Metadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata" /> class.
        /// </summary>
        public Metadata()
            : this(new List<Text>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public Metadata(List<Text> text)
        {
            TextEnteries = text;
        }

        /// <summary>
        /// Gets or sets the TextEnteries.
        /// </summary>
        /// <value>
        /// The TextEnteries.
        /// </value>
        [PropertyGridInitialExpanded(true)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Text> TextEnteries { get; set; }

        /// <summary>
        /// Adds the specified TextEnteries.
        /// </summary>
        /// <param name="text">The TextEnteries.</param>
        public void Add(Text text) => TextEnteries.Add(text);
    }
}
