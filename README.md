# Dead Man's Maze

**Game Design Document**



# Table of Contents

1. Game Description

- 1.1 Elevator Pitch
- 1.2 Summary
- 1.3 Unique Selling Points (USPs)

2. Design

- 2.1 Key Mechanics
- 2.2 Characters and Settings

3. Gameplay

- 3.1 Beginning the Game
- 3.2 First 2-5 minutes

4. Visual-Audio

- 4.1 Art Style
- 4.2 Audio
- 4.3 Front End

5. Production

- 5.1 SWOT Analysis
- 5.2 Production Schedule

6. Prototyping

7. List of All Assets

8. The Team



# 1. Game Description

## 1.1 Elevator Pitch
An atmospheric difficulty increasing horror first-person shooter in which the player must escape a maze by first finding the keys to the exit and then rushing to the exit.

## 1.2 Summary
The game consists of having the player inside a randomly generated maze with very limited lighting. The atmosphere will be set mainly through sound where the player will constantly hear moans, steps, and scratching sounds “coming from” the monsters they must either avoid or take out. The player will have limited ammo for self defense which will only refill slightly when grabbing one of the keys they would need to collect to head to the exit and beat the level. The game has high replayability since the mazes are random each time.


## 1.3 Unique Selling Points (USPs)
- The mazes are randomly generated, which means players will theoretically get endless variations of the experience.
- The horror atmosphere will be conveyed by having sound be the most predominant way for the player to immerse themselves.
- Customizable experience: The player will be able to select how they wish to experience the game, they can make it extremely easy, lengthy or nerve wracking. Through selection of preset maze sizes, starting ammo amount and/or enemy difficulty (health and speed).

# 2. Design

## 2.1 Key Mechanics

The player controls the main character, and the game is viewed from a first person perspective. The character can move forwards and backwards, strafe to the sides, rotate, look up and down. The player will also have a small set of weapons to fight back the enemies.
To achieve the game's goal of getting all the collectables and reaching the exit, the player will navigate the maze, eliminating or avoiding the threats that will populate the environment. Eliminating the threats will require skill, as the player will not have too much ammunition to spare; in case the player runs out of firing power, the only option available will be to avoid combat completely, increasing the difficulty of traversing the maze.
The most relevant game mechanics will be familiar to most people with gaming experience: navigating new environments to find key items and shooting at enemies. The novelty of this proposal is that the maze is generated at random for each new game. While FPS shooting games and survival horror games are very popular, Dead Man’s Maze will combine elements from both genres, and also remove the certainty of knowing the map. The possibility to quickly start a new game and get a new maze to explore will be appealing to both experienced players that look for a different challenge where they can use their already honed skills, and also deal with many unknowns, and to new players that can start a game and get into it without the need to know too many mechanics or have an understanding of the game's world.
The game will be an immersive experience where the odds are against the player.


## 2.2 Characters and Settings
The game is set in the Modern day. Earth is close to being decimated by a virus that has caused the infected to turn into zombie-like creatures. Dr. Alex Fleming, an army medical doctor is tasked with coming up with a cure, and he thinks he just needs one more ingredient in order for it to be effective – the sweet-smelling herb Athelas. The only problem? Athelas is only known to grow in one place in the world: A remote farm in the south of France, whose owner had the twisted idea to grow the lifesaving plant in the middle of a maze that he had constructed himself, to keep it out of the hands of his enemies. Dr. Fleming and his team travel to the farm, and cautiously begin to enter the maze to retrieve the Athelas, when they realize the farm has been overrun with zombies! It’s up to the player to help Dr. Fleming work his way through using the few weapons in his arsenal, to survive and not only retrieve the Athelas, but make it out alive, and in time to save humanity!

# 3. Gameplay

## 3.1 Beginning the Game
The player is greeted by a dark screen with eerie music, where they get options to play, read the instructions or customize various aspects of the game, it could look something like this image:

![Main game menu](images/StartMenuMockup.png?raw=true "Main Menu")

## 3.2 First 2-5 minutes
The player is placed into a random spot in the maze, a screen shows up explaining the lore and what you are doing there, the player will have a button or text that says “Continue” or another one that says “Back to main menu” to return to the previous screen. When the player clicks “Continue” the game begins. The player has a flashlight that will flicker from time to time giving the situation a sense of uncertainty and helplessness. Sound will be atmospheric, having muffled groans, scratching against walls, uneven footsteps and dragging across the floor to give the player a sense that they are not alone in this maze. The player will begin to move the character forwards, backwards and sideways using the WASD keys on the keyboard and moving the camera using the Mouse. Upon encountering their first enemy (zombie), the player may choose to turn around and try to find a different route or fight. The player will have a certain amount of health which can be quantified similar to Call of Duty where the bloodier/redder the screen is, the fewer health points the player has. The player will walk around and shoot enemies in tight corridors (helps generate the common fear of claustrophobia) and will search for keys/items needed to open the exit, the player may be able to find the exit early on but without the keys they will not be able to beat the game. The player will get a very dim sound prompt when they are close to a key and it will intensify or diminish the closer they get. This “hot/cold” mechanic can help relieve some of the frustration a player may feel thus making the game appealing to a broader audience while still retaining an unsettling feeling.




# 4. Visual-Audio

## 4.1 Art Style

The art style will be a 3D polygon style which offers a timeless art style with colorful theme that makes the game unique and convey a playful feel. The game will use 3D polygon assets by Synty as much as possible to provide a consistent art style throughout the entire game including characters, items, building and the environment. Where a specific asset is unavailable, a custom model with texture will be made to resemble the art style as much as possible.

![art style](images/PolygonAdventure_04.webp)

*Figure 4.1: Polygon Adventure Pack (Campfire) Preview by Synty*

The gameplay style will be similar to POLYGON and , both are tactical first person shooter. The game will feature responsive controls, coupled with fast and fluid animation. The player can control the character to move, sprint, jump, crouch and shoot with polygon weapons to match the game's art style. The game will put heavy emphasis on gunplay with impactful shots at every bullet strike and shells ejecting from the gun, coupled with satisfying reload animations.

![POLYGON](images/POLYGON.jpg)

*Figure 4.2: POLYGON*

![World War Polygon](images/WorldWarPolygon.webp)

*Figure 4.3 World War Polygon*

## 4.2 Audio

On the start menu, the game will play an orchestral music to set the mood of the player to get ready to play, similar to Polyfield. Once in the game, the music will change between 2 states depending on what the player is doing. If the player has encountered an enemy, the music will get louder with fast tempo, otherwise the music will be quite with mellow tone.

As for the environmental sound, the sound will give a isolation settings.
The game will rely heavily on atmospheric sound giving the player a sense of dread and “stress” that there may be something right around the corner. Most of the sounds will play at random intervals so that the player may not find the audio cues predictable, but with certain spacing between them so that they feel organic and not part of a predictable algorithm

## 4.3 Front End

Upon starting the game, the player will be greeted with the start menu and a beautiful artwork in the background to invite the player to play the game. The menu UI will consists of a 'Start', 'Settings' and 'Exit buttons and leads to another menu page as shown in the wireframe below:

![Dead Man's Maze - Start Menu](images/Dead_Man's_Maze_-_Start_Menu.png)

*Figure 4.4: Start Menu Wireframe*

The game will feature a simple HUD system with slight transparency to allow the player immersive themselves in the gameplay without being distracted by complicated HUD. It consists of the health bar, ammo bar and gun selection slots as shown in the illustration below: 

![Dead Man's Maze - HUD](images/Dead_Man's_Maze_-_HUD.png)

*Figure 4.5: Gameplay with HUD Illustration*



# 5. Production

## 5.1 SWOT Analysis



## 5.2 Production Schedule





# 6. Prototyping





# 7. List of All Assets





# 8. The Team

| Role                   | Name                | ID        |
| ---------------------- | ------------------- | --------- |
| Project Manager        | Isabela de Oliveira |           |
| Programming            | Raymond Woon        | 190126210 |
| Art                    | Alwin Wong          | 200195717 |
| Design                 | Ernesto Arakaki     |           |
| Quality Assurance (QA) | Daniel Rodriguez    | 200103527 |
