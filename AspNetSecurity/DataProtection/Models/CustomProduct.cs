using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataProtection.Models;

public partial class Product
{
    [NotMapped] // veri tabanında böyle bir kolon olmadığını belirtir
    public string EncrypedId { get; set; }

}
