using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Rook
{
    public class Map
    {
        public MapTile[,] GMap { get; set; }
        public List<PhysicalObject> MapObjects { get; set; }
        Texture2D _mapTexture0;
        //Texture2D mapTexture1;  // animation image 1
        //Texture2D mapTexture2;  // animation image 2

        public Map()
        {
            GMap = new MapTile[ApplicationGlobals.MAP_HEIGHT / ApplicationGlobals.TILE_SIZE, 
                ApplicationGlobals.MAP_WIDTH / ApplicationGlobals.TILE_SIZE];
            MapObjects = new List<PhysicalObject>();

            for (var i = 0; i < ApplicationGlobals.MAP_HEIGHT / ApplicationGlobals.TILE_SIZE; i++)
            {
                for (var j = 0; j < ApplicationGlobals.MAP_WIDTH / ApplicationGlobals.TILE_SIZE; j++)
                {
                    GMap[i, j] = new MapTile
                    {
                        CollisionType = CollisionType.None,
                        MapDest = {X = j*ApplicationGlobals.TILE_SIZE, Y = i*ApplicationGlobals.TILE_SIZE}
                    };

                    GMap[i, j].MapDest.Height = GMap[i, j].MapDest.Width = ApplicationGlobals.TILE_SIZE;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //var rand = new Random();

            for (var i = 0; i < ApplicationGlobals.MAP_HEIGHT / ApplicationGlobals.TILE_SIZE; i++)
            {
                for (var j = 0; j < ApplicationGlobals.MAP_WIDTH / ApplicationGlobals.TILE_SIZE; j++)
                {
                    spriteBatch.Draw(_mapTexture0, GMap[i, j].MapDest, GMap[i, j].MapSrc, Color.White);
                }
            }
        }

        public void Load(ContentManager content, string file)
        {
            _mapTexture0 = content.Load<Texture2D>("mapTiles");

            var mapFile = new System.IO.StreamReader(file);
            string mapLine;
            var h = 0;

            while ((mapLine = mapFile.ReadLine()) != null)
            {
                var mapElt = mapLine.Split(' ');

                for (var w = 0; w < ApplicationGlobals.MAP_WIDTH / ApplicationGlobals.TILE_SIZE; w++)
                {
                    GMap[h, w].MapSrc.Width = GMap[h, w].MapSrc.Height = ApplicationGlobals.TILE_SIZE;
                    GMap[h, w].DisplayChar = char.Parse(mapElt[w]);
                }

                h++;
            }
            
            SetSource();
        }

        void SetSource()
        {
            var rand = new Random();
            for (var h = 0; h < ApplicationGlobals.MAP_HEIGHT / ApplicationGlobals.TILE_SIZE; ++h)
                for (var w = 0; w < ApplicationGlobals.MAP_WIDTH / ApplicationGlobals.TILE_SIZE; ++w)
                {
                    int t;

                    switch (GMap[h, w].DisplayChar)
                    {
                        case 'a':
                            GMap[h, w].MapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'b':
                            GMap[h, w].MapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'c':
                            GMap[h, w].MapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'd':
                            GMap[h, w].MapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 1 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'e':
                            GMap[h, w].MapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 1 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'f':
                            GMap[h, w].MapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 1 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'g':
                            GMap[h, w].MapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 2 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'h':
                            GMap[h, w].MapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 2 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'i':
                            GMap[h, w].MapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 2 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'j':
                            GMap[h, w].MapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 3 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'k':
                            GMap[h, w].MapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 3 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'l':
                            GMap[h, w].MapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 3 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'm':
                            GMap[h, w].MapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 4 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'n':
                            GMap[h, w].MapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 4 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'o':
                            GMap[h, w].MapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 4 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'p':
                            GMap[h, w].MapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 5 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'q':
                            GMap[h, w].MapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 5 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'r':
                            GMap[h, w].MapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 5 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 's':
                            GMap[h, w].MapSrc.X = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 6 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 't':
                            GMap[h, w].MapSrc.X = 1 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 6 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'u':
                            GMap[h, w].MapSrc.X = 2 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 6 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'A':
                            GMap[h, w].MapSrc.X = 5 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'B':
                            GMap[h, w].MapSrc.X = 4 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        case 'C':
                            GMap[h, w].MapSrc.X = 3 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Full;
                            break;
                        // Spike
                        case '^':
                            t = rand.Next(100);
                            if (t < 33)
                                t = 1;
                            else if (t < 66)
                                t = 2;
                            else
                                t = 0;
                            GMap[h, w].MapSrc.X = t * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 9 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Damage;
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
                            GMap[h, w].MapSrc.X = t * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 8 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.Damage;
                            break;
                        // Sky / Background
                        case '.':
                            t = rand.Next(100);
                            if (t < 2)
                                t = 1;
                            else if (t < 4)
                                t = 2;
                            else
                                t = 0;
                            GMap[h, w].MapSrc.X = t * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 7 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.None;
                            break;
                        default:
                            GMap[h, w].MapSrc.X = 3 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].MapSrc.Y = 0 * ApplicationGlobals.TILE_SIZE;
                            GMap[h, w].CollisionType = CollisionType.None;
                            break;
                    }
                }
        }
    }
}
