
Fly and Shoot

*************************************************************************************************************
*************************************************************************************************************

Fly and Shoot is simply game where you fly around a small world and shoot enemies. I was inspired by 
Argon Assault, by GameDev.tv, which is a rail shooter. I thought it would be fun to take Argon Assault of the 
rail as I do not really like that genre of games. I was going to refactor a bunch of Argon Assault code, but 
instead I pretty much completely rewrote it. The shooting mechanics are mostly identical but that is where 
the similarities end.

This document provides some details explaining how the scripts work together.

*************************************************************************************************************
*************************************************************************************************************
*************************************************************************************************************

Scripts with Public Methods:

I noticed, when I was writing an enemy mover script, that I was rewriting code that provided a crude 
coordinate(simply the distance from the center of the world). So, instead of having the same code in a few 
different places, I decided to make a few scripts with public methods.

GlobalPositioningSystem (GPS):
GPS is attached to a empty object, and it sets the empty object to be at the origin on awake.

Public Methods:
1. bool OutOfBounds(Transform userTransform) returns true if the user of the method has gone out of the play 
   area.
2. float SqrDistanceFromOrigin returns the square distance in the xz plane from the center of the world. 

The idea behind these two methods was to create a fish bowl effect for the player. I wanted the player to be 
forced to turn around when they got close to the edge of the game world. To do this, I needed to calculated 
where the player was. 

*************************************************************************************************************

UIManager:
The purpose of UIManager was to have a script that was attached to the UICanvas so that in game events could
be displayed on the UI.

Public Methods:
1. void WarnPlayer(bool trueOrFalse, float time, float maxTime) tells the UI to display a warning to the 
   player with a count down. This code does nothing but updates the UI every frame that it is called.
   The idea behind this method was that the player needs to be warned that they are leaving the play
   area. The consequences of not heeding the warning are taken care of in another script.
   If trueOrFalse is true warning text will be displayed. Else, not. maxTime should be constant, and 
   time should be decrimented by some value each frame. However, if you wanted to count up it could be 
   incremented. However, the logic in a private method called UpdateWarning() in UIManager would need to
   be changed to accomodate this. 
2. ToDo: There should be a public method that updates the score. Every time you destroy an enemy the score 
   should increase.

*************************************************************************************************************


LevelManager:
LevelManager provides some public methods on how to reload the game. It also provides a versatile timer.

1. void DeathReload() should be called when the player has died. I call it in a collision script attached to 
   the player object.
2. void OutOfBoundsReload should be called when the player has been out of bounds for too long.
3. float Timer(float time) is just a nice timer. It returns (time - Time.deltaTime) each time it is called.


*************************************************************************************************************
*************************************************************************************************************
*************************************************************************************************************

Player Scripts:

The player object is made up of an empty parent object (PlayerRig) with another empty object as a child
called Ship. Ship has a capsul and a streched cube inside as children. 
The camera is also childed to PlayerRig so that the camera moves with the player.

Scripts on PlayerRig:

1. PlayerMover:

   PlayerMover handles the in world motion of the player.
   1. void MoveForward(): This method locks the player into moving forward at all times. 
      This method makes use of transform.Translate() to move the player forward each time it is called. 
      It is called in Update such that the player moves forward each frame.
   2. void PlayerMoevementControls(): This method allows the player to change the direction they are facing.
      It does this with ADWS. WS control rotations about the x axis and AD control rotations about the y
      axis. The rotations are done at the same time using Quaternion.RotateTowards(). There are no rations
      about the z axis here. That is controlled in another script as it has nothing to do with changing 
      direction. 

2.PlayerCollisions:

  PlayerCollisions handles collisions. If the player collides with anything the level is reloaded. This
  Script calls LevelManager.DeathReload in OnCollisionEnter(). Things to consider adding to this script
  could be some death FX, sound and visual. However, I think I will move onto another project before 
  I add cosmetics as FX are beyond the intended scope of the project.

3. PlayerLocationManager:
   
   This script calculates where the player is in relation to the center of the world by making use of the
   public methods from GlobalPositioningSystem.
   1. void OutOfBoundsManager() calls GPS methods to check if the player is out of bounds. If so, it calls
      methods from UIManager to update the UI to warn the player. It also calls Timer from LevelManager in
      order to give the player a countdown. If the countdown reaches zero, LevelManager.OutOfBoundsReload()
      is called.

Scripts on Ship:

1. ShipBehaviour:

   ShipBehaviour is for cosmetic things, and also for firing the lasers (todo). Currently, all it does
   is rotate the body and wings about the z axis when A and D are pressed. This is to add a visual 
   effect of banking. It makes it look kind of cool. Lasers will be added later. This script 
   also checks if the ship is upside down. If it is, it reverses the direction that the ship rotates
   about the z axis as it looks silly otherwise. 
