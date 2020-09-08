namespace World

open Entities
open System
open Microsoft.Xna.Framework

type Chunk =
    { root : int * int
      tiles : Entity[,] }
type World =
    { tiles : Entity[,] }

module WorldOperations =

    let generateWorld worldSize =
        let generatedTiles =
            Array2D.init worldSize worldSize
                ( fun _ _ ->
                    let random = Random()
                    let number = random.Next(3)
                    match number with
                    | 1 -> Entities.watter                                                                                                    
                    | 2 -> Entities.grass 
                    | _ -> Entities.stone )

        { tiles = generatedTiles }

    let getChunk chunkSize (world : World) root =
        let createChunk rootY rootX = 
            { root = (rootY, rootX)
              tiles = Array2D.init chunkSize chunkSize 
                            ( fun y x -> 
                              world.tiles.[rootY + y, rootX + x] ) }

        let worldSize = world.tiles.GetUpperBound 0
        match root with
        | y, x when y + chunkSize > worldSize -> createChunk (y - (y + chunkSize - worldSize)) x
        | y, x when y < 0 -> createChunk (y + 1) x
        | y, x when x + chunkSize > worldSize -> createChunk y (x - (x + chunkSize - worldSize))
        | y, x when x < 0 -> createChunk y (x + 1)
        | y, x -> createChunk y x