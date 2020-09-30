﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformDesktopProject.Commands
{
    class Quit : ICommand
    {
        private Game1 myGame;
        public Quit(Game1 game)
        {
            myGame = game;
        }
        public void Execute() => myGame.quit();
    }

    class SetLinkEastIdle : ICommand
    {
        private Player player1;
        public SetLinkEastIdle(Player player) => player1 = player;
        public void Execute() => player1.currentState = new LinkFacingEastState1(player1);
    }
}
