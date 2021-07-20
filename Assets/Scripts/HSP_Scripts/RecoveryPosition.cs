using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryPosition : MonoBehaviour
{
    public GameObject recoveryField;
    public float dropSpeed = 5.0f;
    public GameObject recoveryEffect;

    GameObject go;
    CharacterController cc;
    GameObject recoveryParticle;
    //UnityEngine.BoxCollider boxCol;

    bool isRecoveryDelay = true;
    float recoveryCoolTime = 6f;
    bool isRecoveryEffect = true; // ��ü�� ����Ʈ�� �����Ǵ� ����(true : ����Ʈ ����, false : ����Ʈ ����x)
    AudioSource healAudio;
   void Start()
    {
        isRecoveryDelay = false;
        healAudio = GetComponent<AudioSource>();
        // eŰ�� ������ ��Ŀ���� �ڽ��� �����ȴ�.
        // ��Ŀ���� �ڽ��� �����Ǹ� 10���� ��Ÿ���� ������ �ȴ�.
    }

    void Update()
    {
        // ȸ����ų(RecoveryField) eŰ�� ������
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRecoveryEffect = true;
            if (!isRecoveryDelay)
            {
                isRecoveryDelay = true;
                RecoveryInstantiate();
                StartCoroutine(RecoveryDelayCoroutine());
                healAudio.Play();
            }
            // ������ ȸ�� ��ų �ڽ��� ���� ��ġ�� ��ġ��Ų��.
            //go.transform.position = transform.position;
        }
        //else if (isRecoveryDelay)
        //{
        //    StartCoroutine(RecoveryDelayCoroutine());
        //}
        //isRecoveryDelay = true;

        if (go != null)
        {
            // �Ʒ��� ������ ������ �̵��Ѵ�.
            Vector3 dir = Vector3.down;
            cc.Move(dir * dropSpeed * Time.deltaTime);

            if(cc.collisionFlags == CollisionFlags.Below && isRecoveryEffect)
            {
                //go.transform.GetChild(0).gameObject.SetActive(true);
                //go.gameObject.GetComponent<BoxCollider>().gameObject.SetActive(true); <- �ȵ�
                go.gameObject.GetComponent<BoxCollider>().enabled = true;

                recoveryParticle = Instantiate(recoveryEffect);
                recoveryParticle.transform.position = go.transform.position;
                ParticleSystem ps = recoveryParticle.GetComponent<ParticleSystem>();
                ps.Stop();
                ps.Play();
                isRecoveryEffect = false;
            }
        }
    }

    void RecoveryInstantiate()
    {
        // ȸ�� ��ų �ڽ��� �����Ѵ�.
        go = Instantiate(recoveryField, transform.position, Quaternion.identity);
        cc = go.GetComponent<CharacterController>();
        //isRecoveryDelay = true;
    }
    
    IEnumerator RecoveryDelayCoroutine()
    {
        yield return new WaitForSeconds(recoveryCoolTime);
        isRecoveryDelay = false;
    }
}
