using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly date)
        {
            return DateTime.UtcNow.Year - date.Year - (DateTime.UtcNow.Month < date.Month || (DateTime.UtcNow.Month == date.Month && DateTime.UtcNow.Day < date.Day) ? 1 : 0);
        }
    }
}