namespace Domain

open Microsoft.Xna.Framework

type Entity =
    { character : char
      colour : Color 
      position : int * int}