using System;
using System.Threading;

namespace ParallelResearch
{
    public class AutoResetEventApp
    {
        // ... существующие методы ...

        public void RunTimedTest()
        {
            Console.WriteLine("Запуск теста с временными метками...");
            
            var autoEvent = new AutoResetEvent(true);
            
            for (int i = 1; i <= 3; i++)
            {
                int num = i;
                var thread = new Thread(() => TimedWorker(autoEvent, $"Поток-{num}"));
                thread.Start();
                Thread.Sleep(100); // Небольшая задержка между запуском потоков
            }
            
            Thread.Sleep(8000); // Ждем завершения
        }

        private void TimedWorker(AutoResetEvent autoEvent, string name)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            Console.WriteLine($"{timestamp} | {name} начал работу и ждет...");
            
            autoEvent.WaitOne();
            
            timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            Console.WriteLine($"{timestamp} | {name} вошел в критическую секцию!");
            Thread.Sleep(2000);
            
            timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            Console.WriteLine($"{timestamp} | {name} вышел из критической секции");
            
            autoEvent.Set();
        }
    }
}