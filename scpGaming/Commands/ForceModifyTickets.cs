using CommandSystem;
using Exiled.API.Features;
using PlayerRoles.RoleAssign;
using Exiled.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Permissions.Extensions;

namespace scpGaming.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class ForceModifyTickets : ICommand
    {
        public string Command { get; } = "epic_tomfoolery";
        public string[] Aliases { get; } = null;
        public string Description { get; } = "does some epic tomfoolery";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            try
            {
                if (!sender.CheckPermission("tom.foolery"))
                {
                    response = "missing perm <b>tom.foolery</b>.";
                    return false;
                }

                if (arguments.Count != 2)
                {
                    response = "need 2 args\n" +
                        "format: epic_tomfoolery [ID] [Ticket count]";
                    return false;
                }

                Player player = Exiled.API.Features.Player.List.FirstOrDefault(pl => pl.Id == int.Parse(arguments.At(0)));
                if (player is null)
                {
                    response = "player not found";
                    return false;
                }

                int amount = int.Parse(arguments.At(1));

                using (ScpTicketsLoader scpTicketsLoader = new ScpTicketsLoader())
                {
                    scpTicketsLoader.ModifyTickets(player.ReferenceHub, amount);
                }

                response = "trolling complete.";
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }
    }
}
