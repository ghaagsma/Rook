using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Rook
{
    public class Main : Game
    {
        SpriteBatch _spriteBatch;

        private readonly Map _map;
        private List<PhysicalObject> _objects;

        public Main()
        {
            Content.RootDirectory = "Content";
            _map = new Map();
            _objects = new List<PhysicalObject> { new Hero() };

            new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = ApplicationGlobals.MAP_WIDTH,
                PreferredBackBufferHeight = ApplicationGlobals.MAP_HEIGHT,
                IsFullScreen = false
            };
        }

        // Unused
        //protected override void Initialize()
        //{
        //    base.Initialize();
        //}

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _map.Load(Content, "map1.txt");
            foreach (var o in _objects)
            {
                o.Load(Content);
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non-ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var o in _objects)
            {
                o.Update(gameTime, _map.GMap);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            _spriteBatch.Begin();
            _map.Draw(_spriteBatch);
            foreach (var o in _objects)
            {
                o.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
