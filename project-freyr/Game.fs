module game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type ProjectFreyr () as x =
    inherit Game()

    do x.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(x)
    let mutable spriteBatch = null
    let mutable consolas = null

    override x.Initialize() =
        base.Initialize()
        ()

    override x.LoadContent() =
        spriteBatch <- new SpriteBatch(x.GraphicsDevice);
        consolas <- x.Content.Load<SpriteFont>("Consolas");
        ()

    override x.Update (gameTime) =
        ()

    override x.Draw (gameTime) =
        x.GraphicsDevice.Clear Color.Black
        spriteBatch.Begin()
        Board.tiles |> Array2D.iteri (fun y x entity ->
            let tileDistance = graphics.PreferredBackBufferHeight |> float32

            spriteBatch.DrawString(
                consolas,
                entity.character.ToString(),
                new Vector2( float32 x, float32 y ) * tileDistance / (Board.size |> float32),
                entity.colour))
 
        spriteBatch.End()
        ()