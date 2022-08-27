using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{   
    Enemy enemyScript;

    void Start() 
    {
        enemyScript = GetComponent<Enemy>();  
    }

    void OnParticleCollision(GameObject other) 
    {
        enemyScript.DecrementHitPoints();    
    }

    
}
