using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{   
    /******************************************************* Required Scripts *********************************************************/
    GlobalPositioningSystem gps;
    LevelManager lvlManager;
    UIManager uimanager;

    /******************************************************* Serialized Fields ********************************************************/

    [SerializeField] float maxSpeed = 45f;
    [SerializeField] float minSpeed = 20f;

    // probability of being an oscillating enemy is approx P = 1 - oscillatorChance
    [SerializeField] float oscillatorChance = 0.7f;

    [SerializeField] float oscillationPeriod = 4; // needs play testing
    [SerializeField] int baseScoreValue = 100;
    [SerializeField] int stealAmount = 10;
    
    /******************************************************* Properties ***************************************************************/
    
    float oscillationFrequency; // calculated from oscillator period in Awake()
    public float OscillationFrequency { get { return oscillationFrequency; } }

    float oscillationMagnitude = 0.05f;
    public float OscillationMagnitude { get { return oscillationMagnitude; } }

    float speed; // set when enemy spawns
    public float Speed { get { return speed; } }

    bool isOscillator; // set when enemy spawns
    public bool IsOscillator { get { return isOscillator; } }

    /******************************************************* Private Fields ***********************************************************/
    Rigidbody rb;
    // the range of xy positions that the enemies can spawn in
    float [] xRange = {-400,400};
    float [] yRange = {250, 450};

    float randomNumber;
    int setHitPoints = 6;

    int scoreValue;

    int hitPoints = 6; // reset when enemy spawns
    public void DecrementHitPoints()
    {   
        hitPoints--;
    }

    /******************************************************************************************************************************/
    /******************************************************* Awake() **************************************************************/
    /******************************************************************************************************************************/

    void Awake()
    {   
        // this is set only once
        oscillationFrequency = (2*Mathf.PI/oscillationPeriod);

        InitializeEnemy();
        AddRigidbody();
        uimanager = FindObjectOfType<UIManager>();
        
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void InitializeEnemy()
    {
        SetPosition();

        SetParameters();

        AssignScoreValue();
    }
    void SetPosition()
    {
        // set up initial position of enemy
        float zPos = transform.parent.position.z;
        float xPos = Random.Range(xRange[0], xRange[1]);
        float yPos = Random.Range(yRange[0], yRange[1]);

        Vector3 initialPosition = new Vector3(xPos, yPos, zPos);

        transform.position = initialPosition;
    }

    void SetParameters()
    {
        hitPoints = setHitPoints;

        // assign a random speed between min and max speeds
        speed = Random.Range(minSpeed, maxSpeed);

        randomNumber = Random.Range(0f, 1f); // used to determine if enemy is oscillator or not.

        isOscillator = ShouldOscillate(randomNumber);
    }

    bool ShouldOscillate(float number)
    {   
        // determines of the enemy is an oscillating enemy
        if(number <  oscillatorChance){ return false;}

        return true;
    }  

    void AssignScoreValue()
    {   
        scoreValue = baseScoreValue;
        
        if(speed > 30) { scoreValue += 50; }

        if(isOscillator) { scoreValue += 300; }
  
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
        ShouldIDie();
    }

    void AmInGameArea()
    {   
        // test if the enemy is in the game area. If not, deactivate the enemy 
        // so that it can be recycled in the pool.
        if(gps.OutOfBounds(transform) == false){ return; }

        StartCoroutine(Deactivate(false));
    }

    void ShouldIDie()
    {
        if(hitPoints > 0){ return; }

        StartCoroutine(Deactivate(true));

    }

    IEnumerator Deactivate(bool fromDeath)
    {   

        if(fromDeath) 
        {
             
            float waitTime = 0.5f;      

            yield return new WaitForSeconds(waitTime);

            lvlManager.UpdateScore(scoreValue);

            InitializeEnemy();
            
            gameObject.SetActive(false); 

        }   
        else
        {
            float waitTime = 2f;      

            // wait 5 seconds then reset the position to that of the parent.
            yield return new WaitForSeconds(waitTime);

            lvlManager.UpdateScore(-stealAmount); // if the enemy deactivates through going out of bounds
                                                // the player is penalised.

            InitializeEnemy(); // reinitialize the parameters of the enemy s.t. the enemy types stay random.
            
            gameObject.SetActive(false); 
        }

        // todo: consider adding some FX so that it doesn't just look like the enemy vanished.
        // Maybe some wormhole looking thing.
    }


}
