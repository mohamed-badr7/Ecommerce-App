namespace Models;

public class Products
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal? Rate { get; set; }
    public string? Image { get; set; }
    public string? Images { get; set; }

    public int? CategoriesId { get; set; }

    public Categories? Categories { get; set; }
}
