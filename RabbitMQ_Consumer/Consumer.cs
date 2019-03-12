using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ_Consumer
{
    public class Consumer
    {
        private const string exchangeName = "HelloWorld_RabbitMQ";
        private const string queueName = "HelloQueue";
        public static void RabbitMQConsumer()
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

                    channel.QueueBind(queueName, exchangeName, string.Empty);

                    //Console.WriteLine("Waiting for messages");
                    //var consumer = new EventingBasicConsumer(channel);
                    //consumer.Received += (model, ea) =>
                    //{
                    //    var body = ea.Body;
                    //    var message = Encoding.UTF8.GetString(body);
                    //    Console.WriteLine("Received - {0}", message);
                    //};
                    //channel.BasicConsume(queue: queueName,
                    //                     autoAck: true,
                    //                     consumer: consumer);

                    MessageReceiver messageReceiver = new MessageReceiver(channel);
                    channel.BasicConsume(queue: queueName,
                                         autoAck: true,
                                         consumer: messageReceiver);

                    Console.ReadLine();

                }
            }
        }
    }

}
