using Sender;
using Shared;


Semaphore semaphore = new(1, 1);
SqlContext sqlContext = new();
ClientTcp tcpClient = new();


//Tcp Client ip ve port ayarları set ediliyor 
tcpClient.SetEndPoint(Tcp.IPAddress, Tcp.Port);


//10 adet thread oluşturuluyor
for (int i = 0; i < 10; i++)
{
    Thread thread = new(() => WriteSql());
    thread.Start();
}




Console.ReadKey();

void WriteSql()
{   


    //Sql kayıt atılıyor
    Task.Run(() =>
    {
        sqlContext.Set();
    });    



    //Sql deki kayıt her saniye başı tcp server'a gönderiliyor
    Task.Run(async () =>
    {
        if (semaphore.WaitOne())
        {
            while (true)
            {
                var model = sqlContext.Get();

                tcpClient.SendMessage(model);

                await Task.Delay(1000);
            }
           
        }
       
    });
  
}
