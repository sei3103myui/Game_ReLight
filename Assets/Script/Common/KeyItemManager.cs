using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemManager : MonoBehaviour
{
    //キーアイテムの番号
    [Header("キーアイテムの番号")]
    public int KeyNumber = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClearSave()
    {
        PlayerPrefs.SetInt(string.Format("StageClear_{0}", KeyNumber), 1);
        
    }
}
