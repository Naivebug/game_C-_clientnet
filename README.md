--create by NaiveBug^梁疯
1下载编译运行,如果Rlease不行那就改成Debug..重编译下.就可以运行了..
2里面代码可以连入本人的skynet_gameserver_study这个Git工程..你输入1就自动登录进这个工程服务器了..适合客户端连服务器学习
------
注意:这份客户端代码是Unity3d里的Ulua里的C#网络库的代码,我只是提取出来,加上了Protobuf,就不用自己再造车啦,可以直接用~
------
文件夹解译:
protos:protobuf文件,使用google的protobuf生成一个C#文件,复制进来即可使用
NetworkManager:unity3d开源引擎ulua用C#写的网络库,直接提取出来..
netpack:网络库收到包后,直接通过这里分发消息派发和处理.ps:这里因为测试,收到包后马上发送服务器C2SLoginTest进行登录.
netgm:gm指令测试,监听了输入键,输入0重连服务器,输入1就发送到服务器测试练代码具体自己实现.具体看HandleGM,要练习客户端和服务器就通过这里就可以啦~
----
如果要添加自己代码,如发送大协议号10,小协议号20,buffer如果不想protobuf就自己随意写个字符串,那就在HanleGM自己添加clientnet.Program.netMgr.SendMessage(10, 10, buffer);
如果你想先搞protobuf那就先百度谷哥解决protobuf编写和生成C#文件,服务器对应解码即可..
---


