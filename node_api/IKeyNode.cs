using System;

namespace Engine
{
    /// <summary>
    /// An enumeration of valid input keys for all supported devices.
    /// </summary>
    public enum InputKey
    {
        InputKeyUnrecognized = 0,

        InputKeyKeyboardArrowUp,
        InputKeyKeyboardArrowDown,
        InputKeyKeyboardArrowLeft,
        InputKeyKeyboardArrowRight,

        InputKeyKeyboardCtrlLeft,
        InputKeyKeyboardCtrlRight,
        InputKeyKeyboardAltLeft,
        InputKeyKeyboardAltRight,
        InputKeyKeyboardShiftLeft,
        InputKeyKeyboardShiftRight,
        InputKeyKeyboardSystemLeft,
        InputKeyKeyboardSystemRight,

        InputKeyKeyboardCommand,
        InputKeyKeyboardOption,
        InputKeyKeyboardEsc,
        InputKeyKeyboardTab,
        InputKeyKeyboardSpace,
        InputKeyKeyboardBackspace,
        InputKeyKeyboardEnter,
        InputKeyKeyboardInsert,
        InputKeyKeyboardDelete,
        InputKeyKeyboardHome,
        InputKeyKeyboardEnd,
        InputKeyKeyboardPageUp,
        InputKeyKeyboardPageDown,
        InputKeyKeyboardPauseBreak,
        InputKeyKeyboardPrintScreen,
        InputKeyKeyboardCapsLock,
        InputKeyKeyboardScrollLock,
        InputKeyKeyboardNumLock,

        InputKeyKeyboardNumpad1,
        InputKeyKeyboardNumpad2,
        InputKeyKeyboardNumpad3,
        InputKeyKeyboardNumpad4,
        InputKeyKeyboardNumpad5,
        InputKeyKeyboardNumpad6,
        InputKeyKeyboardNumpad7,
        InputKeyKeyboardNumpad8,
        InputKeyKeyboardNumpad9,
        InputKeyKeyboardNumpad0,
        InputKeyKeyboardNumpadEnter,
        InputKeyKeyboardNumpadDecimal,
        InputKeyKeyboardNumpadAdd,
        InputKeyKeyboardNumpadSubtract,
        InputKeyKeyboardNumpadMultiply,
        InputKeyKeyboardNumpadDivide,

        InputKeyKeyboardTilde,
        InputKeyKeyboardHyphen,
        InputKeyKeyboardEquals,
        InputKeyKeyboardBracketLeft,
        InputKeyKeyboardBracketRight,
        InputKeyKeyboardBackslash,
        InputKeyKeyboardSemicolon,
        InputKeyKeyboardQuote,
        InputKeyKeyboardComma,
        InputKeyKeyboardPeriod,
        InputKeyKeyboardSlash,

        InputKeyKeyboardF1,
        InputKeyKeyboardF2,
        InputKeyKeyboardF3,
        InputKeyKeyboardF4,
        InputKeyKeyboardF5,
        InputKeyKeyboardF6,
        InputKeyKeyboardF7,
        InputKeyKeyboardF8,
        InputKeyKeyboardF9,
        InputKeyKeyboardF10,
        InputKeyKeyboardF11,
        InputKeyKeyboardF12,

        InputKeyKeyboard1,
        InputKeyKeyboard2,
        InputKeyKeyboard3,
        InputKeyKeyboard4,
        InputKeyKeyboard5,
        InputKeyKeyboard6,
        InputKeyKeyboard7,
        InputKeyKeyboard8,
        InputKeyKeyboard9,
        InputKeyKeyboard0,

        InputKeyKeyboardA,
        InputKeyKeyboardB,
        InputKeyKeyboardC,
        InputKeyKeyboardD,
        InputKeyKeyboardE,
        InputKeyKeyboardF,
        InputKeyKeyboardG,
        InputKeyKeyboardH,
        InputKeyKeyboardI,
        InputKeyKeyboardJ,
        InputKeyKeyboardK,
        InputKeyKeyboardL,
        InputKeyKeyboardM,
        InputKeyKeyboardN,
        InputKeyKeyboardO,
        InputKeyKeyboardP,
        InputKeyKeyboardQ,
        InputKeyKeyboardR,
        InputKeyKeyboardS,
        InputKeyKeyboardT,
        InputKeyKeyboardU,
        InputKeyKeyboardV,
        InputKeyKeyboardW,
        InputKeyKeyboardX,
        InputKeyKeyboardY,
        InputKeyKeyboardZ,

        InputKeyMouseCursorX,
        InputKeyMouseCursorY,
        InputKeyMouseLeft,
        InputKeyMouseMiddle,
        InputKeyMouseRight,
        InputKeyMouse4,
        InputKeyMouse5,
        InputKeyMouse6,
        InputKeyMouse7,
        InputKeyMouse8,
        InputKeyMouse9,
        InputKeyMouse10,
        InputKeyMouse11,
        InputKeyMouse12,
        InputKeyMouseWheel,

