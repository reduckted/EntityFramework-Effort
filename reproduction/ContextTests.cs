#nullable disable

using System.Data.Common;
using System.Data.Entity;
using Xunit;

namespace reproduction;

public class ContextTests1 {

    [Fact]
    public async Task Test() {
        using var cx = Effort.DbConnectionFactory.CreateTransient();

        using var context = new TestDatabase(cx, false);

        await context.As.ToListAsync();

        await context.As.Select((x) => new { x.Code, x.Name }).ToListAsync();

        await context.Bs.Select((x) => new { x.Name, x.Size }).ToListAsync();

        await (
            from a in context.As
            join b in context.Bs on a.ID equals b.ID
            join c in context.Cs on a.ID equals c.ID
            select new { a.ID, b.Name, c.Code }
        ).ToListAsync();

    }

}


public class ContextTests2 {

    [Fact]
    public async Task Test() {
        using var cx = Effort.DbConnectionFactory.CreateTransient();

        using var context = new TestDatabase(cx, false);

        await (
            from a in context.As
            join b in context.Bs on a.ID equals b.ID into g
            from item in g
            select new { a.ID, item.Name, item.Code }
        ).ToListAsync();

    }

}

public class ContextTests3 {

    [Fact]
    public async Task Test() {
        using var cx = Effort.DbConnectionFactory.CreateTransient();

        using var context = new TestDatabase(cx, false);

        await (
            from a in context.As
            join c in context.Bs on a.ID equals c.ID into g
            from item in g
            select new { a.ID, item.Name, item.Size }
        ).ToListAsync();
    }


    [Fact]
    public async Task Test2() {
        using var cx = Effort.DbConnectionFactory.CreateTransient();

        using var context = new TestDatabase(cx, false);

        await (
            from a in context.As
            from b in context.Bs
            from c in context.Cs
            from d in context.Ds
            select new { a.ID, b.Name, c.Size, d.Code }
        ).ToListAsync();
    }

}



public class TestDatabase : DbContext {

    public TestDatabase(DbConnection connection, bool contextOwnsConnection) : base(connection, contextOwnsConnection) { }


    public virtual DbSet<A> As { get; set; }
    public virtual DbSet<B> Bs { get; set; }
    public virtual DbSet<C> Cs { get; set; }
    public virtual DbSet<D> Ds { get; set; }
    public virtual DbSet<E> Es { get; set; }
    public virtual DbSet<F> Fs { get; set; }
    public virtual DbSet<G> Gs { get; set; }
    public virtual DbSet<H> Hs { get; set; }

}


public class A {
    public virtual int ID { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual int Size { get; set; }
}

public class B {
    public virtual int ID { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual int Size { get; set; }
}

public class C {
    public virtual int ID { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual int Size { get; set; }
}

public class D {
    public virtual int ID { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual int Size { get; set; }
}

public class E {
    public virtual int ID { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual int Size { get; set; }
}

public class F {
    public virtual int ID { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual int Size { get; set; }
}

public class G {
    public virtual int ID { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual int Size { get; set; }
}

public class H {
    public virtual int ID { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual int Size { get; set; }
}