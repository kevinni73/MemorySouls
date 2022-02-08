# Memory Souls

Memory Souls is a top-down Souls-like game made as a proof of concept for a "combo"-based attack system. Memory Souls was originally made in MonoGame using Prime31's Nez framework and was later remade in Unity over a few days.

Fight a boss by getting close to it and entering the prompted buttons to complete a "combo." The combo will reset if you're too far away from the boss, but it'll always be the same combo until its successfully completed. Don't forget which buttons you've already pressed, and make sure to roll in order to dodge attacks!

## Controls
Controler Recommended

- Move: WASD / Left Stick
- Roll: Spacebar / Right Trigger
- Attacks: Arrow keys / Face Buttons
- Quit: ESC

## How to Play
- Browser: https://kevinni73.github.io/MemorySouls/
    - note: the WebGL build can't detect input device changes, so it will always display Xbox controls
- Windows PC: Unzip "/Build/Memory Souls Windows Build.zip" and run "Memory Souls.exe"

## Gameplay Footage
https://youtu.be/u_m_QQE2LoQ

## Screenshots
![](Screenshots/game.png)

Old version in Monogame, for posterity:
![](Screenshots/old_game.png)

## Potential Improvements and Personal Notes
The most notable potential improvements to game feel:
- More attack patterns for the boss
    - I would prioritize this next if I were to continue working on this. However, I found the boss to be already challenging enough, and the main purpose of this proof of concept is testing whether the combo attack system is enjoyable, not dodging a variety of attacks (already done by Souls-like games).
- Sprite rendering order based on their y-axis position
    - Would accomplish this by either splitting sprites into foreground and background sprites, or adjusting the pivot point of sprites and changing Unity's sprite sort mode. I decided against this for the sake of this prototype since it doesn't affect the core gameplay.

Potential improvements to game code:
- The FSM states of an entity are currently all within a single file. I re-used a State Machine class from another action platformer that I'm working on that also follows this pattern, which involves a lot of interaction between states and careful ordering of operations (picked up from [Celeste's Player file](https://github.com/NoelFB/Celeste/tree/master/Source/Player)). For this game I figured the trade-off of re-using my existing code was worth it as I was the only developer on a game with well-defined scope. However, if that were not the case I would spend time refactoring the state machine and make each state a separate class/component.
- Some entities are unnecessarily coupled / referenced directly by name, such as some Canvas UI elements and the victory music. I would consider creating service locator classes to help decouple some of these systems.

## Assets
- Player sprites by [Gamekrazzy](https://gamekrazzy.itch.io/8-direction-top-down-character)
- Crystal Knight by [Brother Daniel](https://brotherdaniel.itch.io/crystal-knight)
- Background sprites by [Cainos](https://cainos.itch.io/pixel-art-top-down-basic)
- Moonlight Greatsword by [DatGuyJekkt](https://www.deviantart.com/datguyjekkt/art/PIXEL-ART-Dark-Souls-Moonlight-Greatsword-795459841), Creative Commons Attribution 3.0 License
- Input key prompts by [XELU](https://thoseawesomeguys.com/prompts/), Creative Commons 0 License
- Music by [Wingless Seraph](https://wingless-seraph.net/en/material-music_boss.html)

## License
[MIT](https://choosealicense.com/licenses/mit/)