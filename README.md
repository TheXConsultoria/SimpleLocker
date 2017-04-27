# SimpleLocker
A simple concurrency controller for .Net

```csharp
/// This is your thread-"unsafe" object
var list = new List<int>();

/// We use the simple locker instance
var locker = new SimpleLocker();

Parallel.For(0, 10000, i=>
  {
    /// Safelly modify it
    locker.WriteVoid(() => list.Add(i));

    /// Safelly ready it
    Console.WriteLine("CurrentCount {0} - ThreadId: {1}", locker.Read(() => list.Count), 
      Thread.CurrentThread.ManagedThreadId);

  });

Console.WriteLine ("Done!");
Console.ReadLine ();
 ```
