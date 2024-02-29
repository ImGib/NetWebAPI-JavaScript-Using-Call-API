namespace AppServer.DTO.CategoryDTO
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<ProductDTO.ProductResponse>? Products { get; set; }
    }
}
