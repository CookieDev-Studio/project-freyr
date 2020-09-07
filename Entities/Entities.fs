namespace Entities

open Microsoft.Xna.Framework

module Entities =
    let watter = 
        { character = '~'
          colour = Color.Blue 
          movement = Static}
    let grass =
        { character = '#'
          colour = Color.YellowGreen 
          movement = Static}