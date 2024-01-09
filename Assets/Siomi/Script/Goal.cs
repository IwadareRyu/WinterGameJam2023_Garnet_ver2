using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    SpriteRenderer _mySprite;
    Collider2D _myColider;
    [SerializeField] Canvas _canvas;

    private void Start()
    {
        _mySprite = GetComponent<SpriteRenderer>();
        _myColider = GetComponent<Collider2D>();
        _mySprite.enabled = false;
        _canvas.enabled = false;
        _myColider.enabled = false;
    }
    //private void Update()
    //{
    //    //ジュエルのカウントを取ってくる
    //    int juwelCount = GManager.Instance.GetJewelCount();
    //    //3つ以上取っていたらゴールを出現させる
    //    if (juwelCount >= 3)
    //    {
    //        this.gameObject.SetActive(true);
    //    }
    //}

    public void GoalPointEnable()
    {
        _mySprite.enabled = true;
        _canvas.enabled = true;
        _myColider.enabled = true;
    }

    //ゴールの処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player") && DreamStateScripts.DreamState == DreamState.Normal)
        {
            //ゴールの処理を書く
            GManager.Instance.Goal();
        }
    }
}
