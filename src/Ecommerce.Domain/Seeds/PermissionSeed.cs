namespace Ecommerce.Domain.Seeds;

public class PermissionSeed
{
    public static List<Entities.User.Permission> GetPermissions()
    {
        return
        [
            new() { Id = Guid.NewGuid(), Name = "Products.View", Module = "Products", Description = "View products" },
            new() { Id = Guid.NewGuid(), Name = "Products.Create", Module = "Products", Description = "Create products" },
            new() { Id = Guid.NewGuid(), Name = "Products.Edit", Module = "Products", Description = "Edit products" },
            new() { Id = Guid.NewGuid(), Name = "Products.Delete", Module = "Products", Description = "Delete products" },
            new() { Id = Guid.NewGuid(), Name = "Categories.View", Module = "Categories", Description = "View categories" },
            new() { Id = Guid.NewGuid(), Name = "Categories.Create", Module = "Categories", Description = "Create categories" },
            new() { Id = Guid.NewGuid(), Name = "Categories.Edit", Module = "Categories", Description = "Edit categories" },
            new() { Id = Guid.NewGuid(), Name = "Categories.Delete", Module = "Categories", Description = "Delete categories" },
            new() { Id = Guid.NewGuid(), Name = "Orders.View", Module = "Orders", Description = "View orders" },
            new() { Id = Guid.NewGuid(), Name = "Orders.Manage", Module = "Orders", Description = "Manage orders" },
            new() { Id = Guid.NewGuid(), Name = "Orders.Cancel", Module = "Orders", Description = "Cancel orders" },
            new() { Id = Guid.NewGuid(), Name = "Users.View", Module = "Users", Description = "View users" },
            new() { Id = Guid.NewGuid(), Name = "Users.Manage", Module = "Users", Description = "Manage users" },
            new() { Id = Guid.NewGuid(), Name = "Users.Delete", Module = "Users", Description = "Delete users" },
            new() { Id = Guid.NewGuid(), Name = "Reviews.View", Module = "Reviews", Description = "View reviews" },
            new() { Id = Guid.NewGuid(), Name = "Reviews.Moderate", Module = "Reviews", Description = "Moderate reviews" },
            new() { Id = Guid.NewGuid(), Name = "Coupons.View", Module = "Coupons", Description = "View coupons" },
            new() { Id = Guid.NewGuid(), Name = "Coupons.Create", Module = "Coupons", Description = "Create coupons" },
            new() { Id = Guid.NewGuid(), Name = "Coupons.Edit", Module = "Coupons", Description = "Edit coupons" },
            new() { Id = Guid.NewGuid(), Name = "Coupons.Delete", Module = "Coupons", Description = "Delete coupons" },
            new() { Id = Guid.NewGuid(), Name = "Inventory.View", Module = "Inventory", Description = "View inventory" },
            new() { Id = Guid.NewGuid(), Name = "Inventory.Manage", Module = "Inventory", Description = "Manage inventory" },
            new() { Id = Guid.NewGuid(), Name = "Reports.View", Module = "Reports", Description = "View reports" },
            new() { Id = Guid.NewGuid(), Name = "Settings.View", Module = "Settings", Description = "View settings" },
            new() { Id = Guid.NewGuid(), Name = "Settings.Edit", Module = "Settings", Description = "Edit settings" },
            new() { Id = Guid.NewGuid(), Name = "CMS.View", Module = "CMS", Description = "View CMS pages" },
            new() { Id = Guid.NewGuid(), Name = "CMS.Edit", Module = "CMS", Description = "Edit CMS pages" },
            new() { Id = Guid.NewGuid(), Name = "Dashboard.View", Module = "Dashboard", Description = "View dashboard" },
            new() { Id = Guid.NewGuid(), Name = "Marketing.View", Module = "Marketing", Description = "View marketing" },
            new() { Id = Guid.NewGuid(), Name = "Marketing.Manage", Module = "Marketing", Description = "Manage marketing" }
        ];
    }
}
