using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Range(0.1f, 1f)] public float dwellTime;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
    }

    //IEnumerator FollowPath()
    //{
    //    print("Starting patrol...");
    //    foreach (Waypoint waypoint in path)
    //    {
    //        transform.position = waypoint.transform.position;
    //        print("Visiting: " + waypoint);
    //        yield return new WaitForSeconds(dwellTime);
    //    }
    //    print("Ending patrol");
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
