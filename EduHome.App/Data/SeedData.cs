using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new EduHomeDbContext(serviceProvider.GetRequiredService<DbContextOptions<EduHomeDbContext>>()))
        {
            if (!context.Subscribes.Any())
            {
                // Add some initial subscribers to the database
                context.Subscribes.Add(new Subscribe { Email = "subscriber1@example.com" });
                context.Subscribes.Add(new Subscribe { Email = "subscriber2@example.com" });
                context.Subscribes.Add(new Subscribe { Email = "subscriber3@example.com" });

                context.SaveChanges();
            }
        }
    }
}
