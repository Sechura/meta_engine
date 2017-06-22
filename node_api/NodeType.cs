using System;

namespace Engine
{
    /// <summary>
    /// An enumeration of valid node types.
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// A key mapping node.
        /// <para>Amount of this node type that can be loaded at once: 1</para>
        /// </summary>
        NodeTypeKey,

        /// <summary>
        /// A locale mapping node.
        /// <para>Amount of this node type that can be loaded at once: 1</para>
        /// </summary>
        NodeTypeLocale,

        /// <summary>
        /// A window manager node.
        /// <para>Amount of this node type that can be loaded at once: 1</para>
        /// </summary>
        NodeTypeWindow
    }
}