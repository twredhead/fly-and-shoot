
Fly and Shoot Readme

*************************************************************************************************************
*************************************************************************************************************

About the game

This game is a practice project for getting more experience working with Unity Engine.

At the start of the project my goals for the game were:

1. Allow the player to fly around the world.
2. Have the player restart the level if they collide with anything.
3. Stop the player from traveling out of the game world.
4. Have enemies that fly around that the player can shoot and destroy.

Once these goals were completed I added some additional features:

1. Score values for shooting down enemies. The score value depends on the speed and if the   
   enemy oscillates or not.
2. There is a victory condition and a losing condition.
3. There is an instruction scene, a losing scene, and a victory scene.
4. Added a skybox to the main level. The skybox is from the Unity Asset Store and it is called SkyBox Volume 2 (Nebula)

If I were to continue with this project, I would add:

1. VFX for death.
2. VFX for level reload.
3. VFX for winning/losing.
4. VFX for enemy death.
5. VFX for enemy escaping (currently they just disappear at the world edge)
6. SFX for 1-3.
7. In game music.

*************************************************************************************************************
*************************************************************************************************************

Player Game Object

The player is a game object called PlayerRig. This is an empty object that has another empty object called Ship as a child. Ship has two 3D game objects and a particle system called Laser as children. The particle system is used for shooting enemies. 
Most of the scripts are on PlayerRig, however there is a script on the player ship which is used for firing the lasers and rotating the ship about its z axis (relative to the z axis of the parent PlayerRig). The rotations about the z axis make it look like the ship is banking left or right when the player steers the PlayerRig.

*************************************************************************************************************

Scripts on PlayerRig: 

1. PlayerMover

Purpose: Allow the player to control the direction that their ship goes. Also, cause the ship to move forward every frame.

Other Class References: None

Methods:
1. MoveForward()
   Called in Update(). This method causes the player to move forward (in the direction of the 
   local z unit vector) every frame.
2. PlayerMovementControls()
   Called in Update(). This method uses Unity’s Input.GetAxis(“Horizontal”) and “Vertical”  to 
   control rotations about the Y axis and X axis. This method allows the player to control which 
   direction is forward, so it allows the player to steer PlayerRig.

2. PlayerLocationManager

Purpose: Check to see if the player has gone outside the game world. If they have, restart the level after a small delay.

Other Class References:
1. GlobalPositioningSystem
2. UIManager
3. LevelManager

Methods:
1. OutOfBoundsChecker()
   Called in Update(). This method makes use of OutOfBounds(Transform transform) from 
   GlobalPositioningSystem to check if the player is out of bounds. If they are, they player is 
   warned using WarnPlayer() from UIManager. If the player has not moved back into the play 
   area after a set amount of time (decremented each frame using Timer() from LevelManager), 
   the level is reloaded using OutOfBoundsReload() from LevelManager. 

3. PlayerCollisions

Purpose: Check if there has been a collision. If so, restart the level.

Other Class References:
1. LevelManager

Methods: 
This class only uses Awake() and OnCollisionEnter(). In OnCollisionEnter DeathReload(), from LevelManager, is called.

*************************************************************************************************************

Scripts on PlayerShip:

1. ShipBehavoiurs
    
Purpose: Rotate PlayerShip about the Z axis when the player is using a control input (A or D). 
         Also, handle the firing of the lasers.

Other Class References: None

Methods:

1. Roll():
   Called in Update(). This method handles the rotations about the local Z axis. 
2. AmUpsideDown():
   Called in Roll(). This method checks to see if PlayerRig is upside down. If it is, the direction of 
   the player controlled roll direction is reversed. If this is not done, the ship rolls the wrong way 
   while upside down and looks completely unintuitive.  
3. ProcessFiring():
   Called in Update(). If the spacebar is pressed, fire the lasers. 
4. SetLasersOnOff(bool trueFalse):
   Called in ProcessFiring(). If bool trueFalse is true, enable emission from the particle systems.
   If bool trueFalse is false, disable emission.

*************************************************************************************************************
*************************************************************************************************************


