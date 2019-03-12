using System;
using RabbitMQ_Consumer;
using RabbitMQ_Producer;
namespace RabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //Producer.SendMessage();
            Consumer.RabbitMQConsumer();
        }
    }
}
