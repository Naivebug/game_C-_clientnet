﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace clientnet
{
    class Program
    {
        public static async Task InputKey()
        {
            while (true)
            {
                Console.Write("可以输入客户端GM测试快捷键: \n");
                string readstr = Console.ReadLine();
                if (readstr.Trim() != "")
                {
                    netgm.GetInstance().HandleGM(readstr);
                }
            }
        }


        public static NetFramework.NetworkManager netMgr;
        public static String ip;
        public static int port;
        static void Main(string[] args){
        
           
            Console.Write("请输入数字连如下服务器(按Enter默认1号服): \n");
            Console.Write("  --  输入数字1连172.16.5.34(1号服): \n");
            Console.Write("  --  输入数字2连172.16.5.54(2号服): \n");
            protos.protoids tem = new protos.protoids();
            string readstr = Console.ReadLine();
            //windows服务器IP
            ip = "172.16.5.34";
            if (readstr.Trim() == "2")
            {
                ip = "192.168.9.224";//Linux服务器IP
            }
            port = 8888;
            netMgr = new NetFramework.NetworkManager();
            netMgr.Awake();
            //netMgr.SendConnect("192.168.9.107", 8888);
            netMgr.SendConnect(ip, port);
            netpack.netcommon.init();
            Task.Run(() => clientnet.Program.InputKey());
            while (true)
            {
                Thread.Sleep(100);
                netMgr.Update();
            }


        }
    }
}
