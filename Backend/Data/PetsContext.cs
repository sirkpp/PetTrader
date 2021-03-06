﻿using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class PetsContext : DbContext
    {
        public PetsContext(DbContextOptions<PetsContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {

            modelbuilder.Entity<User>()
                .HasMany(d => d.Pets)
                .WithOne(p => p.PetOwner)
                .HasForeignKey(z => z.PetOwnerId);

            modelbuilder.Entity<Transaction>()
                .HasOne(d => d.Buyer)
                .WithMany(p => p.Buy_transactions)
                .HasForeignKey(z => z.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Transaction>()
                .HasOne(d => d.Seller)
                .WithMany(p => p.Sell_transactions)
                .HasForeignKey(z => z.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Transaction>()
                .HasOne(d => d.Pet)
                .WithOne(p => p.Transaction)
                .HasForeignKey<Transaction>(z => z.PetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelbuilder.Entity<PetImage>()
                .HasOne(d => d.Pet)
                .WithMany(p => p.PetImages)
                .HasForeignKey(z => z.PetId);
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PetImage> PetImages { get; set; }

    }
}
