using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

/// <summary>
///
/// </summary>
public class FishMaker : MonoBehaviour 
{
    public Transform fishHoder;
    public Transform[] genPosition;
    public GameObject[] fishPerfabs;

    //生成鱼群的时间间隔
    public float waveGenWaitTime = 0.3f;

    //生成一条鱼的时间间隔
    public float fishGenWaitTime = 0.5f;

    private void Start()
    {
        InvokeRepeating("MakeFishes", 0, waveGenWaitTime);
    }

    void MakeFishes()
    {
        int genPosIndex = Random.Range(0, genPosition.Length);
        int fishPreIndex = Random.Range(0, fishPerfabs.Length);

        int maxNum = fishPerfabs[fishPreIndex].GetComponent<FishAttr>().maxNum;
        int maxSpeed = fishPerfabs[fishPreIndex].GetComponent<FishAttr>().maxSpeed;

        int num = Random.Range((maxNum / 2) + 1, maxNum);
        int speed = Random.Range(maxSpeed / 2, maxSpeed);

        //移动类型
        int moveType = Random.Range(0, 1); //0代表直走，1代表转弯
        int angOffset;                     //仅直走生效  直走的倾斜角
        int angSpeed;                      //仅转弯生效  转弯的角速度

        if (moveType == 0)
        {
            angOffset = Random.Range(-22, 22);
            StartCoroutine(GenStraightFish(genPosIndex, fishPreIndex, num, speed, angOffset));
        }
        else
        {
            if (Random.Range(0, 2) == 0)
            {
                angSpeed = Random.Range(-15, -9);
            }
            else
            {
                angSpeed = Random.Range(15, 9);
            }
            StartCoroutine(GenTurnFish(genPosIndex, fishPreIndex, num, speed, angSpeed));
        }   
    }

    IEnumerator GenStraightFish(int genPosIndex, int fishPreIndex, int num, int speed, int angOffset)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPerfabs[fishPreIndex]);
            fish.transform.SetParent(fishHoder, false);
            fish.transform.localPosition = genPosition[genPosIndex].localPosition;
            fish.transform.localRotation = genPosition[genPosIndex].localRotation;
            fish.transform.Rotate(0, 0, angOffset);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Ef_AutoMove>().speed = speed;
            yield return new WaitForSeconds(fishGenWaitTime);
        }
    }

    IEnumerator GenTurnFish(int genPosIndex, int fishPreIndex, int num, int speed, int angSpeed)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPerfabs[fishPreIndex]);
            fish.transform.SetParent(fishHoder, false);
            fish.transform.localPosition = genPosition[genPosIndex].localPosition;
            fish.transform.localRotation = genPosition[genPosIndex].localRotation;
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Ef_AutoMove>().speed = speed;
            fish.AddComponent<EF_AutoRotate>().speed = angSpeed;    
            yield return new WaitForSeconds(fishGenWaitTime);
        }
    }
}
