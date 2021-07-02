using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAttack : MonoBehaviour
{
    public GameObject spiralRocket;
    public Transform firePosition;
    public float coolTime = 5.0f;

    float elapsedTime = 0;

    void Start()
    {
        // ����ð��� ��Ÿ���� �ְ�
        elapsedTime = coolTime;
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
            
            
            // �Ѿ� �������� �����Ѵ�.
            GameObject go = Instantiate(spiralRocket);

            // �Ѿ��� firePosition���� ȸ����Ų��.
            go.transform.rotation = firePosition.transform.rotation;
            // �Ѿ��� ���� ��ġ�� firePosition���� ��ġ��Ų��.
            go.transform.position = firePosition.transform.position;

            // ����ð��� �ʱ�ȭ�Ѵ�.
            elapsedTime = 0;
        }
    }
}
