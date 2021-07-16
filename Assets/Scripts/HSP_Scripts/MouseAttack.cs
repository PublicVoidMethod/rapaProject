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
        //BotHPBar ã��
        Bot = GameObject.Find("Bot");
        //botHPScript = Bot.GetComponent<BotHPBarScript>();

        // ���� �Ѿ˿� �� źâ�� ���� �ʱ�ȭ�Ѵ�.
        currentBulletCnt = totalBulletCnt;

        // ������ �� źâ ���� ǥ������� �Ѵ�.
        magazineCntText.text = currentBulletCnt.ToString() + " / " + totalBulletCnt.ToString();

        go = null;
        // ����ð��� ��Ÿ���� �ְ�
        elapsedTime = coolTime;

        // ���� ������ �����
        dir = new Vector3(0, 0, 1);

        // ����ī�޶��� ���� �������� ���ư��� �ʹ�.
        dir = Camera.main.transform.forward;

        //maxDistance = 100f;
    }

    void Update()
    {
        // ������ �Ѿ��� ���������� �������� �ʵ��� ���ش�.
        currentBulletCnt = Mathf.Clamp(currentBulletCnt, 0, totalBulletCnt);

        // ���콺 ���� Ŭ���� ���� ��
        if (Input.GetMouseButton(0))
        {
            // ���� �Ѿ��� ī��Ʈ�� �ϳ��� ���ҽ�Ų��.
            // �ؽ�Ʈ�� ī��Ʈ�� ���ش�?
            // ���� �Ѿ��� 0�� �ǰ� ������ �� �� ��Ż źâ�� ���� �ʱ�ȭ �Ѵ�.
            if (isReload)
            {
                if(currentBulletCnt <= 0)
                {
                    // ���� ������ ���߰�???????????????
                    // ������ �Լ��� 1.5�� �ڿ� �����Ѵ�.
                    Invoke("Reload", 1.5f);
                    isReload = false;
                }
                else if(isLeftShoot)
                {
                    // ���̸� �����ϰ�
                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                    // ��Ŭ�� ������ �ڷ�ƾ ����
                    StartCoroutine(LeftShootDelay());
                    isLeftShoot = false;
                    
                    // ���̰� �����Ǿ��� �� ���� ������Ʈ�� ������ ���� ������ ����
                    RaycastHit hitInfo;

                    bool isHit= Physics.Raycast(ray, out hitInfo, 100f);

                    //Debug.Log(hitInfo.na);

                    --currentBulletCnt;

                    magazineCntText.text = currentBulletCnt.ToString() + " / " + totalBulletCnt.ToString();
                    // ���̸� �߻��Ѵ�.
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
            #region ��Ȳ�� ������
            /*
                        if (currentBulletCnt <= 0 && isReload)
                        {
                            // ���� ������ ���߰�???????????????
                            // ������ �Լ��� 1,5�� �ڿ� �����Ѵ�.
                            Invoke("Reload", 1.5f);
                            isReload = false;
                        }
                        else if(isReload)
                        {
                            // ���̸� �����ϰ�
                            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                            // ���̰� �����Ǿ��� �� ���� ������Ʈ�� ������ ���� ������ ����
                            RaycastHit hitInfo;

                            // ���̸� �߻��Ѵ�.
                            if (Physics.Raycast(ray, out hitInfo) && isLeftShoot)
                            {
                                --currentBulletCnt;
                                print(hitInfo.transform.name);
                                magazineCntText.text = currentBulletCnt.ToString() + " / " + totalBulletCnt.ToString();


                                // ��Ŭ�� ������ �ڷ�ƾ ����
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

        // ���� �Ѿ��� �� źâ�� ���� �ٸ��� RŰ�� ������
        if (currentBulletCnt != totalBulletCnt && Input.GetKeyDown(KeyCode.R) && isReload)
        {
            // ���� ������ ���߰�???????????????
            // ������ �Լ��� 1,5�� �ڿ� �����Ѵ�.
            Invoke("Reload", 1.5f);
            isReload = false;
        }

        elapsedTime += Time.deltaTime;

        // ���콺 ������ Ŭ���� �ϰ�, ����ð��� ��Ÿ���� �ð����� Ŀ���ٸ�
        if (Input.GetMouseButtonDown(1) && elapsedTime >= coolTime)
        {
            //�Ѿ� �������� �����Ѵ�.
            //go = Instantiate(spiralRocket);
            // ���̸� �����Ѵ�.
            Ray rRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // ���̿� �ε��� ������Ʈ�� �ִٸ�
            if (Physics.Raycast(rRay, out rHitInfo, maxDistance))
            {
                print(rHitInfo.transform.name);
                //rRay.GetPoint(50)
                // �� ������Ʈ�� ������ ���� ���������� ������ �����.
                //crossDir = rHitInfo.point - firePosition.transform.position;
                //crossDir.Normalize();
                StartCoroutine(AirShootRocketCoroutine(rHitInfo.point));
            }

            // ���̰� ������ �������� ���ϸ�
            //else if(rHitInfo.distance == maxDistance)
            else
            {
                #region ������� �ڵ�
                // ������ ������ ���������� ���̾��������� �� ������ �����
                //crossDir = new Vector3(0, 0, maxDistance) - firePosition.transform.position;
                //crossDir.Normalize();
                //print(crossDir);
                //// ī�޶��� ���� �������� �̵��ϰ� �ʹ�.
                //transform.position += dir * locketSpeed * Time.deltaTime;

                //// �� �������� �̵��Ѵ�.
                //transform.position += crossDir * locketSpeed * Time.deltaTime;
                #endregion
                print(rRay.GetPoint(maxDistance));
                StartCoroutine(AirShootRocketCoroutine(rRay.GetPoint(maxDistance)));
            }

            // ����ð��� �ʱ�ȭ�Ѵ�.
            elapsedTime = 0;

            //    // �Ѿ��� firePosition���� ȸ����Ų��.
            //    go.transform.rotation = firePosition.transform.rotation;
            //    // �Ѿ��� ���� ��ġ�� firePosition���� ��ġ��Ų��.
            //    go.transform.position = firePosition.transform.position;

            //}

            //// �� �������� �����δ�.
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
        // �� �ڷ�ƾ�� �� ������ ����? - ������ ��ġ�� ���ݾ� ������ �������� �ƽ� �Ÿ����� ������ �����.
        // ������ ���� ��ġ�� ��������
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
                // �ƽ��Ÿ��� �������� �� �Ѿ��� �ı���Ų��.
                Destroy(go);
                yield break;
            }
            // �ð��� ������ ���Ѽ� �� ������ �ð���ŭ ������ �� �տ� �����Ѵ�.
            //curTime += Time.deltaTime * 0.1f;
            yield return null;
        }
        //DestroyImmediate(spiralRocket);
    }
}
