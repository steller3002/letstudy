using System.Security.Claims;
using Letstudy.Core;
using Letstudy.Models;
using Letstudy.Requests;
using Letstudy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Letstudy.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Tutor")]
public class TutorController() : ControllerBase
{
    
}