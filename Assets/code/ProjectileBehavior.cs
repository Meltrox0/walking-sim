using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    GameObject lilBuddy;

    void Start()
    {
        lilBuddy = GameObject.Find("lil buddy");//detects position of player
    }

    void Update()
    {
        if(Vector3.Distance(lilBuddy.transform.position, gameObject.transform.position) > 10f)//deactivates projectile if it exceeds 10 units away from player
        {
            gameObject.SetActive(false);
        }
    }
}
