using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviours : MonoBehaviour
{
    [SerializeField] float maxRoll = 30f;
    [SerializeField] float rollSpeed = 5f;

    Quaternion from;
    Quaternion to; 

    Transform parent;

    void Awake() 
    {
        parent = transform.parent;
    }

    void Update() 
    {   
        
        Roll();    
        
    }


    void Roll()
    {   
        bool upsideDown = AmUpsideDown();
        float roll = maxRoll;

        // necessary to check if the ship is upside down as ship will roll in counterintuitive way
        // otherwise. Switch sign of roll to compensate for being upside down.
        if(upsideDown == true)
        {
            roll = -maxRoll;
        }
        else
        {
            roll = Mathf.Abs(maxRoll); 
        }

        if(Input.GetKey(KeyCode.A))
        {   
            Quaternion from = transform.localRotation;
            Quaternion to = Quaternion.Euler(0f,0f,roll);
            transform.localRotation = Quaternion.RotateTowards(from, to, rollSpeed);
        }

        else if(Input.GetKey(KeyCode.D))
        {
            Quaternion from = transform.localRotation;
            Quaternion to = Quaternion.Euler(0f,0f,-roll);
            transform.localRotation = Quaternion.RotateTowards(from, to, rollSpeed);
        }
        else
        {
            Quaternion from = transform.localRotation;
            Quaternion to = Quaternion.Euler(0f,0f,0f);
            transform.localRotation = Quaternion.RotateTowards(from, to, rollSpeed);
        }
                
    }

    bool AmUpsideDown()
    {   // for checking if the player rig is upside down.

        float angle = parent.eulerAngles.z;

        // check if the z angle is between 90 and -90
        if(Mathf.Abs(angle) > 90)
        {   
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
