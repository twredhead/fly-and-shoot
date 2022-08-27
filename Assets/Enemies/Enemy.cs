using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{   

    GlobalPositioningSystem gps;
    LevelManager lvlManager;

    [SerializeField] float maxSpeed = 45f;
    [SerializeField] float minSpeed = 25f;
    
    [SerializeField] float oscillationMagnitude = 0.2f;
    public float OscillationMagnitude { get { return oscillationMagnitude; } }

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


    /******************************************************************************************************************************/
    /******************************************************* Awake() **************************************************************/
    /******************************************************************************************************************************/

    void Awake()
    {   
        InitializeParameters();
    }

    void InitializeParameters()
    {
        // assign a random speed between min and max speeds
        speed = Random.Range(minSpeed,maxSpeed);
        
        randomNumber = Random.Range(0f,1f); // used to determine if enemy is oscillator or not.

        isOscillator = ShouldOscillate(randomNumber);
    }

    bool ShouldOscillate(float number)
    {   
        // determines of the enemy is an oscillating enemy
        if(number <  oscillatorChance){ return false;}

        return true;
    }    


    /******************************************************************************************************************************/
    /******************************************************* Start() & Update() ***************************************************/
    /******************************************************************************************************************************/

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
        // test if the enemy is in the game area. If not, deactivate the enemy 
        // so that it can be recycled in the pool.
        if(gps.OutOfBounds(transform) == false){ return; }

        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {   
        // wait 5 seconds then reset the position to that of the parent.
        yield return new WaitForSeconds(5f);
    
        transform.position = transform.parent.position;

        InitializeParameters(); // reinitialize the parameters of the enemy s.t. the enemy types stay random.
        
        gameObject.SetActive(false);    

        // todo: consider adding some FX so that it doesn't just look like the enemy vanished.
        // Maybe some wormhole looking thing.
    }



     
}
