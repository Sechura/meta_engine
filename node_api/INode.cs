using System;

namespace Engine
{
    public struct NodeMetaInfo
    {
        public struct NodeMetaDescription
        {
            public string ImplementorTypeName;
            public NodeType ImplementedInterface;
        }

        public NodeType[] PrerequisiteNodeTypes;
        public NodeType[] OptionalNodeTypes;
        public NodeMetaDescription[] ImplementedNodes;
    }

    public interface INode
    {
        bool GetNodeMetaInfo(IEngine pEngine, ref NodeMetaInfo pMetaInfo);
        bool RunUnitTests();
    }
}
