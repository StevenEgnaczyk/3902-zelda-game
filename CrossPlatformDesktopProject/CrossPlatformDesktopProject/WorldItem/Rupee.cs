﻿using CrossPlatformDesktopProject.CollisionHandler;
using CrossPlatformDesktopProject.WorldItem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CrossPlatformDesktopProject.WorldItem
{
    class Rupee : IWorldItem
    {
        public float xPos, yPos;
        private Rectangle hitbox;
        public Rupee()
        {
            xPos = 300;
            yPos = 300;
            hitbox = new Rectangle((int)xPos, (int)yPos, 20, 20);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = ItemTextureStorage.Instance.getItemSpriteSheet();
            Rectangle source = ItemTextureStorage.RUPEE;
            spriteBatch.Draw(texture, getRectangle(), source, Color.White);
        }
        private Rectangle getRectangle()
        {
            return new Rectangle((int)xPos, (int)yPos,
                ItemTextureStorage.RUPEE.Width * 3,
                ItemTextureStorage.RUPEE.Height * 3);
        }

        Rectangle ICollider.GetRectangle()
        {
            return getRectangle();
        }

        public Rectangle GetRectangle()
        {
            return hitbox;
        }
    }
}