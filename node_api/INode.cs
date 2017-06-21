using System;

namespace Engine
{
    public struct NodeMetaInfo
    {
        public struct NodeMetaDescription
        {
            [Flags]
            public enum NodeMetaCompatibility
            {
                Windows = 1,
                Linux = 2,
                MacOSX = 4
            }

            public string ImplementorTypeName;
            public NodeType ImplementedInterface;
            public NodeMetaCompatibility CompatibleSystems;
        }

        public NodeType[] PrerequisiteNodeTypes;
        public NodeType[] OptionalNodeTypes;
        public NodeMetaDescription[] ImplementedNodes;
    }

    public interface INode : IDisposable
    {
        bool GetNodeMetaInfo(IEngine pEngine, ref NodeMetaInfo pMetaInfo);
        bool RunUnitTests();
        bool InitializeNode();
    }
}
