using Hotel_Listings.Data;
using Hotel_Listings.IRepository;

namespace Hotel_Listings.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly DatabaseContext databaseContext;
        private IGenericRepository<Country> countries;
        private IGenericRepository<Hotel> hotel;
        public UnitOfWork(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public IGenericRepository<Country> Countries => countries ??= new GenericRepository<Country>(databaseContext);


        public IGenericRepository<Hotel> Hotels => hotel ??= new GenericRepository<Hotel>(databaseContext);

        public void Dispose()
        {
            databaseContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await databaseContext.SaveChangesAsync();
        }
    }
}
