using Core.MessageBroker.Abstract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.MessageBroker.Concrete
{
    public class MessageBroker : IMessageBroker
    {
        private readonly RabbitMQConnectionFactory _rabbitMQConnectionFactory;

        public MessageBroker()
        {
            _rabbitMQConnectionFactory = new RabbitMQConnectionFactory();
        }

        public void Consumer(string queueName)
        {
            using (var connection = _rabbitMQConnectionFactory.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        Debug.WriteLine(queueName, message);
                    };

                    channel.BasicConsume(queueName, true, consumer);
                }
            }
        }

        public void Publisher(string queueName, string message)
        {
            try
            {
                using (var connection = _rabbitMQConnectionFactory.GetRabbitMQConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var properties = channel.CreateBasicProperties();

                        channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(message));

                        Debug.WriteLine(queueName, message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
              
            }
        
        }
    }
}
