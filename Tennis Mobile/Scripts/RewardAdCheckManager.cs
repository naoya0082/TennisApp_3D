using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class RewardAdCheckManager : MonoBehaviour
{
    [SerializeField] GameObject confirmPanel;
    [SerializeField] GameObject getRewardPanel;
    [SerializeField] Button rewardedAdBtn;
    [SerializeField] Image loadingImg;
    [SerializeField] Image batch;
    [SerializeField] Text text;
    [SerializeField] int adPlayLimitCount;

    private string lastAccessDate;
    private string currentDate;
    private int initPlayAdCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("adPlayCount", initPlayAdCount);
        //今の日付を取得
        currentDate = DateTime.Now.Date.ToString("yyyy/MM/dd");

        //日付が変わったら再生回数を初期化
        if (currentDate != PlayerPrefs.GetString("lastAccessDate", lastAccessDate))
            PlayerPrefs.SetInt("adPlayCount", initPlayAdCount);

        //指定回数以上で再生ボタンを非活性化
        if (PlayerPrefs.GetInt("adPlayCount") >= adPlayLimitCount)
        {
            rewardedAdBtn.GetComponent<Button>().interactable = false;
            batch.gameObject.SetActive(false);
            text.gameObject.SetActive(true);
        }
        else
        {
            rewardedAdBtn.GetComponent<Button>().interactable = true;
            Transform batchText = batch.transform.GetChild(0);
            batchText.GetComponent<Text>().text = (adPlayLimitCount - PlayerPrefs.GetInt("adPlayCount")).ToString();
            batch.gameObject.SetActive(true);
            text.gameObject.SetActive(false);
        }


        //最後のアクセス日付を記録
        lastAccessDate =  DateTime.Now.Date.ToString("yyyy/MM/dd");
        PlayerPrefs.SetString("lastAccessDate", lastAccessDate);
    }

    public void OnClickConfirmBtn()
    {
        confirmPanel.SetActive(true);
    }

    public void OnClickShowRewardAdsBtn()
    {
        confirmPanel.SetActive(false);
    }

    public void OnClickBackBtn()
    {
        confirmPanel.SetActive(false);
    }

    public void OnClickReloadBtn()
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        getRewardPanel.SetActive(false);

        loadingImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Player shop");
    }
}
