using System;

namespace Engine
{
    /// <summary>
    /// Function prototype for the window resize event callback.
    /// </summary>
    /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
    /// <param name="pWindowWidth">The pixel width of the window, this can't be less than 640.</param>
    /// <param name="pWindowHeight">The pixel height of the window, this can't be less than 480.</param>
    public delegate void WindowResizeEventDelegate(uint pWindowId, uint pWindowWidth, uint pWindowHeight);

    /// <summary>
    /// Function prototype for the window move event callback.
    /// </summary>
    /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
    /// <param name="pWindowLeft">The pixel position of the window on the display's X axis.</param>
    /// <param name="pWindowTop">The pixel position of the window on the display's Y axis.</param>
    public delegate void WindowMoveEventDelegate(uint pWindowId, int pWindowLeft, int pWindowTop);

    /// <summary>
    /// Function prototype for the window state event callback.
    /// </summary>
    /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
    /// <param name="pWindowState">A valid WindowState enumeration member.</param>
    public delegate void WindowStateEventDelegate(uint pWindowId, WindowState pWindowState);

    /// <summary>
    /// Function prototype for the window closed event callback.
    /// </summary>
    /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
    public delegate void WindowClosedEventDelegate(uint pWindowId);

    /// <summary>
    /// An enumeration of valid window states.
    /// </summary>
    public enum WindowState
    {
        /// <summary>
        /// The window is invisible everywhere.
        /// </summary>
        WindowStateHidden,

        /// <summary>
        /// The window is visible only on the task bar.
        /// <para>If focused the window state will change to the previous desktop visible state.</para>
        /// <para>If the previous desktop visible state is unavailable the window state will default to WindowStateWindowedFullscreen.</para>
        /// </summary>
        WindowStateMinimized,

        /// <summary>
        /// The window is visible on the desktop with a title bar and window border and visible on the task bar.
        /// <para>The window can be moved, it will be initially set to the top left corner (0, 0).</para>
        /// <para>If the window is moved then the window state will change to WindowStateSizable.</para>
        /// <para>The window can't be manually resized, it will auto-resize to fit the current desktop display resolution.</para>
        /// <para>The window does not have exclusive control of the display at any point in time.</para>
        /// <para>If focus is lost the window will not change state.</para>
        /// </summary>
        WindowStateMaximized,

        /// <summary>
        /// The window is visible on the desktop with a title bar and window border and visible on the task bar.
        /// <para>The window can be moved.</para>
        /// <para>The window can be resized.</para>
        /// <para>The window does not have exclusive control of the display at any point in time.</para>
        /// <para>If focus is lost the window will not change state.</para>
        /// </summary>
        WindowStateSizable,

        /// <summary>
        /// The window is visible on the desktop with a title bar and window border and visible on the task bar.
        /// <para>The window can be moved.</para>
        /// <para>The window can only be resized through the engine settings.</para>
        /// <para>The window does not have exclusive control of the display at any point in time.</para>
        /// <para>If focus is lost the window will not change state.</para>
        /// </summary>
        WindowStateWindowed,

        /// <summary>
        /// The window is visible on the desktop without a title bar or window border and visible on the task bar.
        /// <para>The window can't be moved, it is always set to the top left corner (0, 0).</para>
        /// <para>The window can't be manually resized, it will auto-resize to fit the current desktop display resolution.</para>
        /// <para>The window does not have exclusive control of the display at any point in time.</para>
        /// <para>If focus is lost the window will not change state.</para>
        /// </summary>
        WindowStateWindowedFullscreen,

        /// <summary>
        /// The window is visible on the desktop without a title bar or window border and visible on the task bar.
        /// <para>The window can't be moved.</para>
        /// <para>The window can only be resized through the engine settings.</para>
        /// <para>The window gains exclusive control of the display when focused.</para>
        /// <para>If focus is lost the window will state will change to WindowStateMinimized.</para>
        /// </summary>
        WindowStateFullscreen
    }

    /// <summary>
    /// Window manager nodes must implement this interface.
    /// <para>Includes INode, IDisposable.</para>
    /// </summary>
    public interface IWindowNode : INode
    {
        /// <summary>
        /// Callback for events where the window has changed sizes.
        /// </summary>
        WindowResizeEventDelegate WindowResizeEvent { get; set; }

        /// <summary>
        /// Callback for events where the window has moved on the display.
        /// </summary>
        WindowMoveEventDelegate WindowMoveEvent { get; set; }

        /// <summary>
        /// Callback for events where the window has changed states.
        /// </summary>
        WindowStateEventDelegate WindowStateEvent { get; set; }

        /// <summary>
        /// Callback for events where the window has been closed.
        /// </summary>
        WindowClosedEventDelegate WindowClosedEvent { get; set; }

        /// <summary>
        /// The number of windows controlled by this node.
        /// </summary>
        int WindowCount { get; }

        /// <summary>
        /// Gets an array of window IDs for all windows controlled by this node.
        /// <para>This will fail if there aren't any windows currently controlled by this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowIdArray">Window ID array, this will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetWindowsAsArray(ref uint[] pWindowIdArray);

        /// <summary>
        /// Adds a new window with the specified parameters to this node.
        /// <para>This will fail if the window size is less than 640x480.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// <para>This may fail if the underlying system rejects the specified window parameters.</para>
        /// </summary>
        /// <param name="pWindowTitle">The text displayed in the window's title bar and in the task bar.</param>
        /// <param name="pWindowLeft">The pixel position of the window on the display's X axis.</param>
        /// <param name="pWindowTop">The pixel position of the window on the display's Y axis.</param>
        /// <param name="pWindowWidth">The pixel width of the window, this can't be less than 640.</param>
        /// <param name="pWindowHeight">The pixel height of the window, this can't be less than 480.</param>
        /// <param name="pWindowId">The window ID of the new window, this will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool AddWindow(string pWindowTitle, int pWindowLeft, int pWindowTop, uint pWindowWidth, uint pWindowHeight, ref uint pWindowId);

        /// <summary>
        /// Removes an existing window from this node.
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        void RemoveWindow(uint pWindowId);

        /// <summary>
        /// Sets the specified window ID as the primary window.
        /// <para>This does not change the window, it is only to mark the intended render target window for the engine.</para>
        /// <para>This will fail if the specified window ID does not belong to an existing window controlled by this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool SetPrimaryWindow(uint pWindowId);

        /// <summary>
        /// Gets the window ID of the current primary window.
        /// <para>This will fail if there is no primary window set or the previous primary window no longer exists.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetPrimaryWindow(ref uint pWindowId);

        /// <summary>
        /// Gets the underlying system handle for the window associated with the specified window ID.
        /// <para>This will fail if the specified window ID does not belong to an existing window controlled by this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        /// <param name="pWindowHandle">The underlying system handle of the specified window, this will remain unchanged if the function fails.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetWindowHandle(uint pWindowId, ref IntPtr pWindowHandle);

        /// <summary>
        /// Sets the pixel position of the window associated with the specified window ID.
        /// <para>This will fail if the specified window ID does not belong to an existing window controlled by this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        /// <param name="pWindowLeft">The pixel position of the window on the display's X axis.</param>
        /// <param name="pWindowTop">The pixel position of the window on the display's Y axis.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool SetWindowPosition(uint pWindowId, int pWindowLeft, int pWindowTop);

        /// <summary>
        /// Gets the current pixel position of the window associated with the specified window ID.
        /// <para>This will fail if the specified window ID does not belong to an existing window controlled by this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        /// <param name="pWindowLeft">The pixel position of the window on the display's X axis.</param>
        /// <param name="pWindowTop">The pixel position of the window on the display's Y axis.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetWindowPosition(uint pWindowId, ref int pWindowLeft, ref int pWindowTop);

        /// <summary>
        /// Sets the pixel size of the window associated with the specified window ID.
        /// <para>This will fail if the window size is less than 640x480.</para>
        /// <para>This will fail if the specified window ID does not belong to an existing window controlled by this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        /// <param name="pWindowWidth">The pixel width of the window, this can't be less than 640.</param>
        /// <param name="pWindowHeight">The pixel height of the window, this can't be less than 480.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool SetWindowSize(uint pWindowId, uint pWindowWidth, uint pWindowHeight);

        /// <summary>
        /// Gets the current pixel size of the window associated with the specified window ID.
        /// <para>This will fail if the specified window ID does not belong to an existing window controlled by this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        /// <param name="pWindowWidth">The pixel width of the window, this can't be less than 640.</param>
        /// <param name="pWindowHeight">The pixel height of the window, this can't be less than 480.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetWindowSize(uint pWindowId, ref uint pWindowWidth, ref uint pWindowHeight);

        /// <summary>
        /// Sets the title of the window associated with the specified window ID.
        /// <para>This will fail if the specified window ID does not belong to an existing window controlled by this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        /// <param name="pWindowTitle">The text displayed in the window's title bar and in the task bar.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool SetWindowTitle(uint pWindowId, string pWindowTitle);

        /// <summary>
        /// Gets the title of the window associated with the specified window ID.
        /// <para>This will fail if the specified window ID does not belong to an existing window controlled by this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        /// <param name="pWindowTitle">The text displayed in the window's title bar and in the task bar.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetWindowTitle(uint pWindowId, ref string pWindowTitle);

        /// <summary>
        /// Gets the state of the window associated with the specified window ID.
        /// <para>This will fail if the value in pWindowState is not a valid member of the WindowState enumeration.</para>
        /// <para>This will fail if the specified window ID does not belong to an existing window controlled by this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        /// <param name="pWindowState">A valid WindowState enumeration member.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool SetWindowState(uint pWindowId, WindowState pWindowState);

        /// <summary>
        /// Sets the state of the window associated with the specified window ID.
        /// <para>This will fail if the specified window ID does not belong to an existing window controlled by this node.</para>
        /// <para>This will fail if the node is not yet initialized.</para>
        /// </summary>
        /// <param name="pWindowId">A window ID of a window currently controlled by this node.</param>
        /// <param name="pWindowState">A valid WindowState enumeration member.</param>
        /// <returns>Returns true on success and false on failure.</returns>
        bool GetWindowState(uint pWindowId, ref WindowState pWindowState);

        /// <summary>
        /// Processes all window events from the underlying system.
        /// <para>This should be called once at the start of a new frame.</para>
        /// </summary>
        void ProcessWindowEvents();
    }
}