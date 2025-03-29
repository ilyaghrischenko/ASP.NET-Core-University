using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LearningProgressTrackingSystem.Application.Contracts.Identity;
using LearningProgressTrackingSystem.Application.Options;
using LearningProgressTrackingSystem.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LearningProgressTrackingSystem.Application.Services.Identity;

public sealed class JwtBearerService(IOptions<JwtOptions> jwtOptions) : IJwtBearerService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    
    public ClaimsIdentity GetIdentity(AccountEntity account)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, account.Id.ToString()),
            new(ClaimsIdentity.DefaultRoleClaimType, account.Role.ToString())
        };
        
        ClaimsIdentity identity = new(claims, "Token",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        
        return identity;
    }

    public string GetToken(ClaimsIdentity identity)
    {
        DateTime timeNow = DateTime.UtcNow;
        JwtSecurityToken jwt = new(
            issuer: _jwtOptions.ISSUER,
            audience: _jwtOptions.AUDIENCE,
            notBefore: timeNow,
            claims: identity.Claims,
            expires: timeNow.Add(TimeSpan.FromDays(_jwtOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(_jwtOptions.GetKey(), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}