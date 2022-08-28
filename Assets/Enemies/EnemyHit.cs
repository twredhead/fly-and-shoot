using UnityEngine;

[RequireComponent(typeof(Enemy))]
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
