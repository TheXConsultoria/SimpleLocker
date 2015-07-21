using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace SL.TestConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/// Given any object instance
			var list = new List<int>();

			/// We create the simple locker instance
			var locker = new SimpleLocker();

			Parallel.For(0, 10000, i=>
				{
					/// Safelly modify it
					locker.WriteVoid(() => list.Add(i));

					/// Safelly ready it
					Console.WriteLine("CurrentCount {0} - ThreadId: {1}", locker.Read(() => list.Count), Thread.CurrentThread.ManagedThreadId);

				});

			Console.WriteLine ("Done!");
			Console.ReadLine ();
		}
	}
}
