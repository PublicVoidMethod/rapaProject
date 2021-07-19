using UnityEngine;

public class BotManager : MonoBehaviour
{

    public GameObject BotFactory;

    public BotFSM botfsm;
    GameObject Bot;
    // public int Count = 0;
    float currentTime;

    void Start()
    {
        //공장에서 봇 생성
        Instantiate(BotFactory, GetRandomPosition(), transform.rotation);
        // Count++;
    }

    public void BotInstant()
    {

        Instantiate(BotFactory, GetRandomPosition(), transform.rotation);


    }

    public Vector3 GetRandomPosition()
    {
        float x = Random.Range(20f, 22f);
        float z = Random.Range(8f, 27f);

        Vector3 result = new Vector3(x, transform.position.y, z);
        return result;
    }

}