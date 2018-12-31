using Microsoft.EntityFrameworkCore;
using PeopleApp.Data;
using System.Linq;

namespace PeopleApp.Common
{
    public static class UserExtentions
    {
        public static IQueryable<User> OrderByAge(this IQueryable<User> queryable, bool ascending)
        {
            return ascending
                ? queryable.OrderBy(u => u.Age)
                : queryable.OrderByDescending(u => u.Age);
        }

        public static User CreateByName(this DbSet<User> users, string Name)
        {
            var result = users.Add(new User { Givenname = Name });
            return result.Entity;

        }


    }
}
