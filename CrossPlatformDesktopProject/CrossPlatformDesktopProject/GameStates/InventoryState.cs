﻿using CrossPlatformDesktopProject.Equipables;
using CrossPlatformDesktopProject.Link;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CrossPlatformDesktopProject.GameStates
{
    public class InventoryState : IGameState
    {
        Player player;
        Game1 game;
        public InventoryState(Game1 game)
        {
            this.game = game;
            this.player = game.player;
        }

        static List<List<int>> room_map = new List<List<int>>
        {
            // bits are presence of top, bottom, left, right door
            new List<int> { 0b0000, 0b0000, 0b0000, 0b0000,   0b0000, 0b0000, 0b0000, 0b0000},
            new List<int> { 0b0000, 0b0000, 0b0000, 0b0000,   0b0000, 0b0000, 0b0000, 0b0000},
            new List<int> { 0b0000, 0b0000, 0b0001, 0b0110,   0b0000, 0b0000, 0b0000, 0b0000},
            new List<int> { 0b0000, 0b0000, 0b0000, 0b1100,   0b0000, 0b0101, 0b0010, 0b0000},
            new List<int> { 0b0000, 0b0001, 0b0111, 0b1011,   0b0011, 0b1010, 0b0000, 0b0000},
            new List<int> { 0b0000, 0b0000, 0b1001, 0b0111,   0b0010, 0b0000, 0b0000, 0b0000},
            new List<int> { 0b0000, 0b0000, 0b0000, 0b1100,   0b0000, 0b0000, 0b0000, 0b0000},
            new List<int> { 0b0000, 0b0000, 0b0001, 0b1011,   0b0010, 0b0000, 0b0000, 0b0000},
        };

        static Dictionary<string, int[]> key_to_pos = new Dictionary<string, int[]>{
            ["001"] = new int[] { 7, 3 },
            ["002"] = new int[] { 7, 4 },
            ["003"] = new int[] { 7, 2 },
            ["004"] = new int[] { 6, 3 },
            ["005"] = new int[] { 5, 3 },
            ["006"] = new int[] { 5, 4 },
            ["007"] = new int[] { 4, 4 },
            ["008"] = new int[] { 4, 5 },
            ["009"] = new int[] { 3, 5 },
            ["010"] = new int[] { 3, 6 },
            ["011"] = new int[] { 5, 2 },
            ["012"] = new int[] { 4, 2 },
            ["013"] = new int[] { 4, 1 },
            ["014"] = new int[] { 4, 3 },
            ["015"] = new int[] { 3, 3 },
            ["016"] = new int[] { 2, 3 },
            ["017"] = new int[] { 2, 2 },
        };

        public void Draw(SpriteBatch sb)
        {
            Texture2D texture = InventoryTextureStorage.Instance.texture;

            // first draw the big blocks.            
            Rectangle[] sources = {
                InventoryTextureStorage.TOP_THIRD,
                InventoryTextureStorage.MIDDLE_THIRD,
                InventoryTextureStorage.BOTTOM_THIRD,
            };
            int top_y = -sources[2].Height;
            Rectangle[] destinations = {
                new Rectangle(
                    0,
                    top_y,
                    sources[0].Width,
                    sources[0].Height
                ),
                new Rectangle(
                    0,
                    top_y + sources[0].Height,
                    sources[1].Width,
                    sources[1].Height
                ),
                new Rectangle(
                    0,
                    top_y + sources[0].Height + sources[1].Height,
                    sources[2].Width,
                    sources[2].Height
                ),
            };

            for (int i=0; i<sources.Length; i++)
            {
                sb.Draw(texture, destinations[i], sources[i], Color.White);
            }

            int squaresize = InventoryTextureStorage.getDoorSetRectangle(room_map[0][0]).Width;
            const int base_x = 128;
            const int base_y = 40;

            // now draw the map
            {
                sb.Draw(
                    texture,
                    new Rectangle(
                        base_x - 1,
                        base_y - 1,
                        8 * squaresize + 2,
                        8 * squaresize + 2
                    ),
                    InventoryTextureStorage.getDoorSetRectangle(0),
                    Color.White
                );
                for (int i=0; i<room_map.Count; i++)
                {
                    var row = room_map[i];
                    for (int j=0; j<row.Count; j++)
                    {
                        Rectangle source = InventoryTextureStorage.getDoorSetRectangle(row[j]);
                        Rectangle destination = new Rectangle(
                            base_x + source.Width * j,
                            base_y + source.Height * i,
                            source.Width, source.Height
                        );
                        sb.Draw(texture, destination, source, Color.White);
                    }
                }
            }

            // now draw the point on the map
            {
                string roomID = game.currentGamePlayState.CurrentRoom.roomID;
                var pair = key_to_pos[roomID];
                int i = pair[0], j = pair[1];
                Rectangle source = InventoryTextureStorage.YOU_ARE_HERE;
                Rectangle destination = new Rectangle(
                    base_x + squaresize * j + squaresize / 2 - source.Width / 2,
                    base_y + squaresize * i + squaresize / 2 - source.Height / 2,
                    source.Width, source.Height
                );
                sb.Draw(texture, destination, source, Color.White);
            }

            // if (player has map)
            {
                Rectangle source = InventoryTextureStorage.MAP;
                Rectangle destination = new Rectangle(
                    48,
                    destinations[1].Y + 24,
                    source.Width, source.Height
                );
                sb.Draw(texture, destination, source, Color.White);
            }

            // if (player has compass)
            {
                Rectangle source = InventoryTextureStorage.COMPASS;
                Rectangle destination = new Rectangle(
                    44,
                    destinations[1].Y + 64,
                    source.Width, source.Height
                );
                sb.Draw(texture, destination, source, Color.White);
            }

            // if (player has bombs)
            {
                Rectangle source = InventoryTextureStorage.BOMB;
                Rectangle destination = new Rectangle(
                    130,
                    destinations[0].Y + 47,
                    source.Width, source.Height
                );
                sb.Draw(texture, destination, source, Color.White);
            }

            // if (player has boomerang)
            {
                Rectangle source = InventoryTextureStorage.BOOMERANG;
                Rectangle destination = new Rectangle(
                    156,
                    destinations[0].Y + 47,
                    source.Width, source.Height
                );
                sb.Draw(texture, destination, source, Color.White);
            }

            // draw the selected item
            {
                Rectangle destination = new Rectangle(
                    68,
                    destinations[0].Y + 47,
                    InventoryTextureStorage.BOMB.Width,
                    InventoryTextureStorage.BOMB.Height
                );
                IEquipable current = this.player.currentItem;
                if (current is Bomb)
                {
                    sb.Draw(texture, destination, InventoryTextureStorage.BOMB, Color.White);
                }
                //else if (current is Boomerang)
                {
                    sb.Draw(texture, destination, InventoryTextureStorage.BOOMERANG, Color.White);
                }
            }
        }

        public void CursorLeft()
        {

        }

        public void CursorRight()
        {

        }

        private int keypressCooldown = 10;
        public void Update()
        {
            
            if (keypressCooldown > 0)
            {
                keypressCooldown--;
                return;
            }

            var keys = new HashSet<Keys>(Keyboard.GetState().GetPressedKeys());
            if (keys.Contains(Keys.Enter))
            {
                game.currentState = game.currentGamePlayState;
                return;
            }

            if (keys.Contains(Keys.Left))
            {
                keypressCooldown = 10;
                CursorLeft();
                return;
            }

            if (keys.Contains(Keys.Left))
            {
                keypressCooldown = 10;
                CursorLeft();
                return;
            }
        }
    }
}
