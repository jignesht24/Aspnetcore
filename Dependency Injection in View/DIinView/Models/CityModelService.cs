namespace DIinView.Models
{
    using System.Collections.Generic;
    public class CityModelService
    {
        public List<string> GetCities()
        {
            return new List<string>()
            {
                "Ahmedabad", "Gandhinagar", "Bhavnagar", "Surat", "Bhuj"
            };
        }
    }
}
