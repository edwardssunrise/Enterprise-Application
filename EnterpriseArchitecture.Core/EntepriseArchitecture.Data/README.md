## Data Access Layer

"Data Layer" şu amaçlarla kullanılır:

1. Veritabanı bağlantısı sağlamak (Context)
2. Veritabanı üzerinde temel CRUD işlemlerini sağlamak (Generic Repository)
3. Veritabanı üzerinde temel CRUD ve sorgu işlemlerini sağlamak (UnitOfWork)

Bu katmandaki design pattern'ler generic çalışma sağlar.


### 1. Context
Context sınıfı DbContext sınıfından türetilmelidir. Presentation Layer'da yer
alan Web.config içinde tanımlı Connection String  bu kapsamda belirtilerek hangi 
veritabanına bağlantı sağlanacağı bildirilir. Veritabanındaki tablolar *Context 
sınıfında belirtilemelidir. Aynı zamanda DbContext sınıfının OnModelCreating() 
metodu override edilmelidir:

```csharp
using EnterpriseArchitecture.Core;

namespace EntepriseArchitecture.Data.Context
{
    public partial class EnterpriseArchitectureContext : DbContext
    {
        private readonly EnterpriseArchitectureContext _context;

        /// <summary>
        /// Presentation Layer'da yer alan Web.config içinde tanımlı Connection String 
        /// bu kapsamda belirtilerek hangi veritabanına bağlantı sağlanacağı bildirilir.
        /// </summary>
        public EnterpriseArchitectureContext() : base("name=EnterpriseArchitectureEntities")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>Veritabanındaki tablolar *Context sınıfında belirtilemelidir.</summary>
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User", "dbo");
            modelBuilder.Entity<Post>().ToTable("Post", "dbo");
            modelBuilder.Entity<Category>().ToTable("Category", "dbo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
```

### 2. Unit Of Work

Veritabanı üzerindeki temel CRUD işlemlerinin gerçekleştirilmesi için Unit Of Work design pattern'i kullanılır.
Unit Of Work design pattern'i veritabanı üzerinde toplu halde işlem yapılmasını ve olası bir hata durumunda 
işlemin geri alınabilmesini sağlar. Unit Of Work tasarım deseni, yazılım uygulamamızda veritabanıyla ilgili her 
bir aksiyonun anlık olarak veritabanına yansıtılmasını engelleyen ve buna nazaran tüm aksiyonları biriktirip bir
bütün olarak bir defada tek bir connection üzerinden gerçekleştirilmesini sağlayan ve böylece veritabanı maliyetlerini
oldukça minimize eden bir tasarım desenidir.

Transaction, içerisinde bir veya birden fazla sorguyu kontrol edebilme kapasitesine sahip mekanizmadır. Dolayısıyla 
her bir operatif sorgu için transaction devreye sokmaktansa tüm sorgularımızı kapsayacak bir adet transactionın
devreye girmesi maliyeti oldukça minimize edecektir. İşte bu çözümün teknik adı Unit Of Work’tür.

Unit Of Work, toplu veritabanı işlemlerini tek seferde bir kereye mahsus execute eden ve böylece bu toplu işlem
neticesinde kaç kayıtın etkilendiğini rapor olarak sunabilen bir tasarım desenidir.Genellikle Repository Design
Pattern ile birlikte kullanılması tercih edilen Unit Of Work, ayrıca (genellikle) transaction kontrolüyle beraber
kullanılarak tek bir merkezden tüm sorgu süreçlerini kontrol edebilmektedir. Tüm bunların yanında, kullanıcı
tarafından adım adım(zincirleme) yapılan operasyonlarda süreç tam teferruatlı sonlandırılmadan vazgeçildiği
taktirde o noktaya kadar yapılan tüm değişikliklerin geriye alınması gerekmektedir. İşte buradaki iş maliyetini
Unit Of Work ortadan kaldırmakta ve zincir sonlanmaksızın hiçbir entitynin değerini veritabanında fiziksel olarak
değiştirtmemektedir.

Tecrübeye dayalı eleştiriler:

1. Entity Framework kendi bünyesinde UnitOfWork design pattern'ini uygulandığından beraber kullanılmaması tavsiye edilir.
2. Entity Framework Core, Dependency Injection ile context nesnesini otomatik olarak "dispose" ettiğinden bu işlem
   tekrarlanmayabilir.

### 3. Generic Repository

Programlarımızda veritabanı işlemleri yaparken bilinçsizce yapılan kodlama, bizleri müthiş bir kod tekrarına götürecektir.
Özellikle bir uygulamada yapılan CRUD(Create-Read-Update-Delete) işlemleri, bu tekrarları hat safhaya çıkarmaktadır. 
İşte tam da bu noktada Repository Design Pattern derde derman olmaktadır.

Veritabanındaki tabloları temsil eden her Entity Class üzerinde uygulanacak her CRUD işlemi için ayrı DAL (Data Access Layer)
oluşturmamak için Generic Repository Design Pattern uygulamak gerekir. 