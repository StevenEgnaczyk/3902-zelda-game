﻿using CrossPlatformDesktopProject.Equipables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CrossPlatformDesktopProject.Link
{
    class LinkUsingItemNorth : ILinkState
    {
        private Player player;
        private int frames_left;
<<<<<<< HEAD
=======
        private static List<Rectangle> my_sources = new List<Rectangle>
        {
            LinkTextureStorage.LINK_USE_ITEM_NORTH,
        };
        private int my_source_index;
>>>>>>> item-updates-states
        private int my_texture_index;

        public LinkUsingItemNorth(Player player)
        {
            this.player = player;
<<<<<<< HEAD
            frames_left = Player.use_item_frames;
=======
            frames_left = 10;
            my_source_index = 0;
>>>>>>> item-updates-states
            my_texture_index = 0;
        }

        void ILinkState.Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Texture2D texture = LinkTextureStorage.Instance.getTextures()[my_texture_index];
<<<<<<< HEAD
            Rectangle source = LinkTextureStorage.LINK_USE_ITEM_NORTH;
=======
            Rectangle source = my_sources[my_source_index];
>>>>>>> item-updates-states
            Rectangle destination = new Rectangle(
                (int)xPos, (int)yPos,
                source.Width * 3, source.Height * 3);
            spriteBatch.Draw(texture, destination, source, Color.White);
        }

        void ILinkState.Update()
        {
            if (--frames_left <= 0)
            {
                player.currentState = new LinkFacingNorthState(player);
            }
        }

        void ILinkState.setTextureIndex(int index)
        {
            my_texture_index = index;
        }
        void ILinkState.TakeDamage()
        {
<<<<<<< HEAD
            player.currentState = new LinkKnockedSouth(player);
        }

        public void MoveDown() { }
        public void MoveLeft() { }
        public void MoveRight() { }
        public void MoveUp() { }
        public void UsePrimary() { }
        public void UseSecondary1() { }
        public void UseSecondary2() { }
        public void UseSecondary3() { }
=======
            player.currentState = new LinkKnockedWest(player);
        }

        public void MoveDown()
        {
            player.currentState = new LinkFacingSouthState(player);
        }

        public void MoveLeft()
        {
            player.currentState = new LinkFacingWestState(player);
        }

        public void MoveRight()
        {
            player.currentState = new LinkFacingEastState(player);
        }

        public void MoveUp()
        {
            player.yPos -= Player.walking_speed;
            if (--frames_left <= 0)
            {
                frames_left = Player.frames_per_step;
                my_source_index++;
                my_source_index %= my_sources.Count;
            }
        }

        public void UsePrimary()
        {
            player.currentState = new LinkSword1East(player);
        }

        public void UseSecondary1()
        {
        }

        public void UseSecondary2()
        {
        }

        public void UseSecondary3()
        {
        }
>>>>>>> item-updates-states
    }
}