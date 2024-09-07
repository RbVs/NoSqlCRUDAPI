using LiteDB;

namespace NoSqlCRUDAPI.Database;

public class Repository<T> where T : class
{
    private readonly ILiteCollection<T> _collection;
    private readonly LiteDbContext _dbContext;

    public Repository(LiteDbContext dbContext)
    {
        _dbContext = dbContext;
        _collection = _dbContext.Database.GetCollection<T>(typeof(T).Name);
    }

    public IEnumerable<T> GetAll()
    {
        return _collection.FindAll();
    }

    public T GetById(Guid id)
    {
        return _collection.FindById(id);
    }

    public void Insert(T entity)
    {
        _collection.Insert(entity);
    }

    public bool Update(T entity)
    {
        return _collection.Update(entity);
    }

    public bool Delete(Guid id)
    {
        return _collection.Delete(id);
    }
}