using System;
using System.Threading;

namespace ParallelResearch
{
    public class MutexApp
    {
        private Mutex? _mutex;

        public void RunTest(int threadCount, int iterations, int delayTime)
        {
            _mutex = new Mutex();

            Thread[] threads = new Thread[threadCount];
            
            for (int i = 0; i < threadCount; i++)
            {
                threads[i] = new Thread(() => ThreadProc(iterations, delayTime));
                threads[i].Name = $"Thread-{i}";
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Все потоки завершили работу с мьютексом");
        }

        private void ThreadProc(int iterations, int delayTime)
        {
            for (int i = 0; i < iterations; i++)
            {
                UseResource(delayTime);
            }
        }

        private void UseResource(int delayTime)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} запрашивает мьютекс");

            if (_mutex!.WaitOne(1000))
            {
                try
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} вошел в защищенную область");
                    Thread.Sleep(delayTime);
                    Console.WriteLine($"{Thread.CurrentThread.Name} покидает защищенную область");
                }
                finally
                {
                    _mutex.ReleaseMutex();
                    Console.WriteLine($"{Thread.CurrentThread.Name} освободил мьютекс");
                }
            }
            else
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} не смог получить мьютекс");
            }
        }

        public void RunTimedTest()
        {
            Console.WriteLine("Запуск теста Mutex с временными метками...");
            
            var mutex = new Mutex();
            
            for (int i = 1; i <= 3; i++)
            {
                int num = i;
                var thread = new Thread(() => TimedWorker(mutex, $"Mutex-{num}"));
                thread.Start();
                Thread.Sleep(150);
            }
            
            Thread.Sleep(10000);
        }

        private void TimedWorker(Mutex mutex, string name)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            Console.WriteLine($"{timestamp} | {name} запрашивает мьютекс");
            
            if (mutex.WaitOne(3000))
            {
                try
                {
                    timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
                    Console.WriteLine($"{timestamp} | {name} получил мьютекс");
                    Thread.Sleep(2000);
                    timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
                    Console.WriteLine($"{timestamp} | {name} освобождает мьютекс");
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
            {
                timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
                Console.WriteLine($"{timestamp} | {name} не дождался мьютекса");
            }
        }
    }
}