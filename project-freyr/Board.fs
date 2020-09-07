module Board

open Entities

let tiles = 
    Array2D.init 30 30 
        (fun y x -> 
            if x = 1 then Entities.watter                                                                                                         
            else Entities.grass )

let size = tiles.GetUpperBound 0