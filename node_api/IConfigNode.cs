using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// A name and value pair for individual fields.
    /// </summary>
    public interface IConfigField
    {
        /// <summary>
        /// The name of this field.
        /// <para>This name is not case sensitive.</para>
        /// <para>This name is unique within it's container.</para>
        /// </summary>
        string FieldName { get; set; }

        /// <summary>
        /// The value of this field.
        /// </summary>
        string FieldValue { get; set; }
    }

    /// <summary>
    /// A collection of IConfigField and IConfigCollection objects.
    /// </summary>
    public interface IConfigCollection
    {
        /// <summary>
        /// The name of this collection.
        /// <para>This name is not case sensitive.</para>
        /// <para>This name is unique either among root containers or within the parent collection.</para>
        /// </summary>
        string CollectionName { get; set; }

        /// <summary>
        /// Gets an enumerator for fields in this collection.
        /// </summary>
        /// <returns>Returns an IConfigField enumerator.</returns>
        IEnumerator<IConfigField> GetFieldEnumerator();

        /// <summary>
        /// Gets an enumerator for child collections in this collection.
        /// </summary>
        /// <returns>Returns an IConfigCollection enumerator.</returns>
        IEnumerator<IConfigCollection> GetCollectionEnumerator();

        /// <summary>
        /// Adds a field with the specified values to this collection.
        /// <para>Field names are not case sensitive.</para>
        /// <para>This will fail if a field with the same name already exists in this collection.</para>
        /// </summary>
        /// <param name="pFieldName">A unique case insensitive field name.</param>
        /// <param name="pFieldValue">Any single line text value.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool AddField(string pFieldName, string pFieldValue);

        /// <summary>
        /// Removes an existing field in this collection.
        /// <para>Field names are not case sensitive.</para>
        /// </summary>
        /// <param name="pFieldName">An unique case insensitive field name in this collection.</param>
        void RemoveField(string pFieldName);

        /// <summary>
        /// Sets the value of the specified field.
        /// <para>Field names are not case sensitive.</para>
        /// <para>This will fail if the specified field does not exist in this collection.</para>
        /// </summary>
        /// <param name="pFieldName">A unique case insensitive field name.</param>
        /// <param name="pFieldValue">Any single line text value.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool SetField(string pFieldName, string pFieldValue);

        /// <summary>
        /// Gets the value of the specified field.
        /// <para>Field names are not case sensitive.</para>
        /// <para>This will fail if the specified field does not exist in this collection.</para>
        /// </summary>
        /// <param name="pFieldName">A unique case insensitive field name.</param>
        /// <param name="pFieldValue">Any single line text value.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetField(string pFieldName, ref string pFieldValue);

        /// <summary>
        /// Adds a child collection with the specified name to this collection.
        /// <para>Collection names are not case sensitive.</para>
        /// <para>This will fail if a collection with the same name already exists within the same collection tier.</para>
        /// </summary>
        /// <param name="pCollectionName">A unique case insensitive collection name.</param>
        /// <param name="pChildCollection"></param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool AddChildCollection(string pCollectionName, ref IConfigCollection pChildCollection);

        /// <summary>
        /// Removes an existing child collection with the specified name from this collection.
        /// <para>Collection names are not case sensitive.</para>
        /// </summary>
        /// <param name="pCollectionName">A unique case insensitive collection name.</param>
        void RemoveChildCollection(string pCollectionName);

        /// <summary>
        /// Gets a reference to an existing child collection with the specified name from this collection.
        /// <para>Collection names are not case sensitive.</para>
        /// <para>This will fail if a collection with the specified name does not exist within this collection.</para>
        /// </summary>
        /// <param name="pCollectionName">A unique case insensitive collection name.</param>
        /// <param name="pChildCollection">A reference to a child collection. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetChildCollection(string pCollectionName, ref IConfigCollection pChildCollection);

        /// <summary>
        /// Gets a reference to the parent collection of this collection.
        /// <para>This will fail if this is a root collection associated with a config.</para>
        /// </summary>
        /// <param name="pParentCollection">A reference to the parent collection of this collection.  This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetParentCollection(ref IConfigCollection pParentCollection);
    }

    /// <summary>
    /// All configuration manager nodes must implement this interface.
    /// <para>Includes INode, IDisposable.</para>
    /// </summary>
    public interface IConfigNode : INode
    {
        /// <summary>
        /// Gets an enumerator for configs in this node.
        /// </summary>
        /// <returns>An IConfigCollection enumerator.</returns>
        IEnumerator<IConfigCollection> GetConfigEnumerator();

        /// <summary>
        /// Adds a new config with the specified name to this node.
        /// <para>Config names are not case sensitive.</para>
        /// <para>This will fail if the specified config name already exists.</para>
        /// </summary>
        /// <param name="pConfigName">A unique case insensitive config name.</param>
        /// <param name="pRootCollection">A reference to a root collection. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool AddConfig(string pConfigName, ref IConfigCollection pRootCollection);

        /// <summary>
        /// Removes an existing config with the specified name from this node.
        /// <para>Config names are not case sensitive.</para>
        /// </summary>
        /// <param name="pConfigName">A unique case insensitive config name.</param>
        void RemoveConfig(string pConfigName);

        /// <summary>
        /// Gets a reference to an existing config with the specified name.
        /// <para>Config names are not case sensitive.</para>
        /// <para>This will fail if the specified config name does not exist.</para>
        /// </summary>
        /// <param name="pConfigName">A unique case insensitive config name.</param>
        /// <param name="pRootCollection">A reference to a root collection. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetConfig(string pConfigName, ref IConfigCollection pRootCollection);

        /// <summary>
        /// Adds a config with the specified name by parsing the supplied byte array.
        /// <para>Config names are not case sensitive.</para>
        /// <para>This will fail if the specified config name already exists.</para>
        /// </summary>
        /// <param name="pConfigName">A unique case insensitive config name.</param>
        /// <param name="pByteArray">An array of bytes representing a serialized config.</param>
        /// <param name="pRootCollection">A reference to a root collection. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool AddConfigFromByteArray(string pConfigName, byte[] pByteArray, ref IConfigCollection pRootCollection);

        /// <summary>
        /// Creates a serialized representation of the specified config and writes it to the supplied byte array.
        /// <para>Config names are not case sensitive.</para>
        /// <para>This will fail if the specified config name does not exist.</para>
        /// </summary>
        /// <param name="pConfigName">A unique case insensitive config name.</param>
        /// <param name="pByteArray">A reference to a byte array. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetByteArrayFromConfig(string pConfigName, ref byte[] pByteArray);
    }
}
