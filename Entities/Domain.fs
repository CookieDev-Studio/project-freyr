namespace Entities

open Microsoft.Xna.Framework

type Movement =
|Static
|Dynamic of int

type Entity =
    { character : char
      colour : Color 
      movement : Movement}

