using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




// 가입 : Req ( 클라 -> 서버 )
public class CreateAccountPacketReq
{
    public string AccountName { get; set; }
    public string Password { get; set; }

}

// 가입 : Res ( 서버 -> 클라 )
public class CreateAccountPacketRes
{
    public bool CreateOk { get; set; } // 만들어졌다.
    public int State { get; set; } // 상태(사유)

}


// 로그인 : Req ( 클라 -> 서버 )
public class LoginAccountPacketReq
{
    public string AccountName { get; set; }
    public string Password { get; set; }
}

// 로그인 : Res ( 서버 -> 클라 )
public class LoginAccountPacketRes
{
    public bool LoginOk { get; set; }
    public List<ServerInfo> ServerList {get;set;} = new List<ServerInfo>(); // 한국서버 일본서버 미국서버 등으로 구분을 지어보자 => 사실 그냥 한서버로 해도됨

}

// 서버 클래스
public class ServerInfo
{
    public string Name { get; set; }
    public string Ip { get; set; }
    public int CrowdedLevel { get; set; }
}




