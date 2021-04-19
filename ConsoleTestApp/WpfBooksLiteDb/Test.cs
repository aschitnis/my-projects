using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBooksLiteDb.Database.Entities;

namespace WpfBooksLiteDb
{
    public class Test : ILiteCollection<BookEntity>
    {
        public string Name => throw new NotImplementedException();

        public BsonAutoId AutoId => throw new NotImplementedException();

        public EntityMapper EntityMapper => throw new NotImplementedException();

        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Count(BsonExpression predicate)
        {
            throw new NotImplementedException();
        }

        public int Count(string predicate, BsonDocument parameters)
        {
            throw new NotImplementedException();
        }

        public int Count(string predicate, params BsonValue[] args)
        {
            throw new NotImplementedException();
        }

        public int Count(System.Linq.Expressions.Expression<Func<BookEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count(Query query)
        {
            throw new NotImplementedException();
        }

        public bool Delete(BsonValue id)
        {
            throw new NotImplementedException();
        }

        public int DeleteAll()
        {
            throw new NotImplementedException();
        }

        public int DeleteMany(BsonExpression predicate)
        {
            throw new NotImplementedException();
        }

        public int DeleteMany(string predicate, BsonDocument parameters)
        {
            throw new NotImplementedException();
        }

        public int DeleteMany(string predicate, params BsonValue[] args)
        {
            throw new NotImplementedException();
        }

        public int DeleteMany(System.Linq.Expressions.Expression<Func<BookEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool DropIndex(string name)
        {
            throw new NotImplementedException();
        }

        public bool EnsureIndex(string name, BsonExpression expression, bool unique = false)
        {
            throw new NotImplementedException();
        }

        public bool EnsureIndex(BsonExpression expression, bool unique = false)
        {
            throw new NotImplementedException();
        }

        public bool EnsureIndex<K>(System.Linq.Expressions.Expression<Func<BookEntity, K>> keySelector, bool unique = false)
        {
            throw new NotImplementedException();
        }

        public bool EnsureIndex<K>(string name, System.Linq.Expressions.Expression<Func<BookEntity, K>> keySelector, bool unique = false)
        {
            throw new NotImplementedException();
        }

        public bool Exists(BsonExpression predicate)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string predicate, BsonDocument parameters)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string predicate, params BsonValue[] args)
        {
            throw new NotImplementedException();
        }

        public bool Exists(System.Linq.Expressions.Expression<Func<BookEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Query query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookEntity> Find(BsonExpression predicate, int skip = 0, int limit = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookEntity> Find(Query query, int skip = 0, int limit = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookEntity> Find(System.Linq.Expressions.Expression<Func<BookEntity, bool>> predicate, int skip = 0, int limit = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookEntity> FindAll()
        {
            throw new NotImplementedException();
        }

        public BookEntity FindById(BsonValue id)
        {
            throw new NotImplementedException();
        }

        public BookEntity FindOne(BsonExpression predicate)
        {
            throw new NotImplementedException();
        }

        public BookEntity FindOne(string predicate, BsonDocument parameters)
        {
            throw new NotImplementedException();
        }

        public BookEntity FindOne(BsonExpression predicate, params BsonValue[] args)
        {
            throw new NotImplementedException();
        }

        public BookEntity FindOne(System.Linq.Expressions.Expression<Func<BookEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public BookEntity FindOne(Query query)
        {
            throw new NotImplementedException();
        }

        public ILiteCollection<BookEntity> Include<K>(System.Linq.Expressions.Expression<Func<BookEntity, K>> keySelector)
        {
            throw new NotImplementedException();
        }

        public ILiteCollection<BookEntity> Include(BsonExpression keySelector)
        {
            throw new NotImplementedException();
        }

        public BsonValue Insert(BookEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(BsonValue id, BookEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Insert(IEnumerable<BookEntity> entities)
        {
            throw new NotImplementedException();
        }

        public int InsertBulk(IEnumerable<BookEntity> entities, int batchSize = 5000)
        {
            throw new NotImplementedException();
        }

        public long LongCount()
        {
            throw new NotImplementedException();
        }

        public long LongCount(BsonExpression predicate)
        {
            throw new NotImplementedException();
        }

        public long LongCount(string predicate, BsonDocument parameters)
        {
            throw new NotImplementedException();
        }

        public long LongCount(string predicate, params BsonValue[] args)
        {
            throw new NotImplementedException();
        }

        public long LongCount(System.Linq.Expressions.Expression<Func<BookEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public long LongCount(Query query)
        {
            throw new NotImplementedException();
        }

        public BsonValue Max(BsonExpression keySelector)
        {
            throw new NotImplementedException();
        }

        public BsonValue Max()
        {
            throw new NotImplementedException();
        }

        public K Max<K>(System.Linq.Expressions.Expression<Func<BookEntity, K>> keySelector)
        {
            throw new NotImplementedException();
        }

        public BsonValue Min(BsonExpression keySelector)
        {
            throw new NotImplementedException();
        }

        public BsonValue Min()
        {
            throw new NotImplementedException();
        }

        public K Min<K>(System.Linq.Expressions.Expression<Func<BookEntity, K>> keySelector)
        {
            throw new NotImplementedException();
        }

        public ILiteQueryable<BookEntity> Query()
        {
            throw new NotImplementedException();
        }

        public bool Update(BookEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(BsonValue id, BookEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Update(IEnumerable<BookEntity> entities)
        {
            throw new NotImplementedException();
        }

        public int UpdateMany(BsonExpression transform, BsonExpression predicate)
        {
            throw new NotImplementedException();
        }

        public int UpdateMany(System.Linq.Expressions.Expression<Func<BookEntity, BookEntity>> extend, System.Linq.Expressions.Expression<Func<BookEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Upsert(BookEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Upsert(IEnumerable<BookEntity> entities)
        {
            throw new NotImplementedException();
        }

        public bool Upsert(BsonValue id, BookEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
