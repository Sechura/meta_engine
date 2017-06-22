using System;

namespace Engine
{
    /// <summary>
    /// An structure describing meta information about a node.
    /// </summary>
    public struct NodeMetaInfo
    {
        /// <summary>
        /// An enumeration of potentially compatible operating systems for .NET Core.
        /// </summary>
        [Flags]
        public enum NodeMetaCompatibility
        {
            /// <summary>
            /// Microsoft Windows 7 SP1 or higher.
            /// </summary>
            Windows = 1,

            /// <summary>
            /// Ubuntu, CentOS, or another Linux distro.
            /// </summary>
            Linux = 2,

            /// <summary>
            /// Apple macOS 10.11 El Capitan or higher.
            /// </summary>
            MacOS = 4
        }

        /// <summary>
        /// The type name of the described node implementing the interface specified by ImplementedInterface.
        /// </summary>
        public string ImplementorTypeName;

        /// <summary>
        /// The type of interface implemented by the described node.
        /// </summary>
        public NodeType ImplementedInterface;

        /// <summary>
        /// A bitwise representation of the described node's operating system compatibility.
        /// </summary>
        public NodeMetaCompatibility CompatibleSystems;

        /// <summary>
        /// An array of node types that are necessary for the described node to operate properly.
        /// </summary>
        public NodeType[] PrerequisiteNodeTypes;

        /// <summary>
        /// An array of node types which aren't necessary but will extend the described node's functionality.
        /// </summary>
        public NodeType[] OptionalNodeTypes;
    }

    /// <summary>
    /// A common interface for all nodes.
    /// <para>Nodes should use a constructor without parameters to initialize themselves to the point of supporting GetNodeMetaInfo.</para>
    /// <para>All other initialization should occur when the engine calls InitializeNode.</para>
    /// <para>Includes IDisposable.</para>
    /// </summary>
    public interface INode : IDisposable
    {
        /// <summary>
        /// Get the meta information for all interfaces supported by this node.
        /// <para>This function should be available before the engine calls InitializeNode.</para>
        /// </summary>
        /// <param name="pMetaInfo">An array of meta information about the interfaces implemented by this node.</param>
        void GetNodeMetaInfo(ref NodeMetaInfo[] pMetaInfo);

        /// <summary>
        /// Fully initializes the node and all supported interfaces.
        /// <para>The node should expect that all prerequisite node types are loaded by the engine beforehand.</para>
        /// <para>However the node is expected to use pEngine to acquire references to its prerequisite node types.</para>
        /// </summary>
        /// <param name="pEngine">An interface that allows the node to call core engine functions.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool InitializeNode(IEngine pEngine);
    }
}
