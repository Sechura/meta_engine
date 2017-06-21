using System;

namespace Engine
{
    public interface IEngine : IEntitySystem
    {
        bool GetNodeReference<T>(ref T pNodeReference) where T : INode;
    }
}
