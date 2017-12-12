using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace netpack
{
    class netcommon
    {

        public static void netcommonHandle(int bigid, int smallid, byte[] buff)
        {
            if (bigid == 0) //
            {
                if (smallid == NetFramework.Protocal.Connect) //客户端返回连接成功
                {
                    Console.WriteLine("NetFramework.Protocal.Connect connnet ok" );
                }
                else if (smallid == NetFramework.Protocal.Disconnect) //客户端返回关断
                {
                    Console.WriteLine("NetFramework.Protocal.Connect Disconnect");
                }
                else if (smallid == NetFramework.Protocal.Exception) //异常掉线
                {
                    Console.WriteLine("NetFramework.Protocal.Connect Exception");
                }
                else if (smallid == NetFramework.Protocal.ConnectFail) //连接失败
                {
                    Console.WriteLine("NetFramework.Protocal.Connect ConnectFail");
                }
            }
            else //
            {
                Console.WriteLine("netcommonHandle: {0} {1} {2}", bigid, smallid, buff);
                C2SLoginTest();
                //Thread.Sleep(1000);
                //C2SLoginTest();
                //Thread.Sleep(1000);
                //C2SLoginTest();
            }
            
        }

        public static void C2SLoginTest()
        {
            Protocol.C2SLogin.Builder tem = new Protocol.C2SLogin.Builder();
            tem.Account = "test1";
            tem.Pwd = "pwd";
            tem.Servernumber = 1;
            tem.Mac = "mac";
            byte[] buffer = tem.Build().ToByteArray();
            
            clientnet.Program.netMgr.SendMessage(1, 1, buffer);
            Console.WriteLine("---C2SLogin--Account:{0} {1}-", tem.Account, tem.Pwd);
        }
    }
}
