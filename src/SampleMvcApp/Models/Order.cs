using System.ComponentModel.DataAnnotations;

namespace SampleMvcApp.Models;

public class Order
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Customer name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Order number")]
    public string OrderNumber { get; set; } = string.Empty;

    [Range(1, 100000)]
    public int Quantity { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
