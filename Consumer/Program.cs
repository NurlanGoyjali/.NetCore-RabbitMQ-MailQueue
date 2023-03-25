using System.Text;
using Consumer;
using Entity.Concrete;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

# region main

  string Sended = "mail";

  var settings = new Settings();

  var connection = GetConnection();
  var channel = connection.CreateModel();
  var consumerEvent = new EventingBasicConsumer(channel);

  channel.BasicConsume(Sended, true, consumerEvent);

  consumerEvent.Received += Received;
  Console.ReadLine();
  

#endregion

  void Received(object Object, BasicDeliverEventArgs Deliver)
  {
    var json = Encoding.UTF8.GetString(Deliver.Body.ToArray());
    var model = JsonConvert.DeserializeObject<Mail>(json);
    Write(model);
    Process(model);

   
  }

  void Write(Mail mail)
  {
    Console.WriteLine(mail.ToAddress);
    Console.WriteLine(mail.Header);
    Console.WriteLine(mail.Text);
    Console.WriteLine(mail.Sign);
    Console.WriteLine("----------------------");
  }

  void Process(Mail mail)
  {
    // do something
  }

  IConnection GetConnection()
  { 
    var MqConnectionFactory = new ConnectionFactory()
    {
      Uri = new Uri(settings.MqUrl)
    }.CreateConnection();  

    return MqConnectionFactory;

  }