using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ_Producer
{
    public class Producer
    {
        private const string exchangeName = "HelloWorld_RabbitMQ";
        private const string queueName = "HelloQueue";
        private const string routingKeyName = "Hello_Directexchange_key";
        public static void SendMessage()
        {
            ConnectionFactory factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest", Port = 5672 };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
                    channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                   
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = false;

                    string message = "Hello World. This is my first RabbitMQ Message";
                    var body = Encoding.UTF8.GetBytes(message);
                    // channel.QueueBind(queueName, exchangeName, string.Empty);
                    channel.QueueBind(queueName, exchangeName, routingKeyName);

                    channel.BasicPublish(exchange: exchangeName,
                                        // routingKey: string.Empty,
                                         routingKey: routingKeyName,
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine("Message Sent Successfully - {0}", message);

                }
            }
            Console.ReadLine();
        }
    }
    
}
