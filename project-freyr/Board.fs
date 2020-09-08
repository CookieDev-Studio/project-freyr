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

    let getChunk chunkSize (world : World) (rootY, rootX) =
        let createChunk rootY rootX = 
            { root = (rootY, rootX)
              tiles = Array2D.init chunkSize chunkSize 
                            ( fun y x -> 
                              world.tiles.[rootY + y, rootX + x] ) }

        let worldSize = world.tiles.GetUpperBound 0
        if rootY + chunkSize > worldSize then
            createChunk (rootY - (rootY + chunkSize - worldSize)) rootX
        elif rootY < 0 then
            createChunk (rootY + 1) rootX
        elif rootX + chunkSize > worldSize then
            createChunk rootY (rootX - (rootX + chunkSize - worldSize))
        elif rootX < 0 then
            createChunk rootY (rootX + 1)
        else
            createChunk rootY rootX
