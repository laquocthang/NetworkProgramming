using System;
using System.Threading;

namespace ThreadExample
{
	public class ThreadClass
	{
		private const int RANDOM_SLEEP_MAX = 1000;
		private const int LOOP_COUNT = 10;
		private string message;

		public ThreadClass(string message)
		{
			this.message = message;
		}

		public void Run()
		{
			Random random = new Random();
			for (int i = 0; i < LOOP_COUNT; i++)
			{
				Console.WriteLine(message + " (Thread ID: " + Thread.CurrentThread.GetHashCode() + ")");
				try
				{
					Thread.Sleep(random.Next(0, RANDOM_SLEEP_MAX));
				}
				catch (Exception)
				{
				}
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			ThreadClass ex1 = new ThreadClass("The first thread is running...");
			Thread thread1 = new Thread(ex1.Run);
			thread1.Start();

			ThreadClass ex2 = new ThreadClass("The second thread is running...");
			Thread thread2 = new Thread(ex2.Run);
			thread2.Start();

			Console.ReadKey();
		}
	}
}
