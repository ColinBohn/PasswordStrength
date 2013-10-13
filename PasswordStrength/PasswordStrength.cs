using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace PasswordStrength
{
    [ApiVersion(1,14)]
    public class PasswordStrength : TerrariaPlugin
    {
        public override string Name
        {
            get
            {
                return "PasswordStrength";
            }
        }
        public override string Author
        {
            get
            {
                return "Colin";
            }
        }
        public override string Description
        {
            get
            {
                return "Require that players use strong passwords.";
            }
        }
        public override Version Version
        {
            get
            {
                return new Version("1.0");
            }
        }
        public PasswordStrength(Main game)
            : base(game)
        {
            Order = 50;
        }
        public override void Initialize()
        {
            TShockAPI.Hooks.PlayerHooks.PlayerCommand += OnCommand;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TShockAPI.Hooks.PlayerHooks.PlayerCommand -= OnCommand;
            }
            base.Dispose(disposing);
        }
        void OnCommand(PlayerCommandEventArgs args)
        {
            if (args.CommandName == "register")
            {
                if (args.Parameters[0].ToUpper() == args.Player.Name.ToUpper())
                {
                    args.Player.SendErrorMessage("ERROR: Your password cannot be the same as your name.");
                    args.Handled = true;
                    return;
                }
                if (args.Parameters[0].Length < 6)
                {
                    args.Player.SendErrorMessage("ERROR: Your password is too short.");
                    args.Handled = true;
                    return;
                }
                if (args.Parameters[0].Contains("password"))
                {
                    args.Player.SendErrorMessage("ERROR: Your password cannot contain 'password'.");
                    args.Handled = true;
                    return;
                }
                Match match = Regex.Match(args.Parameters[0], @"^(?=.*\d)(?=.*[a-zA-Z]).*$");
                if (!match.Success)
                {
                    args.Player.SendErrorMessage("ERROR: Your password requires both letters and numbers.");
                    args.Handled = true;
                    return;
                }
            }
            if (args.CommandName == "password")
            {
                if (args.Parameters[1].ToUpper() == args.Player.Name.ToUpper())
                {
                    args.Player.SendErrorMessage("ERROR: Your password cannot be the same as your name.");
                    args.Handled = true;
                    return;
                }
                if (args.Parameters[1].Length < 6)
                {
                    args.Player.SendErrorMessage("ERROR: Your password is too short.");
                    args.Handled = true;
                    return;
                }
                if (args.Parameters[1].Contains("password"))
                {
                    args.Player.SendErrorMessage("ERROR: Your password cannot contain 'password'.");
                    args.Handled = true;
                    return;
                }
                Match match = Regex.Match(args.Parameters[1], @"^(?=.*\d)(?=.*[a-zA-Z]).*$");
                if (!match.Success)
                {
                    args.Player.SendErrorMessage("ERROR: Your password requires both letters and numbers.");
                    args.Handled = true;
                    return;
                }
            }
        }
    }
}
