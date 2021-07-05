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
            //���忡�� �� ����
            GameObject Bot = Instantiate(BotFactory);
            //�� ��ġ�� �����ٳ���ʹ�
            Bot.transform.position = this.transform.position;
            //�� ����� ��ġ
            Bot.transform.rotation = transform.rotation;
            count++;
        }
    }
}
