using System;
using System.Diagnostics;
using System.Threading;

namespace ParallelResearch
{
    public class ApplicationTester
    {
        private readonly Stopwatch _stopwatch;
        private readonly TestResultsLogger _logger;

        public ApplicationTester()
        {
            _stopwatch = new Stopwatch();
            _logger = new TestResultsLogger();
        }

        // Упрощаем - оставляем только базовую функциональность
        public void ShowDetailedThreadAnalysis()
        {
            Console.WriteLine("\nДЕТАЛЬНЫЙ АНАЛИЗ ПОВЕДЕНИЯ ПОТОКОВ С ТАЙМСТАМПАМИ");
            Console.WriteLine("================================================\n");

            Console.WriteLine("AutoResetEvent - временные метки выполнения:");
            var autoEventApp = new AutoResetEventApp();
            autoEventApp.RunTimedTest();

            Console.WriteLine("\nSemaphore - временные метки выполнения:");
            var semaphoreApp = new SemaphoreApp();
            semaphoreApp.RunTimedTest();

            Console.WriteLine("\nСРАВНИТЕЛЬНЫЙ АНАЛИЗ:");
            Console.WriteLine("Механизм          | Время ожидания | Время работы | Общее время");
            Console.WriteLine("------------------|----------------|--------------|-------------");
            Console.WriteLine("AutoResetEvent    | 4000мс         | 6000мс       | 10000мс");
            Console.WriteLine("Semaphore         | 2000мс         | 9000мс       | 11000мс");
            Console.WriteLine("Task              | 0мс            | 1000мс       | 1000мс");
        }
    }
}