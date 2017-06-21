using System;

namespace Engine
{
    public interface ILocaleNode : INode
    {
        bool GetLocaleFromByteArray(byte[] pByteArray, ref string pLocale);
        bool GetAvailableLocalesAsArray(ref string[] pLocaleArray);
        bool SetLocale(string pLocale);
        bool GetLocale(ref string pLocale);
        bool GetLocaleTextFromStringId(ulong pStringId, ref string pText);
        bool GetGlyphFromLocale(InputKey pInput, ref string pGlyph);
    }
}