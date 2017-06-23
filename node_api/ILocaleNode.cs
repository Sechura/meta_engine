namespace Engine
{
    /// <summary>
    /// All locale mapping nodes must implement this interface.
    /// <para>Includes INode, IDisposable.</para>
    /// </summary>
    public interface ILocaleNode : INode
    {
        /// <summary>
        /// Reads a byte array as a raw XML defining locale map.
        /// <para>This will fail if the XML file is malformed or does not properly implement the locale map.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pByteArray">An array of bytes obtained from reading a locale map XML.</param>
        /// <param name="pLocale">The locale name specified by the locale map XML. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetLocaleFromByteArray(byte[] pByteArray, ref string pLocale);

        /// <summary>
        /// Gets a string array of locale names currently loaded.
        /// <para>This will fail if there aren't any locale maps loaded in this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pLocaleArray">An array of strings representing valid locale names. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetAvailableLocalesAsArray(ref string[] pLocaleArray);

        /// <summary>
        /// Sets the locale map from a valid locale name.
        /// <para>This will fail if the specified locale name does not represent a locale map currently loaded in this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pLocale">A locale name represending a locale map currently loaded in this node.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool SetLocale(string pLocale);

        /// <summary>
        /// Gets the currently selected locale name.
        /// <para>This will fail if a locale name has not yet been set.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pLocale">A locale name represending a locale map currently loaded in this node. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetLocale(ref string pLocale);

        /// <summary>
        /// Gets a UTF-16 string associated with the specified string ID from the currently selected locale map.
        /// <para>This function will fail if a locale map is not currently selected.</para>
        /// <para>This will fail if the string ID is not associated with any string.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pStringId">An ID associated with a UTF-16 string defined in the currently selected locale map.</param>
        /// <param name="pText">The UTF-16 string associated with the specified string ID. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetLocaleTextFromStringId(ulong pStringId, ref string pText);

        /// <summary>
        /// Gets a UTF-16 representation of the specified input key based on the currently loaded locale map.
        /// <para>This function will fail if a locale map is not currently selected.</para>
        /// <para>This function will fail if the value specified by pInput is not a valid member of the InputKey enumeration.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pInput">A value specified from the InputKey enumeration.</param>
        /// <param name="pGlyph">The UTF-16 representation of the specified InputKey value. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetGlyphFromLocale(InputKey pInput, ref string pGlyph);
    }
}