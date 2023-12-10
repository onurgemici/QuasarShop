﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuasarShopData;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public string? CargoTrackingNumber { get; set; }

    public User? User { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();

}


public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasIndex(p => new { p.Date })
            .IsDescending();

        builder
            .Property(p => p.Date)
            .HasColumnType("smalldatetime");

        builder
            .HasMany(p => p.OrderDetails)
            .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}