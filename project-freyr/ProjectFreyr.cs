using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Board;
using System.Runtime.CompilerServices;

namespace project_freyr
{
    public class ProjectFreyr : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont consolas;

        private int worldSize = 200;
        private int chunkSize = 45;

        private World world;
        private Chunk viewedChunk;

        public ProjectFreyr()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            world = WorldOperations.generateWorld(worldSize);
            viewedChunk = WorldOperations.getChunk(chunkSize, world, 0, 0);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            consolas = Content.Load<SpriteFont>("Consolas");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keys = Keyboard.GetState();
            var root = viewedChunk.root;

            if (keys.IsKeyDown(Keys.S) || keys.IsKeyDown(Keys.Down))
                viewedChunk = WorldOperations.getChunk(chunkSize, world, root.Item1, root.Item2 + 1);
            if (keys.IsKeyDown(Keys.W) || keys.IsKeyDown(Keys.Up))
                viewedChunk = WorldOperations.getChunk(chunkSize, world, root.Item1, root.Item2 - 1);
            if (keys.IsKeyDown(Keys.D) || keys.IsKeyDown(Keys.Right))
                viewedChunk = WorldOperations.getChunk(chunkSize, world, root.Item1 + 1, root.Item2);
            if (keys.IsKeyDown(Keys.A) || keys.IsKeyDown(Keys.Left))
                viewedChunk = WorldOperations.getChunk(chunkSize, world, root.Item1 - 1, root.Item2);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            for (var x = 0; x < chunkSize; x++)
                for (var y = 0; y < chunkSize; y++)
                {
                    var tileDistance = _graphics.PreferredBackBufferHeight;
                    var entity = viewedChunk.tiles[y, x];
                    _spriteBatch.DrawString(
                        consolas,
                        entity.character.ToString(),
                        new Vector2(y, x) * tileDistance / (chunkSize),
                        entity.colour);
                }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
