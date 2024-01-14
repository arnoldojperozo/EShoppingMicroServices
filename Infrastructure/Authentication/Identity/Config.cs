// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("CatalogApiScope"),
                new ApiScope("BasketApiScope")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                //List of Microservices
                new ApiResource("Catalog", "Catalog.API")
                {
                    Scopes = { "CatalogApiScope" }
                },
                new ApiResource("Basket", "Basket.API")
                {
                    Scopes= { "BasketApiScope" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                //M2M - Machine to Machine Flow
                new Client
                {
                    ClientName = "Catalog API Client",
                    ClientId = "CatalogApiClient",
                    ClientSecrets = { new Secret("6bc16839-bb3e-49ce-ad33-ac11714e5e68".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "CatalogApiScope" }
                },
                new Client
                {
                    ClientName = "Basket API Client",
                    ClientId = "BasketApiClient",
                    ClientSecrets = { new Secret("6bc16839-cccc-dddd-ad33-ac11714e5e68".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "BasketApiScope" }
                }
            };
    }
}