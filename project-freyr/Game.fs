module game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open World

type ProjectFreyr () as x =
    inherit Game()

    do x.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(x)
    let mutable spriteBatch = null
    let mutable consolas = null

    let worldSize = 100
    let chunkSize = 35

    let world = WorldOperations.generateWorld worldSize
    let moveChunk = WorldOperations.getChunk chunkSize world

    let mutable chunk = moveChunk (0, 0) 

    override x.Initialize() =
        x.IsMouseVisible <- true
        base.Initialize()
        ()

    override x.LoadContent() =
        spriteBatch <- new SpriteBatch(x.GraphicsDevice);
        consolas <- x.Content.Load<SpriteFont>("Consolas");
        ()

    override x.Update (gameTime) =
        let keys = Keyboard.GetState()

        if keys.IsKeyDown(Keys.S) || keys.IsKeyDown(Keys.Down) then
            let (Y,X) = chunk.root
            chunk <- moveChunk(Y + 1, X)

        if keys.IsKeyDown(Keys.W) || keys.IsKeyDown(Keys.Up) then
            let (Y,X) = chunk.root
            chunk <- moveChunk(Y - 1, X)

        if keys.IsKeyDown(Keys.D) || keys.IsKeyDown(Keys.Right) then
            let (Y,X) = chunk.root
            chunk <- moveChunk(Y, X + 1)
            
        if keys.IsKeyDown(Keys.A) || keys.IsKeyDown(Keys.Left) then
            let (Y,X) = chunk.root
            chunk <- moveChunk(Y, X - 1)
        ()

    override x.Draw (gameTime) =
        x.GraphicsDevice.Clear Color.Black
        spriteBatch.Begin()
        chunk.tiles |> Array2D.iteri (fun y x entity ->
            let tileDistance = graphics.PreferredBackBufferHeight |> float32

            spriteBatch.DrawString(
                consolas,
                entity.character.ToString(),
                new Vector2( float32 x, float32 y ) * tileDistance / (chunkSize |> float32),
                entity.colour))

        spriteBatch.End()
        ()