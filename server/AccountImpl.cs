using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting;
using Grpc.Core;

namespace server
{
    public class AccountImpl : AccountInfo.AccountInfoBase
    {
        string DbName;
        public AccountImpl(string DbName)
        {
            this.DbName = DbName;
        }
        private List<string> PrepTxtPackage(){
            ReaderService RS = new ReaderService();
            return RS.ReadTxtFile();
        }
        private List<string> PrepDbPackage(){
            SqlRepo sqlRepo = new SqlRepo(DbName);
            return sqlRepo.ReadTable();
        }
        public async override Task GetAccountInfo(AccountRequest request, IServerStreamWriter<Info> responseStream, ServerCallContext context){
            //combining the results from text file and db for easier streaming
            var res = PrepTxtPackage().Concat(PrepDbPackage());
            foreach (var item in res)
            {
                var splitter = item.Split('¤');
                var name = splitter[0];
                var email = splitter[1];
                var adr = splitter[2];
                var store = splitter[3];

                await responseStream.WriteAsync(new Info{Name=name, Email=email, Address=adr, StoreType=store});
            }
        }
    }
}