﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MFAUDIT.Persistence
{
    public interface ICryptoUtil
    {
        
        string CreateToken1( string empId);

    }
    public class CryptoUtil: ICryptoUtil
    {
        const int SALT_SIZE = 14;

       
       
       
       
        public string CreateToken1( string empId)
        {
            var tokenString = "";
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("myEncryptionKey@143#"));

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Name ,empId+"," +DateTime.Now.ToLongTimeString())
                        };
                var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                notBefore: DateTime.Now,
                signingCredentials: signinCredentials
                );
                tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            }
            catch (Exception e) { }

            return tokenString;
        }

    }
}
