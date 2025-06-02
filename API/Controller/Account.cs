using System;
using API.Common;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

public static class Account
{
    // This class is intended to handle user account operations such as registration, login, and profile management.
    // It will use the AppDbContext for database interactions and Identity for user management.

    // Example methods could include:
    // - RegisterUser: To create a new user account.
    // - LoginUser: To authenticate a user and issue a JWT token.
    // - GetUserProfile: To retrieve the user's profile information.
    // - UpdateUserProfile: To allow users to update their profile details.

    public static RouteGroupBuilder MapAccountEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/account").WithTags("Account");

        group.MapPost("/register", async (
            HttpContext context,
            UserManager<AppUser> userManager,
            [FromForm] string FullName,
            [FromForm] string Email,
            [FromForm] string Password,
            [FromForm] string UserName) =>
        {
            var userFromDb = await userManager.FindByEmailAsync(Email);
            if (userFromDb != null)
            {
                return Results.BadRequest(Response<string>.ErrorResponse("User with this email already exists",""));
            }

            var newUser = new AppUser
            {
                FullName = FullName,
                Email = Email,
                UserName = UserName,
            };

            var result = await userManager.CreateAsync(newUser, Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Results.BadRequest(Response<string>.ErrorResponse($"User creation failed: {errors}", ""));
            }

            return Results.Ok(Response<AppUser>.SuccessResponse(newUser, "User created successfully"));
        }).DisableAntiforgery();

        return group;
    }

    
}