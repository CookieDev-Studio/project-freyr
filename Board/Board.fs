module Board

open Entities
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics


let tiles = 
    Array2D.init 30 30 
        (fun y _ -> 
            if y = 15 then Entities.watter                                                                                                         
            else Entities.grass )

let draw (spriteBatch : SpriteBatch) tiles font (spacing : int ) = 
    tiles |> Array2D.iteri (fun y x entity -> spriteBatch.DrawString(font, entity.character.ToString(), new Vector2( float32 x, float32 y ) * float32(spacing), entity.colour) )
