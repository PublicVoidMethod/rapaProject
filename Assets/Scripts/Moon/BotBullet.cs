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
        // Soldier76Move�� �÷��̾��� ���ǵ带 �ҷ��´�.
        speed = Soldier76Move.instance.walkSpeed * sss;

        dir = GameObject.Find("Bot(Clone)").GetComponent<BotFSM>().dir;
        // dir = GameObject.Find("Bot(Clone)").GetComponent<BotFSM>().dir;

       canvas = GameObject.Find("Canvas_P");
       hpController = canvas.GetComponentInChildren<HPController>();

    }

    // Update is called once per frame
    void Update()
    {

        //player�������� �Ѿ��� �߻��ϱ�
        transform.position += dir * speed * Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            //������ �Լ� ȣ��
            hpController.PlayerGetDamaged(10);

        }
        Destroy(gameObject);
    }
}

