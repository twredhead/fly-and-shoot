using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{   
    /******************************************************* Required Scripts *********************************************************/
    GlobalPositioningSystem gps;
    LevelManager lvlManager;

    /******************************************************* Serialized Fields ********************************************************/

    [SerializeField] float maxSpeed = 45f;
    [SerializeField] float minSpeed = 25f;

    // probability of being an oscillating enemy is approx P = 1 - oscillatorChance
    [SerializeField] float oscillatorChance = 0.7f;
    
    /******************************************************* Properties ***************************************************************/
    float oscillationMagnitude = 0.2f;
    public float OscillationMagnitude { get { return oscillationMagnitude; } }

    float speed; 
    public float Speed { get { return speed; } }

    bool isOscillator; 
    public bool IsOscillator { get { return isOscillator; } }

    /******************************************************* Private Fields ***********************************************************/

    // the range of xy positions that the enemies can spawn in
    float [] xRange = {-700,700};
    float [] yRange = {250, 500};

    float randomNumber;
    float waitTime = 2f;


    /******************************************************************************************************************************/
    /******************************************************* Awake() **************************************************************/
    /******************************************************************************************************************************/

    void Awake()
    {   
        InitializeParameters();
    }

    void InitializeParameters()
    {
        
        // set up initial position of enemy
        float zPos = transform.parent.position.z;
        float xPos = Random.Range(xRange[0],xRange[1]);
        float yPos = Random.Range(yRange[0],yRange[1]);

        Vector3 initialPosition = new Vector3(xPos,yPos,zPos);

        transform.position = initialPosition;

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
        yield return new WaitForSeconds(waitTime);

        InitializeParameters(); // reinitialize the parameters of the enemy s.t. the enemy types stay random.
        
        gameObject.SetActive(false);    

        // todo: consider adding some FX so that it doesn't just look like the enemy vanished.
        // Maybe some wormhole looking thing.
    }



     
}
