//using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NetFramework
{
    public class NetworkManager //: MonoBehaviour
    {
        public static bool isOpenEncry = false; //�Ƿ�������
        public static bool isEncryOffsetEnable = false; //�����Ƿ����ö�̬ƫ��
        public static int s_protoNumberLen = 2;
        public static int s_PackLen = 2;
        private SocketClient socket;
        static readonly object m_lockObject = new object();
        static Queue<KeyValuePair<int, byte[]>> mEvents = new Queue<KeyValuePair<int, byte[]>>();

        public SocketClient SocketClient {
            get { 
                if (socket == null)
                    socket = new SocketClient();
                return socket;                    
            }
        }
        public void SetEncry(bool bencry)
        {
            isOpenEncry = bencry;
        }

        public void Awake() {
            Init();
        }

        void Init() {
            SocketClient.OnRegister();
        }

        ///------------------------------------------------------------------------------------
        public static void AddEvent(int _event, byte[] data)
        {
            lock (m_lockObject) {
                mEvents.Enqueue(new KeyValuePair<int, byte[]>(_event, data));
            }
        }

        /// <summary>
        /// ����Command�����ﲻ����ķ���˭��
        /// </summary>
        public void Update() {
            if (mEvents.Count > 0) {
                while (mEvents.Count > 0) {
                    KeyValuePair<int, byte[]> _event = mEvents.Dequeue();
                    HandlePack(_event.Key, _event.Value);
                }
            }
        }

        public void HandlePack(int id,byte[] buff)
        {
            int bigid = ((id >> 8) & 0xff);
             int smallid = (id & 0xff);
             netpack.netcommon.netcommonHandle(bigid, smallid, buff);
        }
        /// <summary>
        /// ������������
        /// </summary>
        public void SendConnect(string host, int port)
        {
            CEncryptClient.instance.Reset();
            SocketClient.SendConnect(host, port);
        }

        /// <summary>
        /// ����SOCKET��Ϣ
        /// </summary>
        public void SendMessage(int bigid,int smallid,byte[] buffer) {
            byte[] newbuffer = null;
            if (NetworkManager.isOpenEncry) { 
                //����
                byte[] encrybuffer = new byte[buffer.Length +  NetworkManager.s_protoNumberLen];
                encrybuffer[0] = (byte)(bigid & 0xff); //ѹ���Э���
                encrybuffer[1] = (byte)(smallid & 0xff);//ѹ����Э���
                System.Buffer.BlockCopy(buffer, 0, encrybuffer,  NetworkManager.s_protoNumberLen, buffer.Length);
                buffer = CEncryptClient.instance.SendEncrypt(encrybuffer, encrybuffer.Length, NetworkManager.isEncryOffsetEnable);
                //
                newbuffer = new byte[buffer.Length + NetworkManager.s_PackLen];
                Converter.write_size(newbuffer, buffer.Length);
                //
                System.Buffer.BlockCopy(buffer, 0, newbuffer, NetworkManager.s_PackLen , buffer.Length);
            }
            else
            {
                newbuffer = new byte[buffer.Length + NetworkManager.s_PackLen + NetworkManager.s_protoNumberLen];
                Converter.write_size(newbuffer, buffer.Length + NetworkManager.s_protoNumberLen);
                newbuffer[2] = (byte)(bigid & 0xff); //ѹ���Э���
                newbuffer[3] = (byte)(smallid & 0xff);
                System.Buffer.BlockCopy(buffer, 0, newbuffer, NetworkManager.s_PackLen + NetworkManager.s_protoNumberLen, buffer.Length);
            }
            SocketClient.SendMessage(newbuffer);
        }

        /// <summary>
        /// ��������
        /// </summary>
        new void OnDestroy() {
            SocketClient.OnRemove();
            Debug.Log("~NetworkManager was destroy");
        }
    }
}