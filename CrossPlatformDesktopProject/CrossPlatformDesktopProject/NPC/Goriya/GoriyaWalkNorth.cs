﻿using CrossPlatformDesktopProject.NPC;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CrossPlatformDesktopProject.NPC
{
    class GoriyaWalkNorth : IGoriyaState
    {
        private int my_frame_index;
        private int delay_frame_index;
        private Goriya goriya;

        private static int delay_frames = 6;
        private static List<Rectangle> my_source_frames = new List<Rectangle>{
            NpcTextureStorage.GORIYA_UP_1,
            NpcTextureStorage.GORIYA_UP_2
        };

        public GoriyaWalkNorth(Goriya goriya)
        {
            this.goriya = goriya;
            my_frame_index = 0;
            delay_frame_index = 0;
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Texture2D texture = NpcTextureStorage.Instance.getGoriyaUpDownSpriteSheet();
            Rectangle source = my_source_frames[my_frame_index];
            Rectangle destination = new Rectangle(
                (int)xPos, (int)yPos,
                source.Width * 3, source.Height * 3);
            spriteBatch.Draw(texture, destination, source, Color.White);
        }

        public void Update()
        {
            if (goriya.xPos == 400 && goriya.yPos == 100)
            {
                goriya.currentState = new GoriyaWalkEast(goriya);
            }

            if (++delay_frame_index >= delay_frames)
            {
                delay_frame_index = 0;
                goriya.yPos -= 5;
                my_frame_index++;
                my_frame_index %= my_source_frames.Count;
            }
        }
    }
}