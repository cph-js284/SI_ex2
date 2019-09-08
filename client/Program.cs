using System;
using System.Text;
using System.Threading.Tasks;
using Accounting;
using Grpc.Core;

namespace client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Enter server IP-Address to establish connection. The port is hard coded to 6789 (reasons)");
            var ipadr = Console.ReadLine();
            try
            {
                Channel channel = new Channel(ipadr+":6789", ChannelCredentials.Insecure);
                AccountRequest AR = new AccountRequest();
                AR.Command =" Not really used for anything";
                var client = new AccountInfo.AccountInfoClient(channel);

                using (var reply = client.GetAccountInfo(AR))
                {
                    var responseStream = reply.ResponseStream;
                    StringBuilder responseLog = new StringBuilder("Response: =============================== " + '\n');

                    while (await responseStream.MoveNext())
                    {
                        Info infostr = responseStream.Current;
                        responseLog.Append(infostr.ToString() + '\n');
                    }

                System.Console.WriteLine(responseLog.ToString());

                Console.WriteLine("done ...");
                }
                
            }
            catch (System.Exception ex)
            {
                
                System.Console.WriteLine("meh");
                throw ex;
            }
        }
    }
}
