using System;

namespace Engine
{
    public interface IComponent
    { }

    public interface IComponentSystem
    {
        bool CreateComponent(uint pEntityId, NodeType pNodeType);
        void DestroyComponent(uint pEntityId, NodeType pNodeType);
    }

    public interface IEntitySystem
    {
        bool CreateEntity(ref uint pEntityId);
        void DestroyEntity(uint pEntityId);

        bool CreateEntityGroup(ref uint pEntityGroupId);
        void DestroyEntityGroup(uint pEntityGroupId);
        bool AddEntityToGroup(uint pEntityGroupId, uint pEntityId);
        void RemoveEntityFromGroup(uint pEntityGroupId, uint pEntityId);

        bool AssignComponentSystem(NodeType pNodeType, IComponentSystem pComponentSystem);
        void RetractComponentSystem(NodeType pNodeType);
        bool AttachComponent(uint pEntityId, NodeType pNodeType);
        void DetachComponent(uint pEntityId, NodeType pNodeType);
    }
}