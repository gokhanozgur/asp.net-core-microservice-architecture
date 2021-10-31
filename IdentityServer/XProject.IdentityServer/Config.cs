// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace XProject.IdentityServer
{
    public static class Config
    {

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            // This lines for not user aud jwt (ProjectDevelopment.txt step 9.8).
            new ApiResource("resource_catalog") {Scopes = {"catalog_fullpermission"}},
            new ApiResource("resource_photo_stock") {Scopes = {"photo_stock_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                // This scope declare which data in token (ProjectDevelopment.txt step 16.1).
                new IdentityResources.OpenId(), // This line must be declared.
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResource() { 
                    Name = "roles",
                    DisplayName = "Roles",
                    Description = "User roles",
                    UserClaims = new []{"role"}
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                // This lines for not user scope jwt (ProjectDevelopment.txt step 9.7).
                new ApiScope("catalog_fullpermission", "Catalog API Full Access"),
                //new ApiScope("catalog_readpermission", "Catalog API Read Access"),
                //new ApiScope("catalog_writepermission", "Catalog API Write Access"),
                new ApiScope("photo_stock_fullpermission", "Photo Stock API Full Access"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // No user client declaration (ProjectDevelopment.txt step 9.9).
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials, // When you use not user apis and client credentials not include refresh token.
                    AllowedScopes= {"catalog_fullpermission", "photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName}
                },
                // Resource owner grant type credential workflow client declaration (ProjectDevelopment.txt step 16.2)
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true, // When you use offline access this line must be declared.
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // When you use resource owner grant type credential workflow and this type include refresh token.
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess, // This line for refresh token.
                        "roles",
                        IdentityServerConstants.LocalApi.ScopeName
                    },
                    AccessTokenLifetime = 1*60*60, // Default 1 hour.
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds, // Set refresh token valid time (for this sample 60 day).
                    RefreshTokenUsage = TokenUsage.ReUse // The refresh token reusable in lifetime?
                },
            };
    }
}