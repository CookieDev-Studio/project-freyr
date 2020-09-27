using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Board;

namespace project_freyr
{
    public class ProjectFreyr : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont consolas;

        private readonly int worldSize;
        private int chunkSize = 100;

        private readonly int screenHeight;
        private readonly int screenWidth;

        private World world;
        private Chunk viewedChunk;

        public ProjectFreyr(int worldSize)
        {
            _graphics = new GraphicsDeviceManager(this);
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            
            Window.IsBorderless = true;
            IsMouseVisible = true;

            Content.RootDirectory = "Content";

            this.worldSize = worldSize;
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            //set up screen
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();

            world = WorldOperations.generateWorld(worldSize);
            viewedChunk = WorldOperations.getChunk(chunkSize, world, 0, 0);
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

            //_spriteBatch.Begin();
            
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _spriteBatch.DrawString(consolas, "Controls:", new Vector2(_graphics.PreferredBackBufferWidth / 4 * 3, 20), Color.White);

            for (var x = 0; x < chunkSize; x++)
                for (var y = 0; y < chunkSize; y++)
                {
                    var entity = viewedChunk.tiles[y, x];

                    var tileSize = screenHeight / chunkSize;
                    var stringScale = tileSize / consolas.MeasureString(entity.character.ToString()).X - 0.1f;
                    _spriteBatch.DrawString(
                        consolas,
                        entity.character.ToString(),
                        new Vector2(y, x) * screenHeight / chunkSize,
                        entity.colour,
                        0f, 
                        Vector2.Zero, 
                        stringScale, 
                        SpriteEffects.None, 
                        0);
                }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
