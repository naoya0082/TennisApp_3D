using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardAdCheckManager : MonoBehaviour
{
    [SerializeField] GameObject confirmPanel;
    [SerializeField] GameObject getRewardPanel;

    // Start is called before the first frame update
    void Start()
    {
        confirmPanel.SetActive(false);
        getRewardPanel.SetActive(false);
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
        yield return new WaitForSeconds(1.0f);
        getRewardPanel.SetActive(false);
        SceneManager.LoadScene("Player shop");
    }
}
