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
    /// The property grid initial expanded attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class PropertyGridInitialExpandedAttribute
        : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether [initial expanded].
        /// </summary>
        /// <value>
        ///   <see langword="true" /> if [initial expanded]; otherwise, <see langword="false" />.
        /// </value>
        public bool InitialExpanded { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyGridInitialExpandedAttribute" /> class.
        /// </summary>
        /// <param name="initialExpanded">if set to <see langword="true" /> [initial expanded].</param>
        public PropertyGridInitialExpandedAttribute(bool initialExpanded)
        {
            InitialExpanded = initialExpanded;
        }
    }
}
