using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Rook
{
    public class Map
    {
        public MapTile[,] gMap;
        public List<PhysicalObject> mapObjects;
        Texture2D mapTexture0;
        //Texture2D mapTexture1;  // animation image 1
        //Texture2D mapTexture2;  // animation image 2

        public Map()
        {
            gMap = new MapTile[ApplicationGlobals.MAP_HEIGHT / ApplicationGlobals.TILE_SIZE, ApplicationGlobals.MAP_WIDTH / ApplicationGlobals.TILE_SIZE];
            mapObjects = new List<PhysicalObject>();

            for (int i = 0; i < ApplicationGlobals.MAP_HEIGHT / ApplicationGlobals.TILE_SIZE; i++)
            {
                for (int j = 0; j < ApplicationGlobals.MAP_WIDTH / ApplicationGlobals.TILE_SIZE; j++)
                {
                    gMap[i, j] = new MapTile();

                    gMap[i, j].collisionType = CollisionType.None;
                    gMap[i, j].mapDest.X = j * ApplicationGlobals.TILE_SIZE;
                    gMap[i, j].mapDest.Y = i * ApplicationGlobals.TILE_SIZE;
                    gMap[i, j].mapDest.Height = gMap[i, j].mapDest.Width = ApplicationGlobals.TILE_SIZE;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ApplicationGlobals.MAP_HEIGHT / ApplicationGlobals.TILE_SIZE; i++)
            {
                for (int j = 0; j < ApplicationGlobals.MAP_WIDTH / ApplicationGlobals.TILE_SIZE; j++)
                {
                    spriteBatch.Draw(mapTexture0, gMap[i, j].mapDest, gMap[i, j].mapSrc, Color.White);
                }
            }
        }

        public void Load(ContentManager Content, string file)
        {
            mapTexture0 = Content.Load<Texture2D>("mapTiles");

            System.IO.StreamReader mapFile = new System.IO.StreamReader(file);
            string mapLine;
            int w = 0, h = 0;
            Random rand = new Random();

            while ((mapLine = mapFile.ReadLine()) != null)
            {
                string[] mapElt = mapLine.Split(',');

                for (w = 0; w < ApplicationGlobals.MAP_WIDTH / ApplicationGlobals.TILE_SIZE; w++)
                {
                    gMap[h, w].mapSrc.Width = gMap[h, w].mapSrc.Height = ApplicationGlobals.TILE_SIZE;
                    gMap[h, w].displayChar = char.Parse(mapElt[w]);
                }

                h++;
            }
            
            SetSource();
        }

        void SetSource()
        {
            int t = 0;
            Random rand = new Random();

            for(int h = 0; h < ApplicationGlobals.MAP_HEIGHT / ApplicationGlobals.TILE_SIZE; ++h)
                for(int w = 0; w < ApplicationGlobals.MAP_WIDTH / ApplicationGlobals.TILE_SIZE; ++w)
                {
                    switch (gMap[h, w].displayChar)
                    {
                        case 'a':
                            gMap[h, w].mapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'b':
                            gMap[h, w].mapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'c':
                            gMap[h, w].mapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'd':
                            gMap[h, w].mapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 1 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'e':
                            gMap[h, w].mapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 1 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'f':
                            gMap[h, w].mapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 1 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'g':
                            gMap[h, w].mapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 2 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'h':
                            gMap[h, w].mapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 2 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'i':
                            gMap[h, w].mapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 2 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'j':
                            gMap[h, w].mapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 3 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'k':
                            gMap[h, w].mapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 3 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'l':
                            gMap[h, w].mapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 3 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'm':
                            gMap[h, w].mapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 4 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'n':
                            gMap[h, w].mapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 4 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'o':
                            gMap[h, w].mapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 4 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'p':
                            gMap[h, w].mapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 5 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'q':
                            gMap[h, w].mapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 5 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'r':
                            gMap[h, w].mapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 5 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 's':
                            gMap[h, w].mapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 6 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 't':
                            gMap[h, w].mapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 6 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'u':
                            gMap[h, w].mapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 6 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'A':
                            gMap[h, w].mapSrc.X = 5 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'B':
                            gMap[h, w].mapSrc.X = 4 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case 'C':
                            gMap[h, w].mapSrc.X = 3 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Full;
                            break;
                        case '^':
                            t = rand.Next(100);
                            if (t < 33)
                                t = 1;
                            else if (t < 66)
                                t = 2;
                            else
                                t = 0;
                            gMap[h, w].mapSrc.X = t * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 9 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Damage;
                            break;
                        // Acid
                        case '~':
                            t = rand.Next(100);
                            if (t < 33)
                                t = 1;
                            else if (t < 66)
                                t = 2;
                            else
                                t = 0;
                            gMap[h, w].mapSrc.X = t * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 8 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.Damage;
                            break;
                        // Sky / Background
                        case '.':
                            t = rand.Next(100);
                            if (t < 3)
                                t = 1;
                            else if (t < 6)
                                t = 2;
                            else
                                t = 0;
                            gMap[h, w].mapSrc.X = t * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 7 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.None;
                            break;
                        default:
                            gMap[h, w].mapSrc.X = 3 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].mapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            gMap[h, w].collisionType = CollisionType.None;
                            break;
                    }
                }
        }
    }
}
