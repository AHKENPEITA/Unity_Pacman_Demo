using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public float speed = 0.2f;
    public Transform[] Waypoints;
    public int CurrentWaypoint;


    private void FixedUpdate()
    {
        if (transform.position==Waypoints[CurrentWaypoint].position)
        {
            CurrentWaypoint=(CurrentWaypoint+1)%Waypoints.Length;
        }
        else
        {
            Vector2 temp = Vector2.MoveTowards(transform.position, Waypoints[CurrentWaypoint].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(temp);
        }
        Vector2 dir = (Vector2)Waypoints[CurrentWaypoint].position - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}
