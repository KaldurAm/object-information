namespace ObjectInformation.DAL.Model
{
    using System.Data.Entity;

    public partial class OInformation : DbContext
    {
        //public OInformation()
        //    : base("name=OInformation")
        //{ }

        public OInformation()
            : base("name=OInformation")
        { }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<ObjectProperty> ObjectProperties { get; set; }
        public virtual DbSet<ObjectRealty> ObjectRealties { get; set; }
        public virtual DbSet<ObjectType> ObjectTypes { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Upload> Uploads { get; set; }
        public virtual DbSet<PropertyObjectType> PropertyObjectType { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Polygon> Polygon { get; set; }
        public virtual DbSet<Coordinate> Coordinate { get; set; }
        public virtual DbSet<Checklist> Checklists { get; set; }
        public virtual DbSet<Pledgers> Pledgers { get; set; }
        public virtual DbSet<ObjectRealtyPledgers> ObjectRealtyPledgers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
          
            modelBuilder.Entity<Country>()
                .HasMany(e => e.Regions)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Region>()
                .HasMany(e => e.Cities)
                .WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Districts)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.ObjectRealties)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ObjectType>()
                .HasMany(e => e.ObjectRealties)
                .WithRequired(e => e.ObjectType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ObjectRealty>()
                .HasMany(e => e.ObjectProperties)
                .WithRequired(e => e.ObjectRealty)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ObjectRealty>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.ObjectRealty)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ObjectRealty>()
                .HasMany(e => e.Uploads)
                .WithRequired(e => e.ObjectRealty)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Property>()
                .HasMany(e => e.ObjectProperties)
                .WithRequired(e => e.Property)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.Uploads)
                .WithRequired(e => e.DocumentType)
                .WillCascadeOnDelete(true);
            //Помогает , если вылазитт ошибка о том, что база была изменена
            Database.SetInitializer<OInformation>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}