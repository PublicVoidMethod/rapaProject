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
    bool isRecoveryEffect = true; // 생체장 이펙트가 생성되는 변수(true : 이펙트 생성, false : 이펙트 생성x)
    AudioSource healAudio;
   void Start()
    {
        isRecoveryDelay = false;
        healAudio = GetComponent<AudioSource>();
        // e키를 누르면 리커버리 박스가 생성된다.
        // 리커버리 박스가 생성되면 10초의 쿨타임을 가지게 된다.
    }

    void Update()
    {
        // 회복스킬(RecoveryField) e키를 누르면
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
            // 생성한 회복 스킬 박스를 나의 위치에 위치시킨다.
            //go.transform.position = transform.position;
        }
        //else if (isRecoveryDelay)
        //{
        //    StartCoroutine(RecoveryDelayCoroutine());
        //}
        //isRecoveryDelay = true;

        if (go != null)
        {
            // 아래의 방향을 가지고 이동한다.
            Vector3 dir = Vector3.down;
            cc.Move(dir * dropSpeed * Time.deltaTime);

            if(cc.collisionFlags == CollisionFlags.Below && isRecoveryEffect)
            {
                //go.transform.GetChild(0).gameObject.SetActive(true);
                //go.gameObject.GetComponent<BoxCollider>().gameObject.SetActive(true); <- 안됨
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
        // 회복 스킬 박스를 생성한다.
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
