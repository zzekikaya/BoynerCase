using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.MessageBroker.Concrete
{
    public class RabbitMQConnectionFactory
    {
        private readonly string _hostName = @"localhost:15762/";

        public IConnection GetRabbitMQConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(_hostName),
                Password = "guest",
                UserName = "guest"
            };
            return connectionFactory.CreateConnection();
        }
    }
}
