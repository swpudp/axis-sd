using AxisOrder.QueryContract;
using AxisOrder.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Syllab;
using Syllab.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AxisOrder.WebApi.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/Account")]
    [TypeFilter(typeof(HandleErrorAttribute))]
    [AllowAnonymous]
    [EnableCors(ConstDefine.DefaultCrosPolicy)]
    public class AccountController : Controller
    {

        /// <summary>
        /// Jwt认证选项
        /// </summary>
        private readonly IOptions<JwtOptions> _jwtOptions;


        /// <summary>
        /// 用户查询
        /// </summary>
        private readonly IUserQuery _userQuery;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountController(IOptions<JwtOptions> jwtOptions, IUserQuery userQuery)
        {
            _jwtOptions = jwtOptions;
            _userQuery = userQuery;
        }

        /// <summary>
        /// 登录模型
        /// </summary>
        public class LoginModel
        {
            /// <summary>
            /// 登录名
            /// </summary>
            [Required(ErrorMessage = "请输入登录名")]
            public string LoginName { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            [Required(ErrorMessage = "请输入密码")]
            public string Password { get; set; }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]LoginModel model)
        {
            //验证输入
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrors();
                return Json(new Respond { IsSucceed = false, Message = string.Join(",", errors) });
            }

            //Jwt选项
            var jwtOption = _jwtOptions;

            //用户查询
            var query = _userQuery;

            //查询用户信息
            var userView = await query.QueryByLoginAsync(model.LoginName);

            if (userView == null)
            {
                return Json(new { IsSucceed = false, Message = "用户不存在!" });
            }

            //加密用户登录密码
            var modelPassword = model.Password.CreatePassword();

            //比较密码
            if (string.Compare(modelPassword, userView.Password, true) != 0)
            {
                return Json(new { IsSucceed = false, Message = "用户名和密码不匹配!" });
            }

            //登录用户特征集合
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name,userView.FullName),
                new Claim(ClaimTypes.Role,userView.GetType().Name),
                new Claim(ClaimTypes.Version,userView.Version.ToString()),
                new Claim("LoginName",userView.LoginName),
                new Claim("Id",userView.Id)
            };


            //加密key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Value.SecretKey));

            //创建认证令牌
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //生成Jwt Token
            var token = new JwtSecurityToken(jwtOption.Value.Issuer, jwtOption.Value.Audience, claims, DateTime.Now, DateTime.Now.AddMinutes(jwtOption.Value.Expires), creds);

            //写入Token
            var securityToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Json(new { IsSucceed = true, Message = "登录成功", userView.FullName, userView.LoginName, Token = securityToken, Code = 0 });
        }
    }
}