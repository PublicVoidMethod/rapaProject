using UnityEngine;
using UnityEngine.UI;

public class BotBullet : MonoBehaviour
{


    public float speed = 5;
    Vector3 dir;
    GameObject canvas;
    HPController hPController;

    // Start is called before the first frame update
    void Start()
    {
        dir = GameObject.Find("Bot").GetComponent<BotFSM>().dir;
        // dir = GameObject.Find("Bot(Clone)").GetComponent<BotFSM>().dir;

       canvas = GameObject.Find("Canvas");
       hPController = canvas.GetComponentInChildren<HPController>();

    }

    // Update is called once per frame
    void Update()
    {

        //player방향으로 총알을 발사하기
        transform.position += dir * speed * Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            //데미지 함수 호출
            hPController.PlayerGetDamaged(10);
            Destroy(gameObject);
        }
    }
}

