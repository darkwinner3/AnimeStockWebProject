using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AnimeStockWebProject.Common.GeneralAplicaitonConstants;
namespace AnimeStockWebProject.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName + "," + ModeratorRoleName)]
    public class AdminController : Controller
    {

    }
}
