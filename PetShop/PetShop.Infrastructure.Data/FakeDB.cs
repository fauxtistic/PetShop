using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public static class FakeDB
    {
        public static IEnumerable<Pet> pets = new List<Pet>();
        public static int petCounter = 1;
        public static IEnumerable<Owner> owners = new List<Owner>();
        public static int ownerCounter = 1;
    }
}
