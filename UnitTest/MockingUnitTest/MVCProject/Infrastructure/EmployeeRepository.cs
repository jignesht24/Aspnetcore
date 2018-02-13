using MVCproject.Core;

namespace MVCproject.Infrastructure
{
    public class EmployeeRepository : IGetDataRepository
    {
        public string GetNameById(int id)
        {
            string name;
            if (id == 1)
            {
                name = "Jignesh";
            }
            else if (id == 2)
            {
                name = "Rakesh";
            }
            else
            {
                name = "Not Found";
            }
            return name;
        }
    }
}
