using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class SnakeHead : MonoBehaviour 
{
    public List<Transform> bodyList = new List<Transform>();
    public float Velocity = 0.35f;
    public int Step;
    private int x;
    private int y;
    private Transform canvas;
    private Vector3 HeadPos;
    //蛇身
    public GameObject bodyPerfab;
    public Sprite[] bodySprite =  new Sprite[2];
    //蛇的状态
    private bool isDie = false;
    //死亡特效
    public GameObject dieEffect;
    //声音
    public AudioClip eatClip;
    public AudioClip dieClip;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("sh", "sh02"));
        bodySprite[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb01", "sb0201"));
        bodySprite[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb02", "sb0202"));
    }

    private void Start()
    {
        InvokeRepeating("Move", 0, Velocity);
        x = 0;
        y = Step;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && MainUIController.Instance.isPause == false && isDie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, Velocity-0.2f);
        }

        if (Input.GetKeyUp(KeyCode.Space) && MainUIController.Instance.isPause == false && isDie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, Velocity);
        }

        if (Input.GetKey(KeyCode.W) && y != -Step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            x = 0;
            y = Step;
        }

        if (Input.GetKey(KeyCode.S) && y != Step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0;
            y = -Step;
        }

        if (Input.GetKey(KeyCode.A) && x != Step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -Step;
            y = 0;
        }

        if (Input.GetKey(KeyCode.D) && x != -Step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = Step;
            y = 0;
        }
    }

    private void Move()
    {
        //保存下来蛇头移动之前的位置
        HeadPos = gameObject.transform.localPosition;  
        //蛇头向期望位置移动
        gameObject.transform.localPosition = new Vector3(HeadPos.x + x, HeadPos.y + y, HeadPos.z);
        if (bodyList.Count > 0)
        {
            for (int i = bodyList.Count - 2; i >= 0; i--)
            {
                bodyList[i + 1].localPosition = bodyList[i].localPosition;
            }
            bodyList[0].localPosition = HeadPos;
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(dieClip, Vector3.zero);
        CancelInvoke();
        isDie = true;
        Instantiate(dieEffect);
        //unity提供的PlayerPrefs类可以用来记录数据
        PlayerPrefs.SetInt("lastl", MainUIController.Instance.length);
        PlayerPrefs.SetInt("lasts", MainUIController.Instance.score);
        if (PlayerPrefs.GetInt("basts", 0) < MainUIController.Instance.score)
        {
            PlayerPrefs.SetInt("bastl", MainUIController.Instance.length);
            PlayerPrefs.SetInt("basts", MainUIController.Instance.score);
        }
        StartCoroutine(GameOver(1.5f));
    }

    IEnumerator GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    void Grow()
    {
        AudioSource.PlayClipAtPoint(eatClip, Vector3.zero);
        int index = (bodyList.Count % 2 == 0) ? 0 : 1;
        GameObject body = Instantiate(bodyPerfab, new Vector3(2000,2000,0), Quaternion.identity);
        body.GetComponent<Image>().sprite = bodySprite[index];
        body.transform.SetParent(canvas, false);
        bodyList.Add(body.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI();
            Grow();
            if (Random.Range(0, 100) < 20)
            {
                FoodMaker.Instance.foodMake(true);
            }
            else
            {
                FoodMaker.Instance.foodMake(false);
            }
        }
        else if (collision.gameObject.CompareTag("Reward"))
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI(Random.Range(5, 15));
            Grow();
        }
        else if (collision.gameObject.CompareTag("Body"))
        {
            Die();
        }
        else
        {
            if (MainUIController.Instance.hasBorder)
            {
                Die();
            }
            else
            {
                switch (collision.gameObject.name)
                {
                    case "Up":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 30, transform.localPosition.z);
                        break;
                    case "Down":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 30, transform.localPosition.z);
                        break;
                    case "Left":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 180, transform.localPosition.y, transform.localPosition.z);
                        break;
                    case "Right":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 240, transform.localPosition.y, transform.localPosition.z);
                        break;
                }
            }  
        }
    }

}
