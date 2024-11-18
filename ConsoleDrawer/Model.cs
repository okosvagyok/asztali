using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class DrawingContext : DbContext
{
    public DbSet<Draw> Blogs { get; set; }

    public string DbPath { get; }

    public DrawingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "drawing.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Draw
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string File { get; set; }
}