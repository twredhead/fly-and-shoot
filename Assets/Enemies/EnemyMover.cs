using UnityEngine;

[RequireComponent(typeof(Enemy))] // this script depends on Enemy being a componenet.
public class EnemyMover : MonoBehaviour
{   
    Enemy enemyScript;

    void Start()
    {   
        
        enemyScript = GetComponent<Enemy>();

    }

    void Update()
    {

        Move();

    }

    void Move()
    {   
    
        float sineWave = SineWave(enemyScript.IsOscillator);
        
        transform.Translate(0, sineWave, enemyScript.Speed * Time.deltaTime);
    }


    float SineWave(bool isActive)
    {
        if(isActive == false) { return 0;}
        
        float sineWave = Mathf.Sin(enemyScript.OscillationFrequency * Time.time) * enemyScript.OscillationMagnitude;

        return sineWave;
    }

    


}
