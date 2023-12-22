using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bom : MonoBehaviour
{
    [SerializeField, Header("爆発までの時間")] float _bomInterval = 5f;
    [SerializeField, Header("爆発エフェクト")] GameObject _bomEffect;
    [SerializeField, Header("爆発範囲")] float _bomRange = 5.0f;
    [SerializeField, Header("オーディオソースの設定")] AudioSource _audioSource;
    [SerializeField, Header("出したい音")] AudioClip _audioClip;
    private void Start()
    {
        Invoke("Expllosion", _bomInterval);
    }

    void Expllosion()
    {
        ////音を出す
        //_audioSource.PlayOneShot(_audioClip);

        // 指定範囲のコライダーを全て取得する
        var cols = Physics2D.OverlapCircleAll(this.transform.position, _bomRange);

        //プレイヤーとエネミーを探す
        foreach(var c in cols)
        {
            if(c.TryGetComponent<StunStateScripts>(out var stunState))
            {
                //爆発時のキャラの処理
                stunState.ExplosionScale();
                //スタンさせる
                stunState.ChangeStunState();
            }
        }
        //爆発エフェクトを出現させる
        Instantiate(_bomEffect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //爆発範囲のギズモ
        Gizmos.DrawWireSphere(transform.position, _bomRange);
    }
} 
