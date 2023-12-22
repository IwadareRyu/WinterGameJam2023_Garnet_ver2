using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //[SerializeField] GameObject go;
    private void Start()
    {
        //this.gameObject.SetActive(false);
    }
    private void Update()
    {
        //ジュエルのカウントを取ってくる
        int juwelCount = GManager.Instance.GetJewelCount();
        //3つ以上取っていたらゴールを出現させる
        if (juwelCount >= 3)
        {
            this.gameObject.SetActive(true);
        }
    }

    //ゴールの処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            //ゴールの処理を書く
            GManager.Instance.Goal();
        }
    }
}
