1. Start Scene File
MainMenu.unity

2. How to Play
There are four levels each separated by a jump pad that will boost the player up to the next level. To move the player around use the "W" and "S" key, and to turn the player to a new direction, use the "A" and "D" keys. Press "Spacebar'' to jump. When conversing with NPCs, press “Spacebar” to proceed to the next dialogue. Left click to speed up dialogue text.

Level 1 (The Forest): This is the first level. The player will have to talk to the walking NPC to be guided over to the forest, where the player will talk to the NPC again to begin a puzzle for the area. After the puzzle is solved, the player can pick up the key next to the pond and jump on the blue jump pad in the other corner of the map (that previously had an “X” over it) to boost up to the next level.

The Forest Puzzle Solution: Press on the 4 pressure plates in order. The first one has 1 tree directly next to it, the second has 2 trees next to it, the 3rd has 3 trees, and the 4th has 4 trees.

Level 2 (The Maze): The player will start by talking to the NPC who will instruct the player that they have entered the maze of memories, and they need to collect multiple “memories” throughout the maze in order to unlock the jump pad and get to the next level. Navigate the maze and collect 5 memories (yellow cubes) and then locate the jump pad to get to the next level.

Level 3 (The Circuit): The player lands in front of the npc and begins bouncing. The bouncing will gradually stop. When interacting with the npc, the npc says some filler dialogue. When the player goes through the purple curbs they get pushed forward. Using these speed curbs, the player must navigate through the 3 phases of the level. The first phase is just a tutorial phase. For the second phase, the player must jump through the first curb onto the slippery platform with two more curbs, and then the player must use that speed to get onto the third slippery platform at the end of the phase. On the third phase the player must use the curbs and slope at the correct time to get past the moving obstacle. The obstacles make the player bounce back. Then the player can interact with another npc with some more filler dialogue then use the jump pad to proceed to the next level.

Level 4 (The Clouds): For the final level, the player must talk to the pink fairy NPC (sphere object) and then collect 5 yellow coins floating around in the clouds throughout the level. After 5 coins have been collected, the player must talk to the NPC again to unlock the key to opening the jump pad.

After the player jumps on the level 4 jump pad, the end game sequence is triggered which will play for about 30-40 seconds.

Important Gameplay Features for Testing/Grading: An activatable “god-mode” is available by pressing the “G” key at any time. When the key is pressed, the jump pads blockers will all go away, so the player can traverse through the 4 levels. If the “G” key is pressed in level 3 (The Circuit), then the player will be teleported to the end of the challenge where the jump pad is.

Technology Requirements (referencing “TeamGameDesignReqs.pdf” Assessment Criteria list):
1-2. Game is made in Unity and has a 3D world. 
3. Main player is controlled and has blended animations with Mecanim using state management.
4. The NPC on Level 1 (The Forest) has multiple states with a NavMeshAgent that has path planning to go to various points around the level. The multiple states involve: walking between multiple waypoints, talking to the player, navigating to a different waypoint, then talking to the player again. The NPC is critical to setting up the puzzle for the player (as well as advancing the game’s story). See “Level1NPCController.cs:62” for the state machine.
5. Level 3 (The Circuit) utilizes the player’s rigidbody to create an engaging challenge for the player to progress through. The speed gates programmatically affect the player's rigidbody and help the player to navigate the level. Level 4 (The Clouds) also introduces the “jump reset” mechanic to traverse around the level and pick up the coins. See “SpeedUp.cs and PlayerController.cs”
6. Levels have dynamic and responsive audio, including constant background audio at certain places as well as audio accompanying specific actions like unlocking a key or landing on a new level. There is a constant sky/cloud backdrop that ties together all the levels, making the player feel like they are proceeding up into the sky which ties into the main storyline. Controls are responsive based on the level and certain physics that are present.
7. The player can make choices related to which NPCs to interact with as well as how to complete the puzzle in level 1. There is also complete freedom in how to best approach the Level 4 (The Clouds) to find the best way to collect the required 5 coins using the jump reset objects around the map.
8. There is a Credits Menu in the Main Menu. The in-game HUD is unintrusive and the Pause Menu is responsive. The Main Menu also fits the general theme of the game, and the end cutscene ties it all together.

3. Known Problem Areas
Level 3:
When walking into walls at high speeds it is possible that the player may get stuck on the wall until they stop walking forward. When proceeding to the next level, if the player misses the jump they may get stuck on the ceiling of the level and must walk back to the end of the level to drop back down to the jump pad.

4. Manifest of files edited by each teammate
Jonathan McLatcher: 
Primary Responsibilities: Level 1 (The Forest), “End Game” Sequence, Combining all 4 Prefabbed levels into MainScene.unity, Implementing Ambience Audio in Level 1, 4, and End Game Scene
Scripts Created: PlayerController.cs, DialogueAnimation.cs, Level1NPCController.cs, NPCPlayerCollision.cs, PuzzleManager.cs, PuzzlePlateManager.cs, AudioManager.cs
Scripts Edited: PlayerController.cs

Jenny Liu:
Primary Responsibilities: Level 2 (The Maze)
Scripts Created: DialogueAnimationLvl2.cs, Level2NPCCollision.cs, Level2NPCController.cs, MemoryCountController.cs
Scripts Edited: PlayerController.cs

Derek Lin:
Primary Responsibilities: Level 3 (The Circuit)
Scripts Created: DialogueAnimationlvl3.cs, Level3NPCController, PopSoundPlayer.cs, SpeedUp.cs

Steven Tong:
Primary Responsibilities: Level 4 (The Clouds)
Scripts Created: FairyCollision.cs, L4DialogueAnim.cs, Level4NPCController.cs
Scripts Edited: PlayerController.cs

Luke Shively:
Primary Responsibilities: Main Menu (MainMenu.unity), Pause Menu, Options Menu (including Audio Settings/Manager), Game HUD
Scripts Created: GameQuitter.cs, GameStarter.cs, ContinueManager.cs, PauseManager.cs
Scripts Edited: GameManager.cs

