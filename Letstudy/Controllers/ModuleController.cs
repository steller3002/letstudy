using System.Security.Claims;
using Letstudy.Extensions;
using Letstudy.Identity;
using Letstudy.Models;
using Letstudy.Requests;
using Letstudy.Responses;
using Letstudy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Letstudy.Controllers;

[ApiController]
[Route("api")]
[Authorize(Roles = "Tutor, Student")]
public class ModuleController(
    BoardService boardService,
    ModuleService moduleService,
    UserService userService,
    IAuthorizationService authorizationService) : ControllerBase
{
}