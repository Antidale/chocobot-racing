using chocobot_racing.Extensions;
using DSharpPlus.Commands;
using DSharpPlus.Commands.EventArgs;

namespace chocobot_racing.EventHandlers
{
    public class CommandsEventHanlders
    {
        public static async Task OnCommandInvokedAsync(CommandsExtension _, CommandExecutedEventArgs eventArgs)
        {
            await eventArgs.Context.LogUsageAsync();
        }

        public static async Task OnCommandErroredAsync(CommandsExtension _, CommandErroredEventArgs eventArgs)
        {
            await eventArgs.Context.LogErrorAsync("Whoops. Not fully implemented yet", eventArgs.Exception);
        }
    }
}
