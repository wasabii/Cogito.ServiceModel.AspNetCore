﻿using System;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

using Cogito.AspNetCore.ServiceModel.Threading;

namespace Cogito.AspNetCore.ServiceModel
{

    /// <summary>
    /// Provides async operations for a <see cref="RequestContext" />.
    /// </summary>
    abstract class AsyncRequestContext :
        RequestContext
    {

        public sealed override IAsyncResult BeginReply(Message message, AsyncCallback callback, object state)
        {
            return ReplyAsync(message).ToAsyncBegin(callback, state);
        }

        public sealed override IAsyncResult BeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
        {
            return ReplyAsync(message, timeout).ToAsyncBegin(callback, state);
        }

        public sealed override void EndReply(IAsyncResult result)
        {
            ((Task)result).ToAsyncEnd();
        }

        public sealed override void Reply(Message message)
        {
            ReplyAsync(message).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public sealed override void Reply(Message message, TimeSpan timeout)
        {
            ReplyAsync(message, timeout).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected abstract Task ReplyAsync(Message message);

        protected abstract Task ReplyAsync(Message message, TimeSpan timeout);

    }

}
