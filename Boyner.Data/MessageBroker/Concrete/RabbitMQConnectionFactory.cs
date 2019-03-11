using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.MessageBroker.Concrete
{
    public class RabbitMQConnectionFactory
    {
        //private readonly string _hostName = @"localhost";
        private const string HostName = "192.168.1.1";
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string QueueName = "myqueue";
        private const string ExchangeName = "";

        public IConnection GetRabbitMQConnection()
        {            
            var connectionFactory = new ConnectionFactory { HostName = HostName, UserName = UserName, Password = Password };
            return connectionFactory.CreateConnection();
        }
    }
}
