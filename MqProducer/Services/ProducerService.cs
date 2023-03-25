using System.Text;
using Entity.Concrete;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace MqProducer.Services;

public class ProducerService
{
    private IModel channel;
    
    private readonly string Sended = "mail";

  

    public void MqProducer(string MqConnection , Mail mail )
    {
        var ConnectionFactory = new ConnectionFactory()
        {
            Uri = new Uri(MqConnection)
        };
        using var connect = ConnectionFactory
            .CreateConnection();
        channel = connect.CreateModel();
        channel.QueueDeclare(Sended, true, false, false);
            WriteToQueue(Sended, mail);

    }

    public void WriteToQueue(string QueueName , Mail mail)
    {
        var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mail));
        channel.BasicPublish(String.Empty, Sended,null,message);
    }
}