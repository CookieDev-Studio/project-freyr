namespace Board

open Entities
open System

type Chunk =
    { root : int * int
      tiles : Entity[,] }
type World =
    { tiles : Entity[,] }

module WorldOperations =

    let generateWorld worldSize =
        let generatedTiles =
            Array2D.init worldSize worldSize
                ( fun x y -> if x = 2 && y = 0 then Entities.grass else Entities.water)
                (*( fun _ _ ->
                    let random = Random()
                    let number = random.Next(3)
                    match number with
                    | 1 -> Entities.water                                                                                                    
                    | 2 -> Entities.grass 
                    | _ -> Entities.stone )*)

        { tiles = generatedTiles }

    let getChunk chunkSize (world : World) root =
        let createChunk rootX rootY = 
            { root = (rootX, rootY)
              tiles = Array2D.init chunkSize chunkSize 
                            ( fun x y -> 
                              world.tiles.[rootX + x, rootY + y] ) }

        let worldSize = world.tiles.GetUpperBound 0
        match root with
        | x, y when x + chunkSize > worldSize -> createChunk (x - (x + chunkSize - worldSize)) y
        | x, y when x < 0 -> createChunk (x + 1) y
        | x, y when y + chunkSize > worldSize -> createChunk x (y - (y + chunkSize - worldSize))
        | x, y when y < 0 -> createChunk x (y + 1)
        | x, y -> createChunk x y