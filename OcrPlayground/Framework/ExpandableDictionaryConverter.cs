﻿// <copyright file="ExpandableDictionaryConverter.cs" company="Shkyrockett" >
//     Copyright © 2022 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace OCRPlayground
{
    /// <summary>
    /// The expandable dictionary converter class.
    /// </summary>
    /// <seealso cref="System.ComponentModel.CollectionConverter" />
    /// <acknowledgment>
    /// http://stackoverflow.com/questions/32582504/propertygrid-expandable-collection
    /// </acknowledgment>
    public class ExpandableDictionaryConverter
        : CollectionConverter
    {
        #region Methods
        /// <summary>
        /// Convert the to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <param name="destType">The destType.</param>
        /// <returns>
        /// The <see cref="object" />.
        /// </returns>
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destType)
        {
            if (destType == typeof(string))
            {
                return "[Key, Value]";
            }

            return base.ConvertTo(context, culture, value, destType);
        }

        /// <summary>
        /// Get the properties supported.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

        /// <summary>
        /// Get the properties.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="attributes">The attributes.</param>
        /// <returns>
        /// The <see cref="PropertyDescriptorCollection" />.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
        {
            if (value is not IDictionary dictionary || dictionary.Count == 0)
            {
                return base.GetProperties(context, value, attributes);
            }

            var items = new PropertyDescriptorCollection(null);

            foreach (var key in dictionary.Keys)
            {
                items.Add(new ExpandableDictionaryPropertyDescriptor(dictionary, key));
            }

            return items;
        }
        #endregion Methods
    }
}
