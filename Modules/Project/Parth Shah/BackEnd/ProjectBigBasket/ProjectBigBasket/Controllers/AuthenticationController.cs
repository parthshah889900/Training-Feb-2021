﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ProjectBigBasket.Models;

namespace ProjectBigBasket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<BigBasketUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<BigBasketUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;

        }
            [HttpPost]
            [Route("register")]
            public async Task<IActionResult> Register([FromBody] Register model)
            {
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                BigBasketUser user = new BigBasketUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)

                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }

            [HttpPost]
            [Route("login")]
            public async Task<IActionResult> Login([FromBody] Login model)
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(5),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

               var response = new Response { Status = "Success", Message = "Login successfully!" };
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                response.Status , response.Message
                });


             
                }
            return Ok(new Response { Status = "Error", Message = "Login id or password incorrect" } );
            }

            //method for register admin
            [HttpPost]
            [Route("registerAdmin")]
            public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
            {
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                BigBasketUser user = new BigBasketUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)

                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                if (await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                }

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }


            //method for register user 

            [HttpPost]
            [Route("registerUser")]
            public async Task<IActionResult> RegisterUser([FromBody] Register model)
            {
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                BigBasketUser user = new BigBasketUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)

                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));



                if (await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.User);
                }



                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
        }
    }
 
