using UnityEngine;
using UnityEngine.UI;

public class BotBullet : MonoBehaviour
{

    public float sss = 2f;
    public float speed = 0;
    Vector3 dir;
    GameObject canvas;
    HPController hpController;

    // Sdwtart is called before the first frame update
    void Start()
    {
        // Soldier76Move의 플레이어의 스피드를 불러온다.
        speed = Soldier76Move.instance.walkSpeed * sss;

        dir = GameObject.Find("Bot(Clone)").GetComponent<BotFSM>().dir;
        // dir = GameObject.Find("Bot(Clone)").GetComponent<BotFSM>().dir;

       canvas = GameObject.Find("Canvas_P");
       hpController = canvas.GetComponentInChildren<HPController>();

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
            hpController.PlayerGetDamaged(10);

        }
        Destroy(gameObject);
    }
}

