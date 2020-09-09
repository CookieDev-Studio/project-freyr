namespace Board

open Entities
open System

type Chunk =
    { root : int * int
      tiles : Entity[,] }
type World =
    { tiles : Entity[,] }

module WorldOperations =
    
    let getNeighbours (x, y) (tiles :  Entity[,]) =
        let addIf condition (x, y) list =
            if condition then list |> List.append [tiles.[x, y]]
            else list
    
        let bounds = tiles.GetUpperBound 0
    
        []
        |> addIf (x + 1 < bounds) (x + 1, y)
        |> addIf (y + 1 < bounds) (x, y + 1)
        |> addIf (x - 1 >= 0) (x - 1, y)
        |> addIf (y - 1 >= 0) (x, y - 1)
    
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

    let generateWorld worldSize =
        let initiateTiles = Array2D.create worldSize worldSize Entities.water

        let spawnLand (tiles : Entity[,]) =
            let random = Random()
            for _ in [0..worldSize] do
                tiles.[random.Next(worldSize), random.Next(worldSize)] <- Entities.grass
            tiles

        let determineTile (neighbours : Entity List) entity =
            let random = Random()
            let amountOfGrassNeighbours = (neighbours |> List.where(fun x -> x = Entities.grass)).Length
            if amountOfGrassNeighbours > random.Next 4
            then Entities.grass
            else entity

        let growLand tiles =
            tiles
            |> Array2D.mapi 
                (fun x y entity -> 
                    match entity with
                    |x when x = Entities.grass -> entity
                    |_ -> determineTile (getNeighbours (x, y) tiles) entity)


        let generatedTiles =
            initiateTiles
            |> spawnLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand
            |> growLand

        { tiles = generatedTiles }
