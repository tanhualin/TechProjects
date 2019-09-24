using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyDataCenter.Common.Mvc;
using MyDataCenter.Models;
using MyDataCenter.Models.SystemManage;
using NETCore.Encrypt;
using Newtonsoft.Json;

namespace MyDataCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBaseEx
    {
        private JwtSettings setting;
        public LoginController(IOptions<JwtSettings> options)
        {
            setting = options.Value;
        }

        [HttpPost]
        public IActionResult Login([FromBody]SmartLoginModel login)
        {
            SmartHttpResult<JwtTokenModel> result = new SmartHttpResult<JwtTokenModel>();
            try
            {
                var entity = DAL.SystemManage.SmartUser.GetEntityByName(login.UserName);
                if (entity != null)
                {
                    var encrypted = EncryptProvider.AESEncrypt(login.PassWord, entity.Salt);
                    if (entity.PassWord == encrypted)
                    {
                        //重新加密
                        var Saltkey = Guid.NewGuid().ToString("N");
                        var decrypted = EncryptProvider.AESEncrypt(login.PassWord, Saltkey);
                        //替换密码与密钥
                        DAL.SystemManage.SmartUser.utlSmartUserByName(login.UserName, decrypted, Saltkey);

                        var roles = DAL.SystemManage.SmartRole.GetRolesByUserIdx(entity.Idx);
                        var claims = new List<Claim>() { new Claim(ClaimTypes.Name, login.UserName) };
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(setting.SecretKey));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var tokenModel = new JwtSecurityToken(
                            setting.Issuer,
                            setting.Audience,
                            claims,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(setting.TokenExpires),
                            creds);

                        var jwtToken = new JwtTokenModel();
                        jwtToken.Token = new JwtSecurityTokenHandler().WriteToken(tokenModel);

                        var tokenUser = new TokenUserModel();
                        tokenUser.Name = entity.UserName;
                        tokenUser.Email = entity.Email;
                        tokenUser.Phone = entity.Phone;
                        tokenUser.Avatar = entity.Avatar;
                        jwtToken.User = tokenUser;

                        result.Set(true, jwtToken);

                        return JsonEx(result);
                    }
                    else
                    {
                        result.Set(false, "用户密码不正确！");
                    }
                }
                else
                {
                    result.Set(false, "用户不存在！");
                }
            }
            catch (Exception err)
            {
                result.Set(false, err.Message);
            }
            return JsonEx(result);
        }
    }
}