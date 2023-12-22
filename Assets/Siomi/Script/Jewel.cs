using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Jewel : MonoBehaviour
{
    [SerializeField] bool IsDream = false;
    [SerializeField] bool IsFound = false;
    [SerializeField, Header("オーディオソースの設定")] AudioSource _audioSource;
    [SerializeField, Header("出したい音")] AudioClip _audioClip;
    Collider2D _col;
    SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        DreamStateScripts.DreamWorld += DreamChange;
        DreamStateScripts.DreamWorldEnd += DreamChange;

        _col = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _col.enabled = false;
        _spriteRenderer.enabled = false;
    }

    private void OnDisable()
    {
        DreamStateScripts.DreamWorld -= DreamChange;
        DreamStateScripts.DreamWorldEnd -= DreamChange;
    }

    /// <summary> 夢と現実を切り替えたときの処理 </summary>
    public void DreamChange()
    {
        IsDream = !IsDream;
        if (IsDream || IsFound)
        {
            _spriteRenderer.enabled = true;
            _col.enabled = true;
        }
        else
        {
            _spriteRenderer.enabled = false;
            _col.enabled = false;
        }
    }

    /// <summary> ジュエルにあたったときの処理 </summary>
    public void JewelFound()
    {
        if(IsDream)
        {
            IsFound = true;
        }    
        else
        {
            //ゲームマネージャーのジュエルスコアを更新してデストロイする
            GManager.Instance.AddJewelCount();
            //音を出す
            //_audioSource.PlayOneShot(_audioClip);
            Destroy(this.gameObject);
        }

    }
}
