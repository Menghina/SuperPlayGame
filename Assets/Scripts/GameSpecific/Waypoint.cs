using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Transform Destination;

    private void OnTriggerEnter(Collider col)
    {
        //calls the method SetDestination(Destination) for the object;
        //when enemy reaches spawnpoint x-1 is redirected to spawnpointx;
        //col.SendMessage("SetDestination", Destination);
        //partea a doua de schimbat daca vrau sa scot motorul
        col.GetComponent<AIMotor>().SetDestination(Destination);
    }
}
