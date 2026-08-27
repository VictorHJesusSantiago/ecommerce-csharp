using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminInventoryController : Controller
{
    public IActionResult Index() => View("Inventory");
    public IActionResult Warehouses() => View("Warehouses");
    public IActionResult Suppliers() => View("Suppliers");
    public IActionResult StockMovements() => View("StockMovements");
    public IActionResult PurchaseOrders() => View("PurchaseOrders");
    public IActionResult AuditLog() => View("AuditLog");
    public IActionResult AdjustStock() => View("AdjustStock");
    public IActionResult TransferStock() => View("TransferStock");
    public IActionResult CreateWarehouse() => View("CreateWarehouse");
    public IActionResult CreateSupplier() => View("CreateSupplier");
    public IActionResult CreatePurchaseOrder() => View("CreatePurchaseOrder");
    public IActionResult WarehouseDetail(Guid id) { ViewBag.WarehouseId = id; return View("WarehouseDetail"); }
    public IActionResult SupplierDetail(Guid id) { ViewBag.SupplierId = id; return View("SupplierDetail"); }
    public IActionResult PurchaseOrderDetail(Guid id) { ViewBag.PoId = id; return View("PurchaseOrderDetail"); }
    public IActionResult EditWarehouse(Guid id) { ViewBag.WarehouseId = id; return View("EditWarehouse"); }
    public IActionResult EditSupplier(Guid id) { ViewBag.SupplierId = id; return View("EditSupplier"); }

    [HttpPost]
    public IActionResult AdjustStockPost(Ecommerce.Application.DTOs.Inventory.AdjustStockRequest request)
    {
        return RedirectToAction("StockMovements");
    }

    [HttpPost]
    public IActionResult TransferStockPost(Ecommerce.Application.DTOs.Inventory.TransferStockRequest request)
    {
        return RedirectToAction("StockMovements");
    }

    [HttpPost]
    public IActionResult CreateWarehousePost(Ecommerce.Application.DTOs.Inventory.CreateWarehouseRequest request)
    {
        return RedirectToAction("Warehouses");
    }

    [HttpPost]
    public IActionResult CreateSupplierPost(Ecommerce.Application.DTOs.Inventory.CreateSupplierRequest request)
    {
        return RedirectToAction("Suppliers");
    }

    [HttpPost]
    public IActionResult CreatePurchaseOrderPost(Ecommerce.Application.DTOs.Inventory.CreatePurchaseOrderRequest request)
    {
        return RedirectToAction("PurchaseOrders");
    }

    [HttpPost]
    public IActionResult EditWarehousePost(Ecommerce.Application.DTOs.Inventory.UpdateWarehouseRequest request)
    {
        return RedirectToAction("Warehouses");
    }

    [HttpPost]
    public IActionResult EditSupplierPost(Ecommerce.Application.DTOs.Inventory.UpdateSupplierRequest request)
    {
        return RedirectToAction("Suppliers");
    }
}
