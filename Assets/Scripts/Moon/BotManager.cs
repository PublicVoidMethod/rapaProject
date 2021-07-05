using System.Collections;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    public static BotManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject BotFactory;

    int count;
    public int COUNT
    {
        get { return count; }
        set
        {
            count = value;
            if (count < 0)
            {
                count = 0;
            }
            if (count > maxCount)
            {
                count = maxCount;
            }
        }
    }

    public int maxCount = 2;


    // Update is called once per frame
    void Update()
    {
        if (count < maxCount)
        {
            //공장에서 봇 생성
            GameObject Bot = Instantiate(BotFactory);
            //내 위치에 가져다놓고싶다
            Bot.transform.position = this.transform.position;
            //내 방향과 일치
            Bot.transform.rotation = transform.rotation;
            count++;
        }
    }
}
