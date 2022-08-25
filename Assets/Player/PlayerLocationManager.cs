using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocationManager : MonoBehaviour
{
    GlobalPositioningSystem gps;
    UIManager uiManager;
    LevelManager lvlManager;
        
    bool outOfBounds;

    float maxTime = 6f;   

    float time;

    void Awake() 
    {
        gps = FindObjectOfType<GlobalPositioningSystem>();
        uiManager = FindObjectOfType<UIManager>();
        lvlManager = FindObjectOfType<LevelManager>();
        
        // time is decremented using a timer
        time = maxTime;
           
    }

    void Update() 
    {
        
        OutOfBoundsManager();

    }  

    void OutOfBoundsManager()
    {

        outOfBounds = gps.OutOfBounds(transform); // OutOfBounds returns true if player is out of bounds

        if(!outOfBounds) { uiManager.WarnPlayer(false, time, maxTime); time = maxTime; return; }

        uiManager.WarnPlayer(true, time, maxTime);

        time = lvlManager.Timer(time);

        if(time < 0)
        {
            lvlManager.OutOfBoundsReload();
        }


    }


        

}
