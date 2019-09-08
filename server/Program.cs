using System;
using Accounting;
using Grpc.Core;

namespace server
{
    class Program
    {
        static int port = 6789;
        static void Main(string[] args)
        {
            string DbName = "SomeDb";
            System.Console.WriteLine("Creating MysqlData...");
            SqlRepo sqlRepo = new SqlRepo(DbName);
            sqlRepo.setupdb();
            

            Server server = new Server{
                Services = {AccountInfo.BindService(new AccountImpl(DbName))},
                Ports = {new ServerPort("0.0.0.0", port, ServerCredentials.Insecure)}
            };
            server.Start();
            System.Console.WriteLine($"Server running on port {port}");

            Console.WriteLine("Press enter to close");
            Console.ReadLine();
            server.ShutdownAsync().Wait();
            System.Console.WriteLine("bye");
        }
    }
}
