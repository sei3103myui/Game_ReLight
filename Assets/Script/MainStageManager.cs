using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStageManager : MonoBehaviour
{
    [Header("ステージの鏡オブジェクト")]
    public SpriteRenderer[] mirra;
    [Header("修復後の鏡の画像")]
    public Sprite[] NewMirra;
    [Header("ラストステージの親オブジェクト")]
    public GameObject lastStage;
    

    private bool isLastStage = false;
    private int clearNum = 0;
    private void Awake()
    {
        //ステージクリアしているかチェック
        for(int i = 1; i <= mirra.Length; i++)
        {
            if (PlayerPrefs.HasKey(string.Format("StageClear_{0}", i))){
                //クリア１　クリアしてなければ0
                if (PlayerPrefs.GetInt(string.Format("StageClear_{0}", i)) == 1)
                {
                    //i番目のステージの割れている鏡を修復
                    mirra[i - 1].sprite = NewMirra[i - 1];
                    clearNum++;
                    
                }
                
            }
            
        }

        if(clearNum == mirra.Length)
        {
            isLastStage = true;
            lastStage.SetActive(true);//最後のステージに行ける道を見えるように
        }

        if (GameManager.gameMode == GameManager.GameMode.Clear)
        {
            GameManager.gameMode = GameManager.GameMode.Play;
        }

    }

    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
