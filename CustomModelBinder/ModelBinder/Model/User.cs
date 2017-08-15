namespace ModelBinder.Model
{
    using Microsoft.AspNetCore.Mvc;
        
    //[ModelBinder(BinderType = typeof(CustomModelBinder))]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class User1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
