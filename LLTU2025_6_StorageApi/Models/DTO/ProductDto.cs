using System.ComponentModel.DataAnnotations;

namespace LLTU2025_6_StorageApi.Models.DTO;

public class ProductDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    
    public int Price { get; set; }

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    public string Shelf { get; set; } = string.Empty;
    
    public int Count { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;
    public int InventoryValue => Price * Count;
}
