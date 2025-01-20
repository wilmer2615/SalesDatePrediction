using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public partial class AplicationDbContext : DbContext
    {        
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<CustOrder> CustOrders { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<OrderTotalsByYear> OrderTotalsByYears { get; set; }

        public virtual DbSet<OrderValue> OrderValues { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Shipper> Shippers { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }
     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories", "Production");

                entity.HasIndex(e => e.CategoryName, "categoryname");

                entity.Property(e => e.CategoryId).HasColumnName("categoryid");
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(15)
                    .HasColumnName("categoryname");
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<CustOrder>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("CustOrders", "Sales");

                entity.Property(e => e.CustId).HasColumnName("custid");
                entity.Property(e => e.OrderMonth)
                    .HasColumnType("datetime")
                    .HasColumnName("ordermonth");
                entity.Property(e => e.Qty).HasColumnName("qty");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId);

                entity.ToTable("Customers", "Sales");

                entity.HasIndex(e => e.City, "idx_nc_city");

                entity.HasIndex(e => e.CompanyName, "idx_nc_companyname");

                entity.HasIndex(e => e.PostalCode, "idx_nc_postalcode");

                entity.HasIndex(e => e.Region, "idx_nc_region");

                entity.Property(e => e.CustId).HasColumnName("custid");
                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasColumnName("address");
                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .HasColumnName("city");
                entity.Property(e => e.CompanyName)
                    .HasMaxLength(40)
                    .HasColumnName("companyname");
                entity.Property(e => e.ContactName)
                    .HasMaxLength(30)
                    .HasColumnName("contactname");
                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(30)
                    .HasColumnName("contacttitle");
                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .HasColumnName("country");
                entity.Property(e => e.Fax)
                    .HasMaxLength(24)
                    .HasColumnName("fax");
                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .HasColumnName("phone");
                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("postalcode");
                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .HasColumnName("region");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Empid);

                entity.ToTable("Employees", "HR");

                entity.HasIndex(e => e.LastName, "idx_nc_lastname");

                entity.HasIndex(e => e.PostalCode, "idx_nc_postalcode");

                entity.Property(e => e.Empid).HasColumnName("empid");
                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasColumnName("address");
                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasColumnName("birthdate");
                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .HasColumnName("city");
                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .HasColumnName("country");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(10)
                    .HasColumnName("firstname");
                entity.Property(e => e.HireDate)
                    .HasColumnType("datetime")
                    .HasColumnName("hiredate");
                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .HasColumnName("lastname");
                entity.Property(e => e.MgrId).HasColumnName("mgrid");
                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .HasColumnName("phone");
                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("postalcode");
                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .HasColumnName("region");
                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .HasColumnName("title");
                entity.Property(e => e.TitleOfCourtesy)
                    .HasMaxLength(25)
                    .HasColumnName("titleofcourtesy");

                entity.HasOne(d => d.Mgr).WithMany(p => p.InverseMgr)
                    .HasForeignKey(d => d.MgrId)
                    .HasConstraintName("FK_Employees_Employees");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders", "Sales");

                entity.HasIndex(e => e.CustId, "idx_nc_custid");

                entity.HasIndex(e => e.EmpId, "idx_nc_empid");

                entity.HasIndex(e => e.OrderDate, "idx_nc_orderdate");

                entity.HasIndex(e => e.ShippedDate, "idx_nc_shippeddate");

                entity.HasIndex(e => e.ShipperId, "idx_nc_shipperid");

                entity.HasIndex(e => e.ShipPostalCode, "idx_nc_shippostalcode");

                entity.Property(e => e.OrderId).HasColumnName("orderid");
                entity.Property(e => e.CustId).HasColumnName("custid");
                entity.Property(e => e.EmpId).HasColumnName("empid");
                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasColumnName("freight");
                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderdate");
                entity.Property(e => e.RequiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("requireddate");
                entity.Property(e => e.ShipAddress)
                    .HasMaxLength(60)
                    .HasColumnName("shipaddress");
                entity.Property(e => e.ShipCity)
                    .HasMaxLength(15)
                    .HasColumnName("shipcity");
                entity.Property(e => e.ShipCountry)
                    .HasMaxLength(15)
                    .HasColumnName("shipcountry");
                entity.Property(e => e.ShipName)
                    .HasMaxLength(40)
                    .HasColumnName("shipname");
                entity.Property(e => e.ShippedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shippeddate");
                entity.Property(e => e.ShipperId).HasColumnName("shipperid");
                entity.Property(e => e.ShipPostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("shippostalcode");
                entity.Property(e => e.ShipRegion)
                    .HasMaxLength(15)
                    .HasColumnName("shipregion");

                entity.HasOne(d => d.Cust).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Emp).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.Shipper).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Shippers");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("OrderDetails", "Sales");

                entity.HasIndex(e => e.OrderId, "idx_nc_orderid");

                entity.HasIndex(e => e.ProductId, "idx_nc_productid");

                entity.Property(e => e.OrderId).HasColumnName("orderid");
                entity.Property(e => e.ProductId).HasColumnName("productid");
                entity.Property(e => e.Discount)
                    .HasColumnType("numeric(4, 3)")
                    .HasColumnName("discount");
                entity.Property(e => e.Qty)
                    .HasDefaultValue((short)1)
                    .HasColumnName("qty");
                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unitprice");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Products");
            });

            modelBuilder.Entity<OrderTotalsByYear>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("OrderTotalsByYear", "Sales");

                entity.Property(e => e.OrderYear).HasColumnName("orderyear");
                entity.Property(e => e.Qty).HasColumnName("qty");
            });

            modelBuilder.Entity<OrderValue>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("OrderValues", "Sales");

                entity.Property(e => e.CustId).HasColumnName("custid");
                entity.Property(e => e.EmpId).HasColumnName("empid");
                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderdate");
                entity.Property(e => e.OrderId).HasColumnName("orderid");
                entity.Property(e => e.ShipperId).HasColumnName("shipperid");
                entity.Property(e => e.Val)
                    .HasColumnType("numeric(12, 2)")
                    .HasColumnName("val");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products", "Production");

                entity.HasIndex(e => e.CategoryId, "idx_nc_categoryid");

                entity.HasIndex(e => e.ProductName, "idx_nc_productname");

                entity.HasIndex(e => e.SupplierId, "idx_nc_supplierid");

                entity.Property(e => e.ProductId).HasColumnName("productid");
                entity.Property(e => e.CategoryId).HasColumnName("categoryid");
                entity.Property(e => e.Discontinued).HasColumnName("discontinued");
                entity.Property(e => e.ProductName)
                    .HasMaxLength(40)
                    .HasColumnName("productname");
                entity.Property(e => e.SupplierId).HasColumnName("supplierid");
                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unitprice");

                entity.HasOne(d => d.Category).WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Suppliers");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("Shippers", "Sales");

                entity.Property(e => e.ShipperId).HasColumnName("shipperid");
                entity.Property(e => e.CompanyName)
                    .HasMaxLength(40)
                    .HasColumnName("companyname");
                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Suppliers", "Production");

                entity.HasIndex(e => e.CompanyName, "idx_nc_companyname");

                entity.HasIndex(e => e.PostalCode, "idx_nc_postalcode");

                entity.Property(e => e.SupplierId).HasColumnName("supplierid");
                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasColumnName("address");
                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .HasColumnName("city");
                entity.Property(e => e.CompanyName)
                    .HasMaxLength(40)
                    .HasColumnName("companyname");
                entity.Property(e => e.ContactName)
                    .HasMaxLength(30)
                    .HasColumnName("contactname");
                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(30)
                    .HasColumnName("contacttitle");
                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .HasColumnName("country");
                entity.Property(e => e.Fax)
                    .HasMaxLength(24)
                    .HasColumnName("fax");
                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .HasColumnName("phone");
                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("postalcode");
                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .HasColumnName("region");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
