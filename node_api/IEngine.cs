namespace Engine
{
    /// <summary>
    /// An interface implemented by the engine for all nodes to access.
    /// <para>Includes IEntitySystem.</para>
    /// </summary>
    public interface IEngine : IEntitySystem
    {
        /// <summary>
        /// Gets a reference to an object implementing the specified node interface.
        /// <para>This will fail if the specified node interface is not currently loaded by the engine.</para>
        /// </summary>
        /// <typeparam name="T">An object implementing an interface derived from INode.</typeparam>
        /// <param name="pNodeReference">A reference to an object implementing the specified node interface. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetNodeReference<T>(ref T pNodeReference) where T : INode;

        /// <summary>
        /// Requests that the engine load an instance of an object implementing the interface associated with the specified NodeType.
        /// </summary>
        /// <param name="pNodeType">A valid member of the NodeType enumeration.</param>
        void RequestInstanceOfNodeType(NodeType pNodeType);
    }
}
