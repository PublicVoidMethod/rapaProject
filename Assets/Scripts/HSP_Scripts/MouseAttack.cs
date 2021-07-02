using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAttack : MonoBehaviour
{
    public GameObject spiralRocket;
    public GameObject firePosition;
    public float coolTime = 5.0f;
    public float locketSpeed = 10.0f;
    
    Vector3 dir;
    Vector3 crossDir;
    GameObject go;
    RaycastHit rHitInfo;

    float elapsedTime = 0;

    void Start()
    {
        go = null;
        // ����ð��� ��Ÿ���� �ְ�
        elapsedTime = coolTime;

        // ���� ������ �����
        dir = new Vector3(0, 0, 1);

        // ���� ī�޶��� ���� �������� ���ư��� �ʹ�.
        dir = Camera.main.transform.forward;
    }

    void Update()
    {
        // ���콺 ���� Ŭ���� ���� ��
        if (Input.GetMouseButtonDown(0))
        {
            // ���̸� �����ϰ�
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            // ���̰� �����Ǿ��� �� ���� ������Ʈ�� ������ ���� ������ ����
            RaycastHit hitInfo;

            // ���̸� �߻��Ѵ�.
            if (Physics.Raycast(ray, out hitInfo))
            {
                print(hitInfo.transform.name);
            }
        }

        elapsedTime += Time.deltaTime;

        // ���콺 ������ Ŭ���� �ϰ�, ����ð��� ��Ÿ���� �ð����� Ŀ���ٸ�
        if (Input.GetMouseButtonDown(1) && elapsedTime >= coolTime)
        {
            // ���̸� �����Ѵ�.
            Ray rRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // ���̿� �ε��� ������Ʈ�� �ִٸ�
            if (Physics.Raycast(rRay, out rHitInfo))
            {
                print(rHitInfo.transform.name);

                // �� ������Ʈ�� ������ ���� ���������� ������ �����.
                crossDir = rHitInfo.point - firePosition.transform.position;

            }
            else
            {
                // ī�޶��� ���� �������� �̵��ϰ� �ʹ�.
                transform.position += dir * locketSpeed * Time.deltaTime;
            }
            
            // �Ѿ� �������� �����Ѵ�.
            go = Instantiate(spiralRocket);

            // �Ѿ��� firePosition���� ȸ����Ų��.
            go.transform.rotation = firePosition.transform.rotation;
            // �Ѿ��� ���� ��ġ�� firePosition���� ��ġ��Ų��.
            go.transform.position = firePosition.transform.position;

            // ����ð��� �ʱ�ȭ�Ѵ�.
            elapsedTime = 0;

        }
        // �� �������� �����δ�.
        if(go != null)
        go.transform.position += crossDir * locketSpeed * Time.deltaTime;
    }
}
