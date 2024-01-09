using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Jewel : MonoBehaviour
{
    [SerializeField] bool IsDream = false;
    [SerializeField] bool IsFound = false;
    AudioSource _itemAudioSource;
    [SerializeField, Header("獲得時出したい音")] AudioClip _audioClip;
    [SerializeField, Header("光らせるとき出したい音")] AudioClip _shineAudioClip;
    [SerializeField,Header("夢モードで触った時に出すもの")] GameObject _itemShine;
    Collider2D _col;
    SpriteRenderer _spriteRenderer;
    [SerializeField] GameObject _prefabGoalCanvas;

    // Start is called before the first frame update
    void Start()
    {
        DreamStateScripts.DreamWorld += DreamChange;
        DreamStateScripts.DreamWorldEnd += DreamChange;

        _col = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _itemAudioSource = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<AudioSource>();
        _col.enabled = false;
        _spriteRenderer.enabled = false;
        _itemShine.SetActive(false);
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
            if (!IsFound)
            {
                IsFound = true;
                _itemShine.SetActive(true);
                _itemAudioSource.PlayOneShot(_shineAudioClip);
            }
        }    
        else
        {
            //ゲームマネージャーのジュエルスコアを更新してデストロイする
            GManager.Instance.AddJewelCount();
            if(GManager.Instance.GetJewelCount() == 1)
            {
                FindObjectOfType<Goal>().GoalPointEnable();
                Instantiate(_prefabGoalCanvas,new Vector2(0,0),Quaternion.identity);
            }
            //音を出す
            _itemAudioSource.PlayOneShot(_audioClip);
            Destroy(this.gameObject);
        }

    }
}
