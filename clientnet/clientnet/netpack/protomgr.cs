using Google.Protobuf;
using System.Collections.Generic;
namespace protos
{
    delegate void Fn2(string name ,object data);
    class protomgr
    {
        private static protomgr uniqueInstance;
        public static protomgr GetInstance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (uniqueInstance == null)
            {
                uniqueInstance = new protomgr();
            }
            return uniqueInstance;
        }

        static Dictionary<int, Fn2> funs = new Dictionary<int, Fn2>();
        public protomgr()
        {
        }
        public static void ParseFrom(int id,byte[] data)
        {
            if(protoids.ids.ContainsKey(id))
            {
                object tem = protoids.ids[id](data);
                if (funs.ContainsKey(id))
                {
                    funs[id](protoids.idnames[id], tem);
                }
                else
                {
                    System.Console.WriteLine("未配置函数net ParseFrom not config: {0} {1} {2}", id, protoids.idnames[id], data);
                }
            }else
            {
                System.Console.WriteLine("协议未编译没用这个IDnet ParseFrom not config: {0}  {1}", id, data);
            }
        }
        public static void regestFuns(string name,Fn2 fn)
        {
            if (protoids.nameids.ContainsKey(name))
            {
                int id = protoids.nameids[name];
                funs.Add(id, fn);
            } else if (protoids.nameids.ContainsKey("Protocol." + name)) //default "Protocol." 
            {
                int id = protoids.nameids["Protocol." + name];
                funs.Add(id, fn);
            }
        }
        public static void SendMessage(string name, byte[] buffer)
        {
            int id = 0;
            if (protoids.nameids.ContainsKey(name))
            {
                id = protoids.nameids[name];
            }
            else if (protoids.nameids.ContainsKey("Protocol." + name)) //default "Protocol." 
            {
                id = protoids.nameids["Protocol." + name];
            }
            if ( id > 0 )
            {
                clientnet.Program.netMgr.SendMessage(id >> 8, id % 256, buffer);
            }
            
        }

    }
}
