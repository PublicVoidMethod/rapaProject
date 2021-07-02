using UnityEngine;

public class BotMove : MonoBehaviour
{

    public GameObject BP;
    public int BotSpeed = 1;
    public int Botdist = 15;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = BP.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.forward * Time.deltaTime * BotSpeed;
        if ((Vector3.Distance(BP.transform.position, transform.position)) > Botdist)
        {
            BP.transform.position = transform.position;
            transform.Rotate(0, -90, 0);
        } 
    }
}
