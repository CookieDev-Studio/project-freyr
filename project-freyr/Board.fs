module Board

open Entities
open System

let tiles = 
    Array2D.init 30 30 
        (fun _ _ ->
            let random = Random()
            let number = random.Next(3)
            match number with
            | 1 -> Entities.watter                                                                                                    
            | 2 -> Entities.grass 
            | _ -> Entities.stone)

let size = tiles.GetUpperBound 0