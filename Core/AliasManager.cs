
namespace RepoPilot.Core
{
    internal sealed class AliasManager
    {
        public static void HandleAliasCommand(ref Dictionary<string, string> aliases, string[] args)
        {
            if (args.Length == 1)
            {
                Console.WriteLine("Current aliases:");
                foreach (var alias in aliases)
                {
                    Console.WriteLine($"{alias.Key} = {alias.Value}");
                }
            }
            else if (args.Length == 2)
            {
                string alias = args[1];
                if (aliases.ContainsKey(alias))
                {
                    aliases.Remove(alias);
                    Console.WriteLine($"Alias '{alias}' removed.");
                }
                else
                {
                    Console.WriteLine($"Alias '{alias}' not found.");
                }
            }
            else if (args.Length == 3)
            {
                string alias = args[1];
                string command = args[2];
                aliases[alias] = command;
                Console.WriteLine($"Alias '{alias}' added for command '{command}'.");
            }
            else
            {
                Console.WriteLine("Usage: alias <name> <command>");
                Console.WriteLine("       alias <name> to remove");
                Console.WriteLine("       alias to list all aliases");
            }
        }
    }
}
