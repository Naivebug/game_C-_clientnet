using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
namespace clientnet
{
    class netgm
    {
        private static netgm uniqueInstance;

        // 定义私有构造函数，使外界不能创建该类实例
        private netgm()
        {
        }

        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static netgm GetInstance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (uniqueInstance == null)
            {
                uniqueInstance = new netgm();
            }
            return uniqueInstance;
        } 
        //---------------------------------------------------------------------
        public void HandleGM(string common)
        {
            Console.WriteLine("handle gm common {0}", common);
            if (common == "0")
            {
                //netdll.GetInstance().Connet();
                Program.netMgr.SendConnect(Program.ip, Program.port);

            }
            else if (common == "1")
            {
                sendToTest();
            }
            else if (common == "2")
            {
                sendToPet();
            }
            else if (common == "3")
            {
                sendToMoveTest();
            }
            else
            {
                Console.WriteLine("---error:未找到输入的这个命令: {0}", common);
            }
        }
        public void sendToTest()
        {

            Cs.Person tem = new Cs.Person
            {
                Id = 1234,
                Name = "John Doe",
                Email = "jdoe@example.com",
                Phones = { new Cs.Person.Types.PhoneNumber { Number = "555-4321", Type = Cs.Person.Types.PhoneType.Home } }
            };

            byte[] buffer  = tem.ToByteArray();
            //byte[] buffer = System.Text.Encoding.Default.GetBytes(tem.ToString().ToCharArray());
            //netdll.GetInstance().WriteMsg(1, 123, buffer);
            clientnet.Program.netMgr.SendMessage(35, 255, buffer);
        }
        public void sendToPet()
        {
           /* Protocol.C2SPetIntimacy tem = new Protocol.C2SPetIntimacy();
            tem.Id = 1;
            tem.Itemid = 1000;

            byte[] buffer = tem.Build().ToByteArray();
            clientnet.Program.netMgr.SendMessage(0x22, 1, buffer);
            Console.WriteLine("---sendToPet--:{0} {1}-", tem.Id, tem.Itemid);*/
            
        }
        public void sendToMoveTest()
        {
            /*Protocol.C2SPetIntimacy.Builder tem = new Protocol.C2SPetIntimacy.Builder();
            tem.Id = 1;
            tem.Itemid = 1000;

            byte[] buffer = tem.Build().ToByteArray();
            clientnet.Program.netMgr.SendMessage(10, 0, buffer);
            Console.WriteLine("---sendToPet--:{0} {1}-", tem.Id, tem.Itemid);*/

        }

    }
}
