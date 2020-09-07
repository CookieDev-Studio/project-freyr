module Board

open Domain
open Microsoft.Xna.Framework

let tiles : Entity [,] = 
    Array2D.init 30 30 
        (fun y x -> 
            if y = 15 then
                { character = '~' 
                  colour = Color.Blue
                  position = x, y }                                                                                                             
            else
                { character = '^' 
                  colour = Color.Green
                  position = x, y })                       