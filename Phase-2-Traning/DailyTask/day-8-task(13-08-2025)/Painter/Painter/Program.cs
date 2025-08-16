using Microsoft.Extensions.DependencyInjection;
using System;

namespace Painter
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<ITool, PaintBrush>()
                .AddTransient<PainterWorker>()
                .BuildServiceProvider();

            var painter = serviceProvider.GetService<PainterWorker>();
            Console.WriteLine(painter.Paint());
        }
    }
}


