using UnityEngine;

public class GlobalPositioningSystem : MonoBehaviour
{   
    float maxDistanceFromOrigin = 200;

    Vector2 xzPosition;

    void Awake() 
    {
        // make sure GPS is in the middle of the world on awake.
        transform.position = new Vector3(0,0,0);
        xzPosition = new Vector2(transform.position.x,transform.position.z);

    }

    public bool OutOfBounds(Transform userTransform)
    {   
        // get the position of the user in the xz plane. We do not care about altitude (Y)
        Vector2 userXZPosition = new Vector2(userTransform.position.x, userTransform.position.z);

        // if this condition is met, the player is not out of bounds
        if(SquareDistanceFromOrigin(userXZPosition) < maxDistanceFromOrigin*maxDistanceFromOrigin) {return false;}
        
        return true; // if we get to this line of code, the user is out of bounds

    }

    public float SquareDistanceFromOrigin(Vector2 userXZPosition)
    {   
        float sqrDistance;

        sqrDistance = Vector2.SqrMagnitude(xzPosition - userXZPosition);
        
        return sqrDistance;
    
    }



}
