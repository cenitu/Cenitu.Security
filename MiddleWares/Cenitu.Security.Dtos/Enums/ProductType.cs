using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Cenitu.Security.Dtos.Enums
{
    public enum ProductType
    {
        [Display(Name = "Raw Material")]
        RawMaterial,
        [Display(Name ="Seni Finished")]
        SemiFinished,
        [Display(Name = "Consumable")]
        Consumable,
        [Display(Name = "Product")]
        Product,
    }
}
