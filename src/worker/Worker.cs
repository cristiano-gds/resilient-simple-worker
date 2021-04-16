using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace worker
{
    public class Worker : BackgroundService
    {
        private const string PATHFILE = "./files/simple-file.txt";
        private DateTime lastChangeInFile = DateTime.MinValue;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Polly policy: automatic retries if occurs specific error 
                var retryDirectory = Policy.Handle<DirectoryNotFoundException>()
                    .WaitAndRetry(
                        5, //number of retries
                        t => TimeSpan.FromSeconds(10), //interval between retries
                        onRetry: (exception, TimeSpan, retryCount, context) => {
                            Console.WriteLine("Attempt to open directory #{0}", retryCount);
                        }
                    );

                //Polly policy: automatic retries if occurs specific error 
                var retryFile = Policy.Handle<FileNotFoundException>()
                    .WaitAndRetry(
                        5, //number of retries
                        t => TimeSpan.FromSeconds(10), //interval between retries
                        onRetry: (exception, TimeSpan, retryCount, context) => {
                            Console.WriteLine("Attempt to read file #{0}", retryCount);
                        }
                    );

                //Polly policy: execute a alternative script block if occurs specific error
                var fallback = Policy.Handle<FileNotFoundException>()
                    .Fallback(() => {
                        Console.WriteLine("Attempt exceeded, process ignored");
                    });

                //Polly policy: combine policies
                var wrap = Policy.Wrap(fallback, retryFile, retryDirectory);

                wrap.Execute(() => {
                    
                    DateTime lastWriteTime;
                    
                    if((lastWriteTime = File.GetLastWriteTime(PATHFILE)) != lastChangeInFile)
                    {
                        Console.WriteLine("New content in file:{0}", File.ReadAllText(PATHFILE));
                        lastChangeInFile = lastWriteTime;
                    }
                });

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
