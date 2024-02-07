using System;
using System.Net;

namespace KcpSharp
{
    /// <summary>
    /// Multiplex many channels or conversations over the same transport.
    /// </summary>
    public interface IKcpMultiplexConnection<T> : IKcpMultiplexConnection
    {
        /// <summary>
        /// Create a raw channel with the specified conversation ID.
        /// </summary>
        /// <param name="id">The conversation ID.</param>
        /// <param name="state">The user state of this channel.</param>
        /// <param name="options">The options of the <see cref="KcpRawChannel"/>.</param>
        /// <returns>The raw channel created.</returns>
        /// <exception cref="ObjectDisposedException">The current instance is disposed.</exception>
        /// <exception cref="InvalidOperationException">Another channel or conversation with the same ID was already registered.</exception>
        KcpRawChannel CreateRawChannel(int id, IPEndPoint remoteEndPoint, T state, KcpRawChannelOptions? options = null);

        /// <summary>
        /// Create a conversation with the specified conversation ID.
        /// </summary>
        /// <param name="id">The conversation ID.</param>
        /// <param name="state">The user state of this conversation.</param>
        /// <param name="options">The options of the <see cref="KcpConversation"/>.</param>
        /// <returns>The KCP conversation created.</returns>
        /// <exception cref="ObjectDisposedException">The current instance is disposed.</exception>
        /// <exception cref="InvalidOperationException">Another channel or conversation with the same ID was already registered.</exception>
        KcpConversation CreateConversation(int id, IPEndPoint remoteEndPoint, T state, KcpConversationOptions? options = null);

        /// <summary>
        /// Register a conversation or channel with the specified conversation ID and user state.
        /// </summary>
        /// <param name="conversation">The conversation or channel to register.</param>
        /// <param name="id">The conversation ID.</param>
        /// <param name="state">The user state</param>
        /// <exception cref="ArgumentNullException"><paramref name="conversation"/> is not provided.</exception>
        /// <exception cref="ObjectDisposedException">The current instance is disposed.</exception>
        /// <exception cref="InvalidOperationException">Another channel or conversation with the same ID was already registered.</exception>
        void RegisterConversation(IKcpConversation conversation, int id, T? state);

        /// <summary>
        /// Unregister a conversation or channel with the specified conversation ID.
        /// </summary>
        /// <param name="id">The conversation ID.</param>
        /// <param name="state">The user state.</param>
        /// <returns>The conversation unregistered with the user state. Returns default when the conversation with the specified ID is not found.</returns>
        IKcpConversation? UnregisterConversation(int id, out T? state);
    }
}
