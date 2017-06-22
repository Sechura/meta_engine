using System;

namespace Engine
{
    /// <summary>
    /// An structure describing metadata about a node.
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
    /// <para>Includes IDisposable.</para>
    /// </summary>
    public interface INode : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="pMetaInfo"></param>
        /// <returns></returns>
        bool GetNodeMetaInfo(IEngine pEngine, ref NodeMetaInfo[] pMetaInfo);

        bool RunUnitTests();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool InitializeNode();
    }
}
