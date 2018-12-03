using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Sammak.SandBox.Inventory.QueueMessaging.Consumer
{
    public abstract class QueueHandler : IQueueHandler
    {
        //protected static readonly ConcurrentDictionary<string, IModel> Channels;
        //private readonly int _reconnectDelay = int.Parse(ConfigurationManager.AppSettings["QueueReconnectDelayMilliseconds"]);
        private readonly object connectionLock;
        protected readonly ILogger Logger;

        protected QueueHandler(ILogger logger)
        {
            this.Logger = logger;
            this.connectionLock = new object();
        }

        public abstract string QueueName { get; }

        public abstract void Handle(string body);
        public void Initialize()
        {
            //lock (this.connectionLock)
            //    QueueHandler.Channels.TryAdd(this.QueueName, this.InitializeHandler(this.QueueName, new Action<string>(this.Handle)));
        }

    }
}
