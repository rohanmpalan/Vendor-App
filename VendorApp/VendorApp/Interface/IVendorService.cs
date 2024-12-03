using System;
using VendorApp.Model;

namespace VendorApp.Interface;

public interface IVendorService
{
      List<Vendor> GetVendors();
}
