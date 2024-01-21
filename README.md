TCP haberleşme protokolu ile haberleşebilen 2 adet consol uygulamasıdır. 1 TCP Client 1 TCP Server projesi vardır.
10 adet thread ile sql kayıt atılırken aynı zamanda sqldeki güncel veri ile tcp server'a iletilir.
Tcp ve Sql bağlantıları Shared katmanındaki Sql ve Tcp class larından değiştirilebilir.
Sender ve Receiver exe leri ilgili projenin bin/debug/net8.0 dosyaları içindedir.
Db backup Shared katmanında DbBackup klasöründedir.
