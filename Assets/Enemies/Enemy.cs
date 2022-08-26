using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{   
    GlobalPositioningSystem gps;
    LevelManager lvlManager;

    void Start()
    {   
        gps = FindObjectOfType<GlobalPositioningSystem>();
        lvlManager = FindObjectOfType<LevelManager>();
    }   

    void Update() 
    {
        AmInGameArea();
    }

    void AmInGameArea()
    {
        if(gps.OutOfBounds(transform) == false){ return; }

        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {   
        // wait 5 seconds then reset the position to that of the parent.
        yield return new WaitForSeconds(5f);
    
        transform.position = transform.parent.position;
        
        gameObject.SetActive(false);    
    }



     
}
