using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AstroHelper3
{
    public class FileWriteService
    {
        private static FileWriteService _instance;
        private static readonly object _lock = new object();
        private static ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private static string _outputFilePath;

       
        private FileWriteService() {}

        
        public static FileWriteService GetInstance(string outputFilePath)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new FileWriteService();
                        _outputFilePath = outputFilePath;
                        Task.Run(() => ProcessQueueAsync());
                    }
                }
            }
            return _instance;
        }

        public void EnqueueMessage(string message)
        {
            _queue.Enqueue(message);
        }

        private static async Task ProcessQueueAsync()
        {
            while (true)
            {
                await _semaphore.WaitAsync();

                while (_queue.TryDequeue(out string message))
                {
                    using (var writer = new StreamWriter(_outputFilePath, true))
                    {
                        await writer.WriteLineAsync(message);
                    }
                }

                _semaphore.Release();
                await Task.Delay(5000); 
            }
        }
    }
}