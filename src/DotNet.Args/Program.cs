// Copyright 2020 C. Augusto Proiete & Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace DotNet.Args
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var appVersion = typeof(Program).Assembly.GetCustomAttributes(true)
                    .OfType<AssemblyInformationalVersionAttribute>().Single().InformationalVersion;

                var appTitle = $"dotnet-args CLI {appVersion}";
                Console.WriteLine(appTitle, ConsoleColor.White);

                Print(args);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ConsoleColor.Red);
                Console.WriteLine();
                Console.WriteLine(ex.ToString());
                return 1;
            }
        }

        private static void Print(string[] args)
        {
            if (args is null || args.Length == 0)
            {
                Console.WriteLine("Usage: dotnet args <Argument1> [Argument2] [Argument3] [...]");
                Console.WriteLine("Example: dotnet args --file project.zip --message \"My project!\"", ConsoleColor.DarkGray);
                Console.WriteLine();
                Console.WriteLine("No command-line arguments received.", ConsoleColor.Red);
                return;
            }

            Console.Write("Command-line arguments received (");
            Console.Write(args.Length.ToString(CultureInfo.InvariantCulture), ConsoleColor.Magenta);
            Console.Write("):");
            Console.WriteLine();

            var argIndex = 0;
            while (argIndex < args.Length)
            {
                Console.Write("- args[");
                Console.Write(argIndex.ToString(CultureInfo.InvariantCulture), ConsoleColor.Yellow);
                Console.Write("]: ");
                Console.Write(FormattableString.Invariant($"{args[argIndex]}"), ConsoleColor.Cyan);
                Console.WriteLine();
                argIndex++;
            }

            var firstArg = Environment.GetCommandLineArgs()[0];
            var commandLine = Environment.CommandLine;

            if (!string.IsNullOrWhiteSpace(firstArg) && firstArg.EndsWith("dotnet-args.dll", StringComparison.OrdinalIgnoreCase))
            {
                commandLine = commandLine.Remove(0, firstArg.Length).TrimStart();
            }

            Console.WriteLine();
            Console.WriteLine("Complete command-line:");
            Console.WriteLine(commandLine);
        }
    }
}
