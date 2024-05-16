namespace Web_day_tieng_Anh.Models
{
    public class CartBuyCourse
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.CourseId == item.CourseId);
            Items.Add(item);
            
        }
        public void RemoveItem(int courseId)
        {
            Items.RemoveAll(i => i.CourseId == courseId);
        }
        public decimal CalculateTotalCost()
        {
            return Items.Sum(item => item.Price);
        }


    }
}
