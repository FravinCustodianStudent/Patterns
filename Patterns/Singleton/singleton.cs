﻿using MoreLinq.Extensions;
using NUnit.Framework;

namespace Patterns.Singleton;


  public class MyDatabase
  {
    private MyDatabase()
    {
      Console.WriteLine("Initializing database");
    }
    private static Lazy<MyDatabase> instance = 
      new Lazy<MyDatabase>(() => new MyDatabase());

    public static MyDatabase Instance => instance.Value;
  }
  
  public interface IDatabase
  {
    int GetPopulation(string name);
  }

  public class SingletonDatabase : IDatabase
  {
    private Dictionary<string, int> capitals;
    private static int instanceCount;
    public static int Count => instanceCount;

    private SingletonDatabase()
    {
      Console.WriteLine("Initializing database");

      capitals = File.ReadAllLines(
        Path.Combine(
          new FileInfo(typeof(IDatabase).Assembly.Location)
            .DirectoryName, 
          "capitals.txt")
        )
        .Batch(2)
        .ToDictionary(
          list => list.ElementAt(0).Trim(),
          list => int.Parse(list.ElementAt(1)));
    }

    public int GetPopulation(string name)
    {
      return capitals[name];
    }

    // laziness + thread safety
    private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() =>
    {
      instanceCount++;
      return new SingletonDatabase();
    });

    public static IDatabase Instance => instance.Value;
  }

  public class SingletonRecordFinder
  {
    public int TotalPopulation(IEnumerable<string> names)
    {
      int result = 0;
      foreach (var name in names)
        result += SingletonDatabase.Instance.GetPopulation(name);
      return result;
    }
  }

  public class ConfigurableRecordFinder
  {
    private IDatabase database;
  
    public ConfigurableRecordFinder(IDatabase database)
    {
      this.database = database;
    }
  
    public int GetTotalPopulation(IEnumerable<string> names)
    {
      int result = 0;
      foreach (var name in names)
        result += database.GetPopulation(name);
      return result;
    }
  }

  public class DummyDatabase : IDatabase
  {
    public int GetPopulation(string name)
    {
      return new Dictionary<string, int>
      {
        ["alpha"] = 1,
        ["beta"] = 2,
        ["gamma"] = 3
      }[name];
    }
  }

  /// <summary>
  /// IMPORTANT: be sure to turn off shadow copying for unit tests in R#!
  /// </summary>
  [TestFixture]
  public class SingletonTests
  {
    [Test]
    public void IsSingletonTest()
    {
      var db = SingletonDatabase.Instance;
      var db2 = SingletonDatabase.Instance;
      Assert.That(db, Is.SameAs(db2));
      Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
    }

    [Test]
    public void SingletonTotalPopulationTest()
    {
      // testing on a live database
      var rf = new SingletonRecordFinder();
      var names = new[] {"Seoul", "Mexico City"};
      int tp = rf.TotalPopulation(names);
      Assert.That(tp, Is.EqualTo(17500000 + 17400000));
    }

    [Test]
    public void DependentTotalPopulationTest()
    {
      var db = new DummyDatabase();
      var rf = new ConfigurableRecordFinder(db);
      Assert.That(
        rf.GetTotalPopulation(new[]{"alpha", "gamma"}),
        Is.EqualTo(4));
    }
  }