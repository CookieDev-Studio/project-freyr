namespace Entities

open Microsoft.Xna.Framework

module Entities =
    let water = 
        { character = '~'
          colour = Color.DeepSkyBlue
          movement = Static}

    let grass =
        { character = '#'
          colour = Color.YellowGreen 
          movement = Static}

    let stone =
        { character = '^'
          colour = Color.LightGray
          movement = Static}