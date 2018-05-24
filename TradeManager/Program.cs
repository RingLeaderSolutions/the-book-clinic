﻿using System;
using System.Threading.Tasks;
using Autofac;
using TradeManager.Services;

namespace TradeManager
{
    class Program
    {
        static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }


        static async Task MainAsync()
        {
            var container = Initialise();

            using (var scope = container.BeginLifetimeScope())
            {
                var tradeService = scope.Resolve<TradeService>();
                tradeService.ShowTrades();

                var tradeName = Console.ReadLine();

                while (tradeName != "q")
                {
                    if (!string.IsNullOrEmpty(tradeName))
                    {
                        await tradeService.AcceptTrade(tradeName);
                    }

                    tradeName = Console.ReadLine();
                }

                Environment.Exit(0);
            }
        }


        private static IContainer Initialise()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new TradeManagerModule());

            var container = builder.Build();
            Console.WriteLine("Trade Manager started.");
            return container;
        }
    }
}
