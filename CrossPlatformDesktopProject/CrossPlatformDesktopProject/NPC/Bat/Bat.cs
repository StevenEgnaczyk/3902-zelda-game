﻿using Microsoft.Xna.Framework.Graphics;

namespace CrossPlatformDesktopProject.NPC
{
    class Bat : INpc
    {
        public INpcState currentState;
        public float xPos, yPos;

        public Bat()
        {
            currentState = new BatWalkEast(this);
            xPos = 400;
            yPos = 100;
        }

        public void Update()
        {
            currentState.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentState.Draw(spriteBatch, xPos, yPos);
        }
    }

}