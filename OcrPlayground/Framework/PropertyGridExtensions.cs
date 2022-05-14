// <copyright file="PropertyGridExtensions.cs" company="Shkyrockett" >
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
    /// The property grid extensions.
    /// </summary>
    public static class PropertyGridExtensions
    {
        /// <summary>
        /// Enumerates the groups.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <returns></returns>
        public static IEnumerable<GridItem> EnumerateGroups(this PropertyGrid propertyGrid)
        {
            if (propertyGrid.SelectedGridItem == null)
            {
                yield break;
            }

            foreach (var i in propertyGrid.EnumerateItems())
            {
                if (i.Expandable)
                {
                    yield return i;
                }
            }
        }

        /// <summary>
        /// Enumerates the items.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <returns></returns>
        public static IEnumerable<GridItem> EnumerateItems(this PropertyGrid propertyGrid)
        {
            if (propertyGrid.SelectedGridItem == null)
            {
                yield break;
            }

            var root = propertyGrid.SelectedGridItem;
            while (root.Parent != null)
            {
                root = root.Parent;
            }

            yield return root;

            foreach (var i in root.EnumerateItems())
            {
                yield return i;
            }
        }

        /// <summary>
        /// Gets the group.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public static GridItem? GetGroup(this PropertyGrid propertyGrid, string label)
        {
            if (propertyGrid.SelectedGridItem == null)
            {
                return null;
            }

            foreach (var i in propertyGrid.EnumerateItems())
            {
                if (i.Expandable && i.Label == label)
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Enumerates the items.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private static IEnumerable<GridItem> EnumerateItems(this GridItem item)
        {
            foreach (GridItem i in item.GridItems)
            {
                yield return i;

                foreach (var j in i.EnumerateItems())
                {
                    yield return j;
                }
            }
        }

        /// <summary>
        /// Expands the name of the group.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <param name="groupName">Name of the group.</param>
        public static void ExpandGroupName(this PropertyGrid propertyGrid, string groupName)
        {
            foreach (GridItem gridItem in propertyGrid.SelectedGridItem.GridItems)
            {
                if (gridItem != null)
                {
                    if (gridItem.GridItemType == GridItemType.Category && gridItem.Label == groupName)
                    {
                        gridItem.Expanded = true;
                    }
                }
            }
        }

        /// <summary>
        /// Expands the item with initial expanded attribute.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        public static void ExpandItemWithInitialExpandedAttribute(this PropertyGrid propertyGrid) => ExpandItemWithInitialExpandedAttribute(propertyGrid, propertyGrid.SelectedGridItem);

        /// <summary>
        /// Expands the item with initial expanded attribute.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <param name="gridItem">The grid item.</param>
        private static void ExpandItemWithInitialExpandedAttribute(PropertyGrid propertyGrid, GridItem gridItem)
        {
            if (gridItem != null)
            {
                if (gridItem.GridItemType == GridItemType.Property && gridItem.Expandable)
                {
                    var objs = gridItem.Value.GetType().GetCustomAttributes(typeof(PropertyGridInitialExpandedAttribute), false);
                    if (objs.Length > 0)
                    {
                        if (((PropertyGridInitialExpandedAttribute)objs[0]).InitialExpanded)
                        {
                            gridItem.Expanded = true;
                        }
                    }
                }

                foreach (GridItem childItem in gridItem.GridItems)
                {
                    ExpandItemWithInitialExpandedAttribute(propertyGrid, childItem);
                }
            }
        }
    }
}
