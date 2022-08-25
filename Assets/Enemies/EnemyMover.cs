using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{   
    [SerializeField] float speed = 40f;


    void Awake()
    {

    }

    void Update()
    {
        transform.Translate(0, 0, speed*Time.deltaTime);
    }





}
