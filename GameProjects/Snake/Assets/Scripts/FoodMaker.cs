using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class FoodMaker : MonoBehaviour 
{
    private static FoodMaker _instance;
    public static FoodMaker Instance
    {
        get
        {
            return _instance;
        }
    }

    public int xlimit = 21;
    public int ylimit = 14;
    public int xoffset = 7;
    public GameObject foodPerfab;
    public Sprite[] foodSprites;
    private Transform foodHolder;

    public GameObject rewardPerfab;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        foodHolder = GameObject.Find("FoodHolder").transform;
        foodMake(false);
    }

    public void foodMake(bool isReward)
    {
        int index = Random.Range(0, foodSprites.Length);
        GameObject food = Instantiate(foodPerfab);
        food.GetComponent<Image>().sprite = foodSprites[index];
        food.transform.SetParent(foodHolder, false);
        int x = Random.Range(-xlimit + xoffset, xlimit);
        int y = Random.Range(-ylimit, ylimit);
        food.transform.localPosition = new Vector3(x * 30, y * 30, 0);

        if (isReward == true)
        {
            GameObject reward = Instantiate(rewardPerfab);
            reward.transform.SetParent(foodHolder, false);
            x = Random.Range(-xlimit + xoffset, xlimit);
            y = Random.Range(-ylimit, ylimit);
            food.transform.localPosition = new Vector3(x * 30, y * 30, 0);
        }
    }
	
}
