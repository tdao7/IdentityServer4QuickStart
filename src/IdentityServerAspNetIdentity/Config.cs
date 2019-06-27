﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Test;

namespace IdentityServerAspNetIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            //Defining an API Resource
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return defindListUser();
        }

        private static IEnumerable<Client> defindListUser()
        {
            return new List<Client>
            {
                //Resource owner passowrd grant clinet
                new Client
                {
                    ClientId = "syn.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"api1", "Home/Index"}
                },

                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // where to redirect to after login
                    RedirectUris = {"http://localhost:5002/signin-oidc"},

                    // where to redirect to after logout
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }


        private static List<Client> defindListClient()
        {
            //Defining the client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    //No interactive user, use the clientId/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    //secret for authentiocation
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    //Scopes that client has access to 
                    AllowedScopes = {"api1", "Home/Index"}
                }
            };
        }
    }
}