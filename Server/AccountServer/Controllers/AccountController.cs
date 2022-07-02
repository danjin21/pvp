using AccountServer.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountServer.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class AccountController : ControllerBase
    //{
    //}



    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;

        }

        // 원래 Post 인지 Get 인지에 따라 다른데 (REST) 그냥 POST로만 한다.
        [HttpPost]
        [Route("create")] // 이것 자체가 패킷이다.

        public CreateAccountPacketRes CreateAccount([FromBody] CreateAccountPacketReq req) // require
        {               // RES                                           // REQ

            CreateAccountPacketRes res = new CreateAccountPacketRes();

            // DB를 긁어온다.
            AccountDb account = _context.Accounts
                                .AsNoTracking() // 읽기만 한다.
                                .Where(a => a.AccountName == req.AccountName)
                                .FirstOrDefault();

            // 중복되는 아이디가 없는 경우
            if(account == null)
            {
                _context.Accounts.Add(new AccountDb()
                {
                    // AccountDbID 는 자동으로 만들어진다.
                    AccountName = req.AccountName,
                    Password = req.Password// 여기서 해시 비번화 작업 해야됨.                    
                });

                // 성공여부를 받는다.
                // 중복으로 2명이 같은 아이디로 회원가입 들어오더라도, 1명은 안되면 이걸로 false를 받는다.
                bool success = _context.SaveChangesEx();
                res.CreateOk = success;
            }
            // 중복되는 아이디가 있는 경우
            else
            {
                res.CreateOk = false;
                res.State = 0; // 0이면 중복되는 아이디가 있다는 뜻.


            }

            return res;
        }

        [HttpPost]
        [Route("login")]
        public LoginAccountPacketRes LoginAccount([FromBody] LoginAccountPacketReq req) // FromBody : Json 파일로부터 받아온다.
        {
            LoginAccountPacketRes res = new LoginAccountPacketRes();

            AccountDb account = _context.Accounts
                .AsNoTracking()
                .Where(a => a.AccountName == req.AccountName && a.Password == req.Password)
                .FirstOrDefault();

            if(account == null)
            {
                res.LoginOk = false;
            }
            else
            {
                res.LoginOk = true;

                // TODO 서버 목록을 가져온다.

                res.ServerList = new List<ServerInfo>()
                { 
                    new ServerInfo()
                    {
                        Name = "KOR",
                        Ip = "127.0.0.1",
                        CrowdedLevel = 0
                    },

                    new ServerInfo()
                    {
                        Name = "JP",
                        Ip = "127.0.0.1",
                        CrowdedLevel = 3,
                    }
                };

            }

            return res;
        }

    }


}
