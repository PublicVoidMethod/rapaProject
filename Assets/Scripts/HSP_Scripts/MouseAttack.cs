using UnityEngine;
using UnityEngine.UI;

public class MouseAttack : MonoBehaviour
{
    public GameObject spiralRocket;
    public GameObject firePosition;
    public Text magazineCntText;
    public float coolTime = 5.0f;
    public float locketSpeed = 10.0f;
    public int currentBulletCnt = 0;
    public int totalBulletCnt = 25;

    Vector3 dir;
    Vector3 crossDir;
    GameObject go;
    RaycastHit rHitInfo;

    float elapsedTime = 0;

    GameObject Bot;
    BotHPBarScript botHPScript;
    void Start()
    {

        //BotHPBar 찾기
        Bot = GameObject.Find("Bot");
        botHPScript = Bot.GetComponent<BotHPBarScript>();



        // 현재 총알에 총 탄창의 수로 초기화한다.
        currentBulletCnt = totalBulletCnt;

        // 시작할 때 탄창 수를 표시해줘야 한다.
        magazineCntText.text = currentBulletCnt.ToString() + " / " + totalBulletCnt.ToString();

        go = null;
        // 경과시간에 쿨타임을 넣고
        elapsedTime = coolTime;

        // 정면 방향을 만들고
        dir = new Vector3(0, 0, 1);

        // 메인카메라의 정면 방향으로 나아가고 싶다.
        dir = Camera.main.transform.forward;
    }

    void Update()
    {
        // 마우스 왼쪽 클릭을 했을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 현재 총알의 카운트를 하나씩 감소시킨다.
            // 텍스트의 카운트를 써준다?
            --currentBulletCnt;
            // 현재 총알이 0이 되거나 R키를 눌렀을 때 토탈 탄창의 수로 초기화 한다.
            if (currentBulletCnt <= 0)
            {
                Reload();
            }
            else
            {
                magazineCntText.text = currentBulletCnt.ToString() + " / " + totalBulletCnt.ToString();

                // 레이를 생성하고
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                // 레이가 생성되었을 때 닿은 오브젝트의 정보를 담을 변수를 생성
                RaycastHit hitInfo;

                // 레이를 발사한다.
                if (Physics.Raycast(ray, out hitInfo))
                {
                    print(hitInfo.transform.name);

                    if (hitInfo.transform.name.Contains("BotHead"))
                    {
                        botHPScript.BotGetDamaged(2);
                    }
                    else if (hitInfo.transform.name.Contains("BotBody"))
                    {
                        botHPScript.BotGetDamaged(1);
                    }
                }
            }
        }

        // 현재 총알이 총 탄창의 값과 다르고 R키를 누르면
        if (currentBulletCnt != totalBulletCnt && Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        elapsedTime += Time.deltaTime;

        // 마우스 오른쪽 클릭을 하고, 경과시간이 쿨타임의 시간보다 커진다면
        if (Input.GetMouseButtonDown(1) && elapsedTime >= coolTime)
        {
            // 레이를 생성한다.
            Ray rRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 레이에 부딪힌 오브젝트가 있다면
            if (Physics.Raycast(rRay, out rHitInfo))
            {
                print(rHitInfo.transform.name);

                // 그 오브젝트의 정보를 받은 포지션으로 방향을 만들고.
                crossDir = rHitInfo.point - firePosition.transform.position;
                crossDir.Normalize();
            }
            else
            {
                // 카메라의 정면 방향으로 이동하고 싶다.
                transform.position += dir * locketSpeed * Time.deltaTime;
            }

            // 총알 프리팹을 생성한다.
            go = Instantiate(spiralRocket);

            // 총알을 firePosition으로 회전시킨다.
            go.transform.rotation = firePosition.transform.rotation;
            // 총알의 생성 위치를 firePosition으로 위치시킨다.
            go.transform.position = firePosition.transform.position;

            // 경과시간을 초기화한다.
            elapsedTime = 0;
        }

        // 그 방향으로 움직인다.
        if (go != null)
        {
            go.transform.position += crossDir * locketSpeed * Time.deltaTime;
        }
    }

    void Reload()
    {
        currentBulletCnt = totalBulletCnt;
        magazineCntText.text = currentBulletCnt.ToString() + " / " + totalBulletCnt.ToString();
    }
}
