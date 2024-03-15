using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    public float speed = 0.35f;
    public int score;

    private Vector2 destination;

    private void Start()
    {
        destination = transform.position;
        score = 0;
    }

    private void FixedUpdate()
    {
        Vector2 temp = Vector2.MoveTowards(transform.position, destination, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);


        //必须等移动到目的地后才能设置下一次移动的坐标
        if ((Vector2)transform.position == destination)
        {
            //通过监听键盘来设定下一次移动的目的地
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (NotWall(Vector2.up)))
            {
                destination = (Vector2)transform.position + Vector2.up;
            }
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (NotWall(Vector2.left)))
            {
                destination = (Vector2)transform.position + Vector2.left;
            }
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (NotWall(Vector2.down)))
            {
                destination = (Vector2)transform.position + Vector2.down;
            }
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (NotWall(Vector2.right)))
            {
                destination = (Vector2)transform.position + Vector2.right;
            }

            Vector2 dir = destination - (Vector2)transform.position;
            GetComponent<Animator>().SetFloat("DirX",dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }

        bool NotWall(Vector2 dir)
        {
            Vector2 position = transform.position;
            RaycastHit2D hit = Physics2D.Linecast(position + dir, position);
            return (hit.collider.tag != "Wall");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PacDot")
        {
            score++;
            Debug.Log("分数：" + score);
            collision.gameObject.SetActive(false);

            if (score == 328)
            {
                Debug.Log("胜利！");
            }

        }
    }
}
