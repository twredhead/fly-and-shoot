using UnityEngine;

public class PlayerMover : MonoBehaviour
{   
    [SerializeField] float speed = 50f;
    [SerializeField] float controlSpeed = 0.5f;
    [SerializeField] float maxRoll = 30f;
    Vector3 bank;
    Vector3 newDirection;

    void Awake() 
    {
        newDirection = transform.eulerAngles;  
        Vector3 bank = new Vector3 (0f,0f,maxRoll);  
    }

    void Update() 
    {   
        transform.position += transform.forward * Time.deltaTime * speed;
        ControlManager();

    }

    void ControlManager()
    {
        float leftRight = Input.GetAxis("Horizontal");
        float upDown = Input.GetAxis("Vertical");
        
        Vector3 turnVec = new Vector3 (upDown, leftRight, 0f);
          
        Quaternion from = Quaternion.Euler(transform.eulerAngles);
        Quaternion to =  Quaternion.Euler(newDirection);

        // rotate From the current direction to the newDirection.
        transform.localRotation = Quaternion.RotateTowards(from,to,controlSpeed);
        
        // update newDirection every frame.
        newDirection += turnVec;

    }




}
