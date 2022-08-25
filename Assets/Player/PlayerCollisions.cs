using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{   
    LevelManager lvlManager;

    void Awake()
    {
        lvlManager = FindObjectOfType<LevelManager>();
    }
    
    void OnCollisionEnter(Collision other)
    {
        // when a collision happens the player dies and has to restart the level.
        lvlManager.DeathReload();

        // To Do: Add some explosion FX and stop the player motion
    }

}
