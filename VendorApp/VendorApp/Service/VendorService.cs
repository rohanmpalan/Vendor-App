using System;
using VendorApp.Interface;
using VendorApp.Model;

namespace VendorApp.Service;

public class VendorService : IVendorService
{
    public List<Vendor> GetVendors()
    {
        return new List<Vendor>()
        {
            new Vendor { Id = 1, Name = "Tech Supplies Co.", Location = "New York, USA" },
            new Vendor { Id = 2, Name = "GreenFields Organic", Location = "London, UK" },
            new Vendor { Id = 3, Name = "AutoParts Hub", Location = "Berlin, Germany" },
            new Vendor { Id = 4, Name = "Creative Studio Supplies", Location = "Sydney, Australia" },
            new Vendor { Id = 5, Name = "Global Textiles", Location = "Mumbai, India" },
            new Vendor { Id = 6, Name = "Foodie's Paradise", Location = "Toronto, Canada" },
        };
    }
}
