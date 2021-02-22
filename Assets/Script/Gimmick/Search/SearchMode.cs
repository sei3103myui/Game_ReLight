using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SearchMode : MonoBehaviour
{
    public enum LightMode
    {
        Left,
        Right
    }

    public enum AppearTimeMode
    {
        Repeart,
        Limit,
        True
    }

    public enum AppearObjMode
    {
        Human,
        SetActiveTrue,
        SetActiveFalse,
        IsTrigger,
        Collider,
        ChangeImage,
        TalkFlgOn,
        Null
    }
    [Header("0より－領域ならLeft　＋領域ならRight")]
    public LightMode lightMode;
    [Header("オブジェクトを明るくする時間の設定")]
    public AppearTimeMode appearMode;
    [Header("オブジェクトをどのように動的にするか")]
    public AppearObjMode[] appearObjMode;
    [Header("モードがLimitのとき待つ時間")]
    public float waitTime = 5f;
    [Header("Rayを飛ばす距離(左に飛ばすならマイナスつける)")]
    public float dis = 10f;

    [Header("サーチオブジェクト")]
    public GameObject appearObj;
    [Header("SetActive(True)にするオブジェクト")]
    public GameObject[] NewActiveObj;
    [Header("画像差し替えに使う新しい画像")]
    public Sprite Newsprite;
    [HideInInspector]public SpriteRenderer appearRenderer;
    void Start()
    {
        appearRenderer = appearObj.GetComponent<SpriteRenderer>();
    }

    public void setEvent()
    {
        for(int i = 0; i < appearObjMode.Length; i++)
        {
            switch (appearObjMode[i])
            {
                case AppearObjMode.Collider:
                    appearObj.AddComponent<Collider2D>();
                    break;
                case AppearObjMode.Human:
                    
                    appearObj.AddComponent<Collider2D>().isTrigger = true;
                    
                    break;
                case AppearObjMode.IsTrigger:
                    appearObj.GetComponent<Collider2D>().isTrigger = true;
                    break;
                case AppearObjMode.SetActiveTrue:
                    for(int o = 0; o < NewActiveObj.Length; o++)
                    {
                        NewActiveObj[o].SetActive(true);
                    }
                    break;
                case AppearObjMode.SetActiveFalse:
                    appearObj.SetActive(false);
                    break;
                case AppearObjMode.ChangeImage:
                    appearRenderer.sprite = Newsprite;
                    break;
                case AppearObjMode.TalkFlgOn:
                    appearObj.GetComponent<AppearObjController>().TalkFlgOn();
                    break;
                case AppearObjMode.Null:
                    break;
            }
        }
    }

}
