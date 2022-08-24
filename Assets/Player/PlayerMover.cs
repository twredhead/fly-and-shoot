using UnityEngine;

public class PlayerMover : MonoBehaviour
{   
    [SerializeField] float speed = 50f;
    [SerializeField] float controlSpeed = 1f;
    [SerializeField] float maxDistance = 900; // for keeping player in world

    Vector3 newDirection;

    Vector3 originAtCurrentAltitude;

    // navigation aids
    Transform navAid;

    Vector2 gpsLocationXZ;

    Vector2 playerLocationXZ;

    float sqrDistanceFromOrigion;

    void Awake() 
    {
        newDirection = transform.eulerAngles; 
        navAid = FindObjectOfType<GlobalPositioningSystem>().transform; 
        gpsLocationXZ = new Vector2(navAid.position.x,navAid.position.z);
    }

    void Update() 
    {   

        MoveForward();
        
        ControlManager();

    }

    void MoveForward()
    {   
        // todo: Fix this so that the object does not rotate back to its original forward.
        // I though that have this local variable changed inside the if statement would fix the problem.
        // It did not.
        Vector3 forward = transform.forward;

        if(OutOfBounds())
        {   // vector at current altitude but point toward the xz origin. (0,position.y,0)         
            originAtCurrentAltitude = new Vector3(navAid.position.x,transform.position.y,navAid.position.z);
            transform.LookAt(originAtCurrentAltitude);
            
            forward = transform.forward;
            
            transform.position += forward*150; // jump a long way from the edge of the world.

        } 

        transform.position += forward * Time.deltaTime * speed;
    }


    void ControlManager()
    {
        float leftRight = Input.GetAxis("Horizontal");
        float upDown = Input.GetAxis("Vertical");
        
        Vector3 turnVec = new Vector3 (upDown, leftRight, 0f);
          
        Quaternion from = Quaternion.Euler(transform.eulerAngles);
        Quaternion to =  Quaternion.Euler(newDirection);

        // rotate From the current direction to the newDirection.
        transform.localRotation = Quaternion.RotateTowards(from, to, controlSpeed);
        
        // update newDirection every frame.
        newDirection += turnVec;

    }

    bool OutOfBounds()
    {   
        // if this condition is met, the player is not out of bounds
        if(SquareDistanceFromOrigin() < maxDistance*maxDistance) {return false;}
        
        return true; // if we get to this line of code, the player is out of bounds

    }

    float SquareDistanceFromOrigin()
    {   
        // this code allows the gps location to be changed from the origin. Otherwise you could just take the square 
        // of the x,z position without worrying about where anthing else is. Currently that is not 
        // something that is going to happen however.
        playerLocationXZ = new Vector2(transform.position.x, transform.position.z);

        sqrDistanceFromOrigion = Vector2.SqrMagnitude(playerLocationXZ - gpsLocationXZ);

        return sqrDistanceFromOrigion;

    }

}
