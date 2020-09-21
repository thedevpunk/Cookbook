using System;
using Core.Entities;

namespace Api
{
    public static class TestData
    {
        public static User TestUser {
            get {
                return new User {
                    Id = Guid.Parse("ca584a20-a301-48f9-94c6-835298668e19"),
                    Email = "danny.ocean@test.com",
                    Username = "Danny Ocean"
                };
            }
        }
    }
}