using UnityEngine;

public class PlayerMover : MonoBehaviour
{   

    [SerializeField] float speed = 50f;
    [SerializeField] float controlSpeed = 1f;

    Vector3 newDirection;

    void Awake() 
    {
        newDirection = transform.eulerAngles; 
    }

    void Update() 
    {   

        MoveForward();
        
        PlayerMovementControls();

    }

    void MoveForward()
    {   
        transform.Translate(0,0,Time.deltaTime * speed);
    }


    void PlayerMovementControls()
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

}
