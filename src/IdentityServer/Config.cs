// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4.Test;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
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
                    AllowedScopes = {"api1"}
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
                    AllowedScopes = {"api1"}
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "passowrd"
                }
            };
        }
    }
}