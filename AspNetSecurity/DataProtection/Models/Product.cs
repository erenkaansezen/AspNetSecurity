using System;
using System.Collections.Generic;

namespace DataProtection.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Color { get; set; }

    public int? ProductCategoryId { get; set; }
}
