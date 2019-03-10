using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ_Producer
{
    public class Producer
    {
        private const string exchangeName = "HelloWorld_RabbitMQ";
        private const string queueName = "HelloQueue";
        public static void RabbitMQProducer()
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

                    string message = "Hello World. This is my first RabbitMQ Message";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.QueueBind(queueName, exchangeName, string.Empty);

                    channel.BasicPublish(exchange: exchangeName,
                                         routingKey: string.Empty,
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine("Message Sent Successfully - {0}", message);

                }
            }
            Console.ReadLine();
        }
    }
    
}
