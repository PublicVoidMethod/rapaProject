using System;
using System.Collections;
using System.Collections.Generic;
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
    float leftDelay = 0.3f;
    bool isLeftShoot = true;
    bool isReload = true;
    float maxDistance = 50.0f;

    GameObject Bot;
    BotHPBarScript botHPScript;
    void Start()
    {
        //BotHPBar 찾기
        Bot = GameObject.Find("Bot");
        //botHPScript = Bot.GetComponent<BotHPBarScript>();

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

        //maxDistance = 100f;
    }

    void Update()
    {
        // 현재의 총알이 음수값으로 내려가지 않도록 해준다.
        currentBulletCnt = Mathf.Clamp(currentBulletCnt, 0, totalBulletCnt);

        // 마우스 왼쪽 클릭을 했을 때
        if (Input.GetMouseButton(0))
        {
            // 현재 총알의 카운트를 하나씩 감소시킨다.
            // 텍스트의 카운트를 써준다?
            // 현재 총알이 0이 되고 장전을 할 때 토탈 탄창의 수로 초기화 한다.
            if (isReload)
            {
                if(currentBulletCnt <= 0)
                {
                    // 레이 생성을 멈추고???????????????
                    // 재장전 함수를 1.5초 뒤에 실행한다.
                    Invoke("Reload", 1.5f);
                    isReload = false;
                }
                else if(isLeftShoot)
                {
                    // 레이를 생성하고
                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                    // 좌클릭 딜레이 코루틴 실행
                    StartCoroutine(LeftShootDelay());
                    isLeftShoot = false;
                    
                    // 레이가 생성되었을 때 닿은 오브젝트의 정보를 담을 변수를 생성
                    RaycastHit hitInfo;

                    bool isHit= Physics.Raycast(ray, out hitInfo, 100f);

                    //Debug.Log(hitInfo.na);

                    --currentBulletCnt;

                    magazineCntText.text = currentBulletCnt.ToString() + " / " + totalBulletCnt.ToString();
                    // 레이를 발사한다.
                    if (isHit)
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
            #region 장황한 재장전
            /*
                        if (currentBulletCnt <= 0 && isReload)
                        {
                            // 레이 생성을 멈추고???????????????
                            // 재장전 함수를 1,5초 뒤에 실행한다.
                            Invoke("Reload", 1.5f);
                            isReload = false;
                        }
                        else if(isReload)
                        {
                            // 레이를 생성하고
                            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                            // 레이가 생성되었을 때 닿은 오브젝트의 정보를 담을 변수를 생성
                            RaycastHit hitInfo;

                            // 레이를 발사한다.
                            if (Physics.Raycast(ray, out hitInfo) && isLeftShoot)
                            {
                                --currentBulletCnt;
                                print(hitInfo.transform.name);
                                magazineCntText.text = currentBulletCnt.ToString() + " / " + totalBulletCnt.ToString();


                                // 좌클릭 딜레이 코루틴 실행
                                StartCoroutine(LeftShootDelay());

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
            */
            #endregion
        }

        // 현재 총알이 총 탄창의 값과 다르고 R키를 누르면
        if (currentBulletCnt != totalBulletCnt && Input.GetKeyDown(KeyCode.R) && isReload)
        {
            // 레이 생성을 멈추고???????????????
            // 재장전 함수를 1,5초 뒤에 실행한다.
            Invoke("Reload", 1.5f);
            isReload = false;
        }

        elapsedTime += Time.deltaTime;

        // 마우스 오른쪽 클릭을 하고, 경과시간이 쿨타임의 시간보다 커진다면
        if (Input.GetMouseButtonDown(1) && elapsedTime >= coolTime)
        {
            //총알 프리팹을 생성한다.
            //go = Instantiate(spiralRocket);
            // 레이를 생성한다.
            Ray rRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 레이에 부딪힌 오브젝트가 있다면
            if (Physics.Raycast(rRay, out rHitInfo, maxDistance))
            {
                print(rHitInfo.transform.name);
                //rRay.GetPoint(50)
                // 그 오브젝트의 정보를 받은 포지션으로 방향을 만들고.
                //crossDir = rHitInfo.point - firePosition.transform.position;
                //crossDir.Normalize();
                StartCoroutine(AirShootRocketCoroutine(rHitInfo.point));
            }

            // 레이가 정보를 가져오지 못하면
            //else if(rHitInfo.distance == maxDistance)
            else
            {
                #region 쓸모없는 코드
                // 방향을 레이의 끝지점에서 파이어포지션을 뺀 방향을 만들고
                //crossDir = new Vector3(0, 0, maxDistance) - firePosition.transform.position;
                //crossDir.Normalize();
                //print(crossDir);
                //// 카메라의 정면 방향으로 이동하고 싶다.
                //transform.position += dir * locketSpeed * Time.deltaTime;

                //// 그 방향으로 이동한다.
                //transform.position += crossDir * locketSpeed * Time.deltaTime;
                #endregion
                print(rRay.GetPoint(maxDistance));
                StartCoroutine(AirShootRocketCoroutine(rRay.GetPoint(maxDistance)));
            }

            // 경과시간을 초기화한다.
            elapsedTime = 0;

            //    // 총알을 firePosition으로 회전시킨다.
            //    go.transform.rotation = firePosition.transform.rotation;
            //    // 총알의 생성 위치를 firePosition으로 위치시킨다.
            //    go.transform.position = firePosition.transform.position;

            //}

            //// 그 방향으로 움직인다.
            //if (go != null)
            //{
            //    go.transform.position += crossDir * locketSpeed * Time.deltaTime;
        }
    }

    void Reload()
    {
        currentBulletCnt = totalBulletCnt;
        magazineCntText.text = currentBulletCnt.ToString() + " / " + totalBulletCnt.ToString();
        isReload = true;
    }

    IEnumerator LeftShootDelay()
    {
        yield return new WaitForSeconds(leftDelay);
        isLeftShoot = true;
    }

   
    IEnumerator AirShootRocketCoroutine(Vector3 maxPos)
    {
        GameObject go = Instantiate(spiralRocket, firePosition.transform.position, Quaternion.identity);
       
        float curTime = 0;
        // 이 코루틴에 온 목적이 뭘까? - 로켓의 위치를 조금씩 앞으로 움직여서 맥스 거리까지 가도록 만든다.
        // 로켓의 최초 위치를 잡아줘야함
        while(curTime < 1.0f)
        {
            //Vector3 myPos = Vector3.Lerp(firePosition.transform.position, maxPos, curTime);
            if (Vector3.Distance(maxPos, go.transform.position) > 1)
            {
                Vector3 dir = (maxPos - firePosition.transform.position).normalized;
                go.transform.position += dir * 20 * Time.deltaTime;
            }
            else
            {
                go.transform.position = maxPos;
                // 맥스거리에 도달했을 때 총알을 파괴시킨다.
                Destroy(go);
                yield break;
            }
            // 시간을 누적을 시켜서 그 누적된 시간만큼 로켓을 더 앞에 생성한다.
            //curTime += Time.deltaTime * 0.1f;
            yield return null;
        }
        //DestroyImmediate(spiralRocket);
    }
}
