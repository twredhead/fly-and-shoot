using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMover : MonoBehaviour
{   
    [SerializeField] float maxSpeed = 45f;
    [SerializeField] float minSpeed = 25f;
    [SerializeField] float oscillationMagnitude = 0.2f;

    // oscillator enemies should happen approximately P = 1-oscillatorChance
    [SerializeField] float oscillatorChance = 0.7f;
    
    // speed makes the enemy harder to kill, so the score should be higher with a higher speed.
    // This needs to be getable, but not changable. I am choosing to make the speed a property
    // to increase it's visibility.
    [SerializeField] float speed; 
    public float Speed { get { return speed; } }

    // similar story with oscillator. If the enemy oscillates, I want to increase the score value
    // in another script.
    [SerializeField] bool isOscillator; 
    public bool IsOscillator { get { return isOscillator; } }

    float randomNumber;
    float pi = Mathf.PI;

    
    /*****************************************************************************************************/
    /*************************************  Awake() Methods   ********************************************/
    /*****************************************************************************************************/

    void Awake()
    {   
        
        // assign a random speed between min and max speeds
        speed = Random.Range(minSpeed,maxSpeed);
        
        randomNumber = Random.Range(0f,1f); // used to determine if enemy is oscillator or not.

        isOscillator = ShouldOscillate(randomNumber);

    }

    bool ShouldOscillate(float number)
    {   
    
        if(number <  oscillatorChance){ return false;}

        return true;
    }


    /*****************************************************************************************************/
    /*************************************  Update() Methods   *******************************************/
    /*****************************************************************************************************/

    void Update()
    {
        Move();

    }

    void Move()
    {   
        float sineWave = SineWave(isOscillator);

        transform.Translate(0, sineWave, speed * Time.deltaTime);
    }


    float SineWave(bool isActive)
    {
        if(isActive == false) { return 0;}
        
        float sineWave = Mathf.Sin(pi*Time.time) * oscillationMagnitude;

        return sineWave;
    }

    


}
