using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Details { get; set; }

    // Mark TrackingNumber as ValidateNever to avoid validation errors during form submission
    [ValidateNever]
    public string TrackingNumber { get; set; }

    public string ShippingStatus { get; set; } = "Pending"; // Default status

}
