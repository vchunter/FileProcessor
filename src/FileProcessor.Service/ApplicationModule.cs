using Autofac;
using FileProcessor.Core;
using FileProcessor.Core.Files;
using System;

namespace FileProcessor.Service
{
    public class Application : IApplication
    {
        private readonly IProcessor _processor;

        public Application(IProcessor processor)
        {
            _processor = processor;
        }

        public void Run()
        {
            Console.WriteLine("Service Started...");

            _processor.ProcessAllFiles();

            Console.ReadLine();
        }
    }

    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<Processor>().As<IProcessor>();
            builder.RegisterType<FileParser>().As<IFileParser>();

            return builder.Build();
        }
    }
}
