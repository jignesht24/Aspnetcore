namespace ModelBinder
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using ModelBinder.Model;

    public class CustomModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(User))
                return new CustomModelBinder();

            return null;
        }
    }
}
