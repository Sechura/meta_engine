namespace Engine
{
    /// <summary>
    /// All components in the entity system must implement this interface.
    /// </summary>
    public interface IComponent
    {
        // empty
    }

    /// <summary>
    /// All component systems in the entity system must implement this interface.
    /// </summary>
    public interface IComponentSystem
    {
        /// <summary>
        /// Assigns a member of the NodeType enumeration to this component system.
        /// <para>This will fail if a NodeType has already been assigned and not yet retracted.</para>
        /// </summary>
        /// <param name="pNodeType">A valid member of the NodeType enumeration.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool AssignNodeType(NodeType pNodeType);

        /// <summary>
        /// Retracts any previously assigned member of the NodeType enumeration from this component system.
        /// <para>All component data associated with this component system will be removed.</para>
        /// </summary>
        void RetractNodeType();

        /// <summary>
        /// Creates a new instance of the component associated with this component system and attaches it to the specified entity.
        /// <para>This will fail if pEntityId is not a valid existing entity.</para>
        /// </summary>
        /// <param name="pEntityId">A valid entity ID from an entity system.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool CreateComponent(uint pEntityId);

        /// <summary>
        /// Destroys an existing instance of a component associated with this component system that is currently attached to the specified entity.
        /// </summary>
        /// <param name="pEntityId">A valid entity ID from an entity system.</param>
        void DestroyComponent(uint pEntityId);
    }

    /// <summary>
    /// An entity system must implement this interface.
    /// </summary>
    public interface IEntitySystem
    {
        /// <summary>
        /// Creates a new entity ID.
        /// <para>This will fail if the [implementation-defined] maximum number of entities already exist.</para>
        /// </summary>
        /// <param name="pEntityId">A valid entity ID, this will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool CreateEntity(ref uint pEntityId);

        /// <summary>
        /// Destroys an existing entity ID.
        /// </summary>
        /// <param name="pEntityId">A valid entity ID.</param>
        void DestroyEntity(uint pEntityId);

        /// <summary>
        /// Creates a new entity group ID.
        /// <para>This will fail if the [implementation-defined] maximum number of entity groups already exist.</para>
        /// </summary>
        /// <param name="pEntityGroupId">A valid entity group ID, this will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool CreateEntityGroup(ref uint pEntityGroupId);

        /// <summary>
        /// Destroys an existing entity group ID.
        /// </summary>
        /// <param name="pEntityGroupId">A valid entity group ID.</param>
        void DestroyEntityGroup(uint pEntityGroupId);

        /// <summary>
        /// Adds an entity to an entity group.
        /// <para>This will fail if pEntityGroupID is not a valid existing entity group.</para>
        /// <para>This will fail if pEntityId is not a valid existing entity.</para>
        /// </summary>
        /// <param name="pEntityGroupId">A valid entity group ID.</param>
        /// <param name="pEntityId">A valid entity ID.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool AddEntityToGroup(uint pEntityGroupId, uint pEntityId);

        /// <summary>
        /// Removes an entity from an entity group.
        /// </summary>
        /// <param name="pEntityGroupId">A valid entity group ID.</param>
        /// <param name="pEntityId">A valid entity ID.</param>
        void RemoveEntityFromGroup(uint pEntityGroupId, uint pEntityId);

        /// <summary>
        /// Assigns a member of the NodeType enumeration to the specified component system.
        /// <para>This will fail if pNodeType is not a valid member of the NodeType enumeration.</para>
        /// <para>This will fail if a NodeType is already assigned to the specified component system and hasn't yet been retracted.</para>
        /// <para>This will fail if the specified NodeType is already assigned to another component system.</para>
        /// </summary>
        /// <param name="pNodeType">A valid member of the NodeType enumeration.</param>
        /// <param name="pComponentSystem">An object implementing the IComponentSystem interface.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool AssignComponentSystem(NodeType pNodeType, IComponentSystem pComponentSystem);

        /// <summary>
        /// Retracts the specified member of the NodeType enumeration from any component system.
        /// </summary>
        /// <param name="pNodeType">A valid member of the NodeType enumeration.</param>
        void RetractComponentSystem(NodeType pNodeType);

        /// <summary>
        /// Attaches a new instance of the component associated with the specified NodeType to the specified entity.
        /// <para>This will fail if pEntityId is not a valid existing entity.</para>
        /// <para>This will fail if pNodeType is not assigned to a component system.</para>
        /// </summary>
        /// <param name="pEntityId">A valid entity ID.</param>
        /// <param name="pNodeType">A valid member of the NodeType enumeration.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool AttachComponent(uint pEntityId, NodeType pNodeType);

        /// <summary>
        /// Detaches an existing instance of the component associated with the specified NodeType from the specified entity.
        /// </summary>
        /// <param name="pEntityId">A valid entity ID.</param>
        /// <param name="pNodeType">A valid member of the NodeType enumeration.</param>
        void DetachComponent(uint pEntityId, NodeType pNodeType);
    }
}