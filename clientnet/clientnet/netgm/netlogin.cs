using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
namespace clientnet
{
    class netlogin
    {
        private static netlogin uniqueInstance;

        // 定义私有构造函数，使外界不能创建该类实例
        private netlogin()
        {
            //注册协议返回
            protos.protomgr.regestFuns("Protocol.S2CHello", S2CHello);
            protos.protomgr.regestFuns("Protocol.S2CLoginSuccess", S2CLoginSuccess);
        }

        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static netlogin GetInstance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (uniqueInstance == null)
            {
                uniqueInstance = new netlogin();
            }
            return uniqueInstance;
        }
        //---------------------------------------------------------------------
        public void Handle(string name, object data) //可以当做通用接口,在这里name区分
        {
            Console.WriteLine("handle  common {0}", name);
            if (name == "Protocol.S2CHello")
            {

            }
            else if (name == "Protocol.S2CLoginSuccess")
            {
            }
            else
            {
                Console.WriteLine("---error:未找到输入的这个命令: {0}", name);
            }
        }
        public void S2CHello(string name, object data)
        {
            Console.WriteLine("S2CHello: {0} {1}", name, data);

            C2SLoginTest();
        }
        public void S2CLoginSuccess(string name, object data)
        {
            Console.WriteLine("S2CLoginSuccess: {0} {1}", name, data);
        }
        public void C2SLoginTest()
        {
            Protocol.C2SLogin tem = new Protocol.C2SLogin
            {
                Account = "AccountTest1",
                Pwd = "pwd",
                Servernumber = 1,
                Mac = "mac",
            };
            protos.protomgr.SendMessage("Protocol.C2SLogin", tem.ToByteArray());
        }
    }
}
