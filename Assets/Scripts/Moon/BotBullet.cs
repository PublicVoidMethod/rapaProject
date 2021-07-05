using UnityEngine;

public class BotBullet : MonoBehaviour
{


    public float speed = 5;
    Vector3 dir;
    
    // Start is called before the first frame update
    void Start()
    {
        dir = GameObject.Find("Bot").GetComponent<BotFSM>().dir;
    }

    // Update is called once per frame
    void Update()
    {

        //player방향으로 총알을 발사하기
        transform.position += dir * speed * Time.deltaTime;

    }
}

