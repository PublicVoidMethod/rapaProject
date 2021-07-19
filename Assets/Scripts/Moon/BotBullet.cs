using UnityEngine;
using UnityEngine.UI;

public class BotBullet : MonoBehaviour
{

    public float sss = 0.5f;
    public float speed = 0;
    GameObject bot;
    Vector3 dir;
    GameObject canvas;
    HPController hpController;

    // Sdwtart is called before the first frame update
    void Start()
    {
        bot = GameObject.Find("BulletPosition");
        // Soldier76Move�� �÷��̾��� ���ǵ带 �ҷ��´�.
        speed = Soldier76Move.instance.walkSpeed * sss;

       canvas = GameObject.Find("Canvas_P");
       hpController = canvas.GetComponentInChildren<HPController>();
  dir = bot.transform.position - Soldier76Move.instance.transform.position;

      
    }

    // Update is called once per frame
    void Update()
    {

        //player�������� �Ѿ��� �߻��ϱ�
        transform.position += -dir * Time.deltaTime * speed;

    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.name);
        if (collision.gameObject.name.Contains("Player"))
        {
            //������ �Լ� ȣ��
            hpController.PlayerGetDamaged(10);

        }
       Destroy(gameObject);
    }
}

