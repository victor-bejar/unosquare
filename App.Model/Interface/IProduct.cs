namespace App.Model.Interface
{
    public interface IProduct
    {
        int ProductId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int? AgeRestriction { get; set; }
        string Company { get; set; }
        decimal Price { get; set; }
    }
}
