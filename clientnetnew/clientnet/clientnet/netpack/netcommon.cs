using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;

namespace netpack
{
    class netcommon
    {

        public static void init()
        {
            clientnet.netlogin.GetInstance();
        }
        public static void netcommonHandle(int bigid, int smallid, byte[] buff)
        {
            if (bigid == 0 && smallid < 20) //
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
                }else
                {
                    Console.WriteLine("NetFramework.Protocal.Connect bigid < 0 ");
                }
            }
            else //
            {
                Console.WriteLine("netcommonHandle: {0} {1} {2}", bigid, smallid, buff);
                protos.protomgr.ParseFrom(bigid*256 + smallid,buff);
            }

        }

        
    }
}
