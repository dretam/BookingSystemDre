using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.DataAccess.Models;

public partial class BookingSystemContext : DbContext
{
    public BookingSystemContext()
    {
    }

    public BookingSystemContext(DbContextOptions<BookingSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChlMenu> ChlMenus { get; set; }

    public virtual DbSet<ChlResource> ChlResources { get; set; }

    public virtual DbSet<MstBookingCode> MstBookingCodes { get; set; }

    public virtual DbSet<MstGlobalSetup> MstGlobalSetups { get; set; }

    public virtual DbSet<MstLocation> MstLocations { get; set; }

    public virtual DbSet<MstMenu> MstMenus { get; set; }

    public virtual DbSet<MstResource> MstResources { get; set; }

    public virtual DbSet<MstRole> MstRoles { get; set; }

    public virtual DbSet<MstRoom> MstRooms { get; set; }

    public virtual DbSet<MstUser> MstUsers { get; set; }

    public virtual DbSet<TrxCatering> TrxCaterings { get; set; }

    public virtual DbSet<TrxHistory> TrxHistories { get; set; }

    public virtual DbSet<TrxMstRoomChlResource> TrxMstRoomChlResources { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BookingSystem;Username=postgres;Password=D@rknessRuin68");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChlMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ChlMenu_pkey");

            entity.ToTable("ChlMenu");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasPrecision(6);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasDefaultValue(false);

            entity.HasOne(d => d.MstMenu).WithMany(p => p.ChlMenus)
                .HasForeignKey(d => d.MstMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_mstMenu_chlMenu");
        });

        modelBuilder.Entity<ChlResource>(entity =>
        {
            entity.HasKey(e => e.ResourceCode).HasName("ChlResource_pkey");

            entity.ToTable("ChlResource");

            entity.Property(e => e.ResourceCode).HasMaxLength(255);
            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.Status).HasDefaultValue(false);

            entity.HasOne(d => d.Resource).WithMany(p => p.ChlResources)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_mstRes_chlRes");
        });

        modelBuilder.Entity<MstBookingCode>(entity =>
        {
            entity.HasKey(e => e.BookingCode).HasName("MstBookingCode_pkey");

            entity.ToTable("MstBookingCode");

            entity.Property(e => e.BookingCode).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasPrecision(6);
            entity.Property(e => e.DeletedDate).HasPrecision(6);
            entity.Property(e => e.Status).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasPrecision(6);
        });

        modelBuilder.Entity<MstGlobalSetup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MstGlobalSetup_pkey");

            entity.ToTable("MstGlobalSetup");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasPrecision(6);
            entity.Property(e => e.DeletedDate).HasPrecision(6);
            entity.Property(e => e.ParameterCode).HasMaxLength(100);
            entity.Property(e => e.ParameterDesc).HasMaxLength(8000);
            entity.Property(e => e.ParameterName).HasMaxLength(255);
            entity.Property(e => e.ParameterValue).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasPrecision(6);
        });

        modelBuilder.Entity<MstLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MstLocation_pkey");

            entity.ToTable("MstLocation");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasPrecision(6);
            entity.Property(e => e.DeletedDate).HasPrecision(6);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasPrecision(6);
        });

        modelBuilder.Entity<MstMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MstMenu_pkey");

            entity.ToTable("MstMenu");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status).HasDefaultValue(false);

            entity.HasOne(d => d.Role).WithMany(p => p.MstMenus)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menu_role");
        });

        modelBuilder.Entity<MstResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MstResource_pkey");

            entity.ToTable("MstResource");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasPrecision(6);
            entity.Property(e => e.IconPath).HasMaxLength(255);
            entity.Property(e => e.ResourceName).HasMaxLength(255);
            entity.Property(e => e.Status).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasPrecision(6);
        });

        modelBuilder.Entity<MstRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("MstRole");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasPrecision(6);
            entity.Property(e => e.DeletedDate).HasPrecision(6);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasPrecision(6);
        });

        modelBuilder.Entity<MstRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MstRoom_pkey");

            entity.ToTable("MstRoom");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Capacity).HasDefaultValue(1);
            entity.Property(e => e.Color).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasPrecision(6);
            entity.Property(e => e.DeletedDate).HasPrecision(6);
            entity.Property(e => e.Description).HasMaxLength(8000);
            entity.Property(e => e.Floor).HasDefaultValue(1);
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasPrecision(6);

            entity.HasOne(d => d.Location).WithMany(p => p.MstRooms)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_room_location");
        });

        modelBuilder.Entity<MstUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("MstUser");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasPrecision(6);
            entity.Property(e => e.DeletedDate).HasPrecision(6);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LoginName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(8000);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UpdatedDate).HasPrecision(6);

            entity.HasOne(d => d.Role).WithMany(p => p.MstUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_role");
        });

        modelBuilder.Entity<TrxCatering>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TrxCatering_pkey");

            entity.ToTable("TrxCatering");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.BookingRoomId).HasColumnName("BookingRoomID");
            entity.Property(e => e.Item).HasMaxLength(255);
            entity.Property(e => e.Notes).HasMaxLength(8000);
            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.BookingRoom).WithMany(p => p.TrxCateringBookingRooms)
                .HasForeignKey(d => d.BookingRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trxcat_trxhis");

            entity.HasOne(d => d.RequestedByNavigation).WithMany(p => p.TrxCaterings)
                .HasForeignKey(d => d.RequestedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trxcat_user");

            entity.HasOne(d => d.Room).WithMany(p => p.TrxCateringRooms)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trxcat_room");
        });

        modelBuilder.Entity<TrxHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TrxHistory_pkey");

            entity.ToTable("TrxHistory");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CanceledBy).HasMaxLength(100);
            entity.Property(e => e.CanceledDate).HasPrecision(6);
            entity.Property(e => e.IsAllDay).HasDefaultValue(false);
            entity.Property(e => e.Necessity).HasMaxLength(255);
            entity.Property(e => e.RequestedDate).HasPrecision(6);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.Routine).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.TimeFrom).HasColumnType("time(6) with time zone");
            entity.Property(e => e.TimeTo).HasColumnType("time(6) with time zone");

            entity.HasOne(d => d.RequestedByNavigation).WithMany(p => p.TrxHistories)
                .HasForeignKey(d => d.RequestedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_history_user");

            entity.HasOne(d => d.Room).WithMany(p => p.TrxHistories)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_history_room");
        });

        modelBuilder.Entity<TrxMstRoomChlResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_trx_mstRoom_chlRes");

            entity.ToTable("TrxMstRoomChlResource");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.ChlResId).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasPrecision(6);
            entity.Property(e => e.DeletedDate).HasPrecision(6);
            entity.Property(e => e.UpdatedDate).HasPrecision(6);

            entity.HasOne(d => d.ChlRes).WithMany(p => p.TrxMstRoomChlResources)
                .HasForeignKey(d => d.ChlResId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trx_chlRes");

            entity.HasOne(d => d.MstRoom).WithMany(p => p.TrxMstRoomChlResources)
                .HasForeignKey(d => d.MstRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trx_mstRoom");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
