﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class MainUIController : MonoBehaviour 
{
    public int score = 0;
    public int length = 0;

    public Text msgText;
    public Text scoreText;
    public Text lengthText;

    public Image bgImage;
    private Color tempColor;
    public bool isPause = false;
    public Image pauseImage;
    public Sprite[] pauseSprite;

    public bool hasBorder = true;

    private static MainUIController _instance;
    public static MainUIController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        switch (score / 100)
        {
            case 0:
            case 1:
            case 2:
                break;
            case 3:
            case 4:
                ColorUtility.TryParseHtmlString("#CCEEFFFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 2;    
                break;
            case 5:
            case 6:
                ColorUtility.TryParseHtmlString("#CCFFDBFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 3;
                break;
            case 7:
            case 8:
                ColorUtility.TryParseHtmlString("#EBFFCCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 4;
                break;
            case 9:
            case 10:
                ColorUtility.TryParseHtmlString("#FFF3CCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 5;
                break;
            default:
                ColorUtility.TryParseHtmlString("#FFDACCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "无尽阶段";
                break;
        }
    }

    public void UpdateUI(int s = 5, int l = 1)
    {
        score += s;
        length += l;
        scoreText.text = "得分：\n" + score;
        lengthText.text = "长度：\n" + length;
    }

    //暂停方法
    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
            pauseImage.sprite = pauseSprite[1];
        }
        else
        {
            Time.timeScale = 1;
            pauseImage.sprite = pauseSprite[0];
        }
    }

    //主页按钮
    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("border", 1) == 0)
        {
            hasBorder = false;
            foreach (Transform t in bgImage.gameObject.transform)
            {
                t.gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }

}
