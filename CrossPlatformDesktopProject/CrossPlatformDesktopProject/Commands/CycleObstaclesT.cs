﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformDesktopProject.Commands
{
    class CycleObstaclesT : ICommand
    {
        private Game1 myGame;
        private Sprint2ListStorage sL;
        public CycleObstaclesT(Game1 game, Sprint2ListStorage sL)
        {
            myGame = game;
            this.sL = sL;
        }
        public void Execute() => myGame.obstacle = Sprint2ListStorage.obstacles[sL.lastObstacleIndex()];
    }
}
