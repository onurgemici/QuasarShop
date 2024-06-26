﻿using Microsoft.EntityFrameworkCore;
using QuasarShopData;
using System.Text.RegularExpressions;

namespace QuasarShopServices;

public interface IProductsService
{
    IQueryable<Product> GetAll();

    Task<Product?> GetById(Guid id);

    Task<Product?> GetByIdWithCatalogs(Guid id);

    Task Create(Product item);

    Task Create(string name, bool enabled, Guid userId, decimal price, int discountRate, string? description, string? image, IEnumerable<string>? images, IEnumerable<Guid> catalogs);

    Task Update(Product item, IEnumerable<Guid> catalogs, IEnumerable<string> images, IEnumerable<Guid> imagesToDelete);

    Task Delete(Guid id);

    string? GetProductImage(Guid id);
    byte[]? GetProductImageBytes(Guid id);
}


public class ProductsService : IProductsService
{
    private readonly AppDbContext context;

    public ProductsService(
        AppDbContext context
        )
    {
        this.context = context;
    }

    public async Task Create(Product item)
    {
        item.DateCreated = DateTime.UtcNow;

        await context.Products.AddAsync(item);
        await context.SaveChangesAsync();
    }

    public async Task Create(string name, bool enabled, Guid userId, decimal price, int discountRate, string? description, string? image, IEnumerable<string>? images, IEnumerable<Guid> catalogs)
    {
        var selectedCatalogs = context.Catalogs.Where(p => catalogs.Any(q => q == p.Id)).ToList();
        await Create(new Product
        {
            UserId = userId,
            Name = name,
            Enabled = enabled,
            Price = price,
            DiscountRate = discountRate,
            Description = description,
            Image = image,
            ProductImages = images?.Select(p => new ProductImage { UserId = userId, Image = p }).ToList(),
            Catalogs = selectedCatalogs
        });
    }
    public async Task Update(Product item, IEnumerable<Guid> catalogs, IEnumerable<string> images, IEnumerable<Guid> imagesToDelete)
    {
        var selectedCatalogs = context.Catalogs.Where(p => catalogs.Any(q => q == p.Id)).ToList();
        item.Catalogs.Clear();
        item.Catalogs = selectedCatalogs;
        images?.Select(p => new ProductImage { Image = p }).ToList().ForEach(item.ProductImages.Add);
        context.ProductImages.Where(p => imagesToDelete.Any(q => q == p.Id)).ExecuteDelete();
        context.Products.Update(item);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var item = await GetById(id);
        if (item is null)
            throw new Exception("Katalog bulunamadı");
        context.Products.Remove(item);
        await context.SaveChangesAsync();
    }

    public IQueryable<Product> GetAll()
    {
        return context.Products.AsQueryable<Product>();
    }

    public Task<Product?> GetById(Guid id)
    {
        return context.Products.SingleOrDefaultAsync(p => p.Id == id);
    }
    public Task<Product?> GetByIdWithCatalogs(Guid id)
    {
        return context.Products.Include(p=>p.ProductImages).Include(p=>p.Catalogs).SingleOrDefaultAsync(p => p.Id == id);
    }

    public string? GetProductImage(Guid id)
    {
        return GetAll().Select(p => new { p.Id, p.Image }).SingleOrDefault(p => p.Id == id)?.Image;
    }

    public byte[]? GetProductImageBytes(Guid id)
    {
        return Convert.FromBase64String(GetProductImage(id).Replace("data:image/jpeg;base64,", ""));
    }

}
