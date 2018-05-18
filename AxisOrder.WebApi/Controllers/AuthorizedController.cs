using AxisOrder.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AxisOrder.WebApi.Controllers
{
    /// <summary>
    /// 授权访问的控基类制器
    /// </summary>
    [TypeFilter(typeof(HandleErrorAttribute))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorizedController : Controller
    {
    }
}