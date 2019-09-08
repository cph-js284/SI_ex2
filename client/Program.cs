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
            Channel channel = new Channel("192.168.0.11:6789", ChannelCredentials.Insecure);
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
    }
}
