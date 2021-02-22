using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    //どのセーブデータを開いているか
    public static int SELECT_DATA_NUMBER = 0;

    public AudioClip titleBgm;
    public GameObject DefaultObj;
    public GameObject okbutton;
    public GameObject startbutton;
    public GameObject DeleatOption;
    public GameObject ContinueButton;
    //public GameObject DataDreateButton;
    //[Header("SaveData選択画面")]
    //public GameObject saveData;
    //public GameObject savedatafirst;
    //public GameObject saveDataOkbutton;
    //public Text savedataText;

    private int selectNumber = 0;
    //public PlayerPrefsCommon playerPrefsCommon;
    [SerializeField]private string LoadSceneName;
    
    //private List<string[]> Itemsdata = new List<string[]>();
    // Start is called before the first frame update
    void Start()
    {
        AudioManager2D.Instance.AudioBgm.clip = titleBgm;
        AudioManager2D.Instance.AudioBgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    /// <summary>
    /// スタートボタン選択
    /// </summary>
    public void OnStartClick()
    {
        AudioManager2D.Instance.AudioBgm.Stop();
        SceneManager.LoadScene(LoadSceneName);
    }
    
   
    //ゲーム終了ボタンが選択されたら
    public void OnOptionClick()
    {
        //ゲーム終了
        Application.Quit();
    }

    /// <summary>
    /// データ削除ボタン押下時
    /// </summary>
    public void OnClickDelete()
    {
        if (!DeleatOption.activeInHierarchy)
        {
            DefaultObj.SetActive(false);
            DeleatOption.SetActive(true);
            EventSystem.current.SetSelectedGameObject(okbutton);
        }

    }
    //確認画面でOK押下
    public void OnClickOkDeleat()
    {

        PlayerPrefs.DeleteAll();
        DeleatOption.SetActive(false);
        DefaultObj.SetActive(true);
        EventSystem.current.SetSelectedGameObject(startbutton);
    }
    //確認画面でNO押下
    public void OnClickNoButton()
    {
        DeleatOption.SetActive(false);
        DefaultObj.SetActive(true);
        EventSystem.current.SetSelectedGameObject(startbutton);
    }


}