        InputKeyGamepadStickLeftAxisX,
        InputKeyGamepadStickLeftAxisY,
        InputKeyGamepadStickRightAxisX,
        InputKeyGamepadStickRightAxisY,
        InputKeyGamepadStart,
        InputKeyGamepadSelect,
        InputKeyGamepadA,
        InputKeyGamepadB,
        InputKeyGamepadX,
        InputKeyGamepadY,
        InputKeyGamepadDpadUp,
        InputKeyGamepadDpadDown,
        InputKeyGamepadDpadLeft,
        InputKeyGamepadDpadRight,
        InputKeyGamepadTriggerLeft1,
        InputKeyGamepadTriggerLeft2,
        InputKeyGamepadTriggerRight1,
        InputKeyGamepadTriggerRight2,

        InputKeyJoystickAxisX,
        InputKeyJoystickAxisY,
        InputKeyJoystickAxisZ,
        InputKeyJoystickButton1,
        InputKeyJoystickButton2,
        InputKeyJoystickButton3,
        InputKeyJoystickButton4,
        InputKeyJoystickButton5,
        InputKeyJoystickButton6,
        InputKeyJoystickButton7,
        InputKeyJoystickButton8,
        InputKeyJoystickButton9,
        InputKeyJoystickButton10,
        InputKeyJoystickButton11,
        InputKeyJoystickButton12,
        InputKeyJoystickButton13,
        InputKeyJoystickButton14,
        InputKeyJoystickButton15,
        InputKeyJoystickButton16,

        InputKeyWheel,

        InputKeyHeadsetRoll,
        InputKeyHeadsetPitch,
        InputKeyHeadsetYaw,
        InputKeyHeadsetLookX,
        InputKeyHeadsetLookY,
        InputKeyHeadsetLookZ,

        InputKeyRemoteRoll,
        InputKeyRemotePitch,
        InputKeyRemoteYaw,
        InputKeyRemotePointX,
        InputKeyRemotePointY,
        InputKeyRemotePointZ,
        InputKeyRemoteStart,
        InputKeyRemoteSelect,
        InputKeyRemoteDpadUp,
        InputKeyRemoteDpadDown,
        InputKeyRemoteDpadLeft,
        InputKeyRemoteDpadRight,
        InputKeyRemoteA,
        InputKeyRemoteB,
        InputKeyRemoteC,
        InputKeyRemoteZ,
        InputKeyRemote1,
        InputKeyRemote2,

        InputKeyCamera,

        InputKeyUpperBound
    }

    /// <summary>
    /// A structure defining the current state of an InputKey member.
    /// </summary>
    public struct InputKeyState
    {
        /// <summary>
        /// The current position of the InputKey member.
        /// </summary>
        UInt32 Position;

        /// <summary>
        /// The delta of the InputKey member from the last state update.
        /// </summary>
        float Delta;
    }

    /// <summary>
    /// All key mapping nodes must implement this interface.
    /// <para>Includes INode, IDisposable.</para>
    /// </summary>
    public interface IKeyNode : INode
    {
        /// <summary>
        /// Gets the current state of the specified InputKey member.
        /// <para>This will fail if pKey is not a valid member of the InputKey enumeration.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pKey">A valid member of the InputKey enumeration.</param>
        /// <param name="pKeyState">The current state of the specified InputKey member. This will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetInputKeyState(InputKey pKey, ref InputKeyState pKeyState);

        /// <summary>
        /// Gets the current states of all specified InputKey members.
        /// <para>This will fail if any values in pKeyArray are not valid members of the InputKey enumeration.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pKeyArray">An array of members of the InputKey enumeration.</param>
        /// <param name="pKeyStates">An array of states of the specified InputKey members of equivalent indices. This will remain unchanged of the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetInputKeyStateArray(InputKey[] pKeyArray, ref InputKeyState[] pKeyStates);

        /// <summary>
        /// Binds all input translated from the currently selected locale map to the specified action.
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pLocaleAction">A callback function that receives all locale mapped input.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool BindInputToLocaleAction(Action<InputKey, string> pLocaleAction);

        /// <summary>
        /// Unbinds all input translated from the currently selected locale map from any action.
        /// </summary>
        void UnbindInputFromLocaleAction();

        /// <summary>
        /// Binds the specified member of the InputKey enumeration to the specified action.
        /// <para>This will fail if pKey is not a valid member of the InputKey enumeration.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pKey">A valid member of the InputKey enumeration.</param>
        /// <param name="pAction">A callback function that receives the state of the specified member of the InputKey enumeration.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool BindInputKeyToAction(InputKey pKey, Action<InputKey, InputKeyState> pAction);

        /// <summary>
        /// Unbinds the specified member of the InputKey enumeration from any action.
        /// </summary>
        /// <param name="pKey">A valid member of the InputKey enumeration.</param>
        void UnbindInputKeyFromAction(InputKey pKey);
    }
}