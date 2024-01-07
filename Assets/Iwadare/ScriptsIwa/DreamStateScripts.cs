using DG.Tweening;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Events;

public class DreamStateScripts : MonoBehaviour
{
    static DreamState _dreamState;
    public static DreamState DreamState => _dreamState;
    [SerializeField] float _dreamTime = 5f;
    [SerializeField] float _dreamCoolTime = 10f;
    [SerializeField] float _initialAudioVolume = 0.5f;
    [SerializeField] float _dreamAudioVolume = 0.25f;
    float _time;
    float _defaultUIPersent;
    public float _uiPersent = 1;
    [Tooltip("夢の始まり")]
    public static UnityAction DreamWorld;
    [Tooltip("夢の終わり")]
    public static UnityAction DreamWorldEnd;
    [SerializeField] Transform _playerTrans;
    Vector2 _tmpTrans;
    static bool _isCountTimer;
    public static bool IsCountTimer => _isCountTimer;
    // Start is called before the first frame update
    void Awake()
    {

        _dreamState = DreamState.Normal;
        _time = _dreamCoolTime;
        _isCountTimer = true;
        CoolTimeUIUpdate();
        _defaultUIPersent = _uiPersent;
        _initialAudioVolume = GManager.Instance.GetBGMVolume();
    }

    private void OnEnable()
    {
        DreamWorld += ChangeDream;
        DreamWorldEnd += ChangeDreamNormal;
    }

    private void OnDisable()
    {
        DreamWorld -= ChangeDream;
        DreamWorldEnd -= ChangeDreamNormal;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCountTimer) { return; }
        _time += Time.deltaTime;
        if (_dreamState == DreamState.Dream)
        {
            if (_time > _dreamTime)
            {
                _time = 0f;
                DreamWorldEnd.Invoke();
            }
            DreamTimeUIUpdate();
        }
        else
        {

            if (_time >= _dreamCoolTime)
            {
                _time = _dreamCoolTime;
                _isCountTimer = true;
            }
            CoolTimeUIUpdate();
        }
    }

    void DreamTimeUIUpdate()
    {
        _uiPersent = _time / _dreamTime;
    }

    void CoolTimeUIUpdate()
    {
        _uiPersent = _time / _dreamCoolTime;
    }

    void ChangeDream()
    {
        Debug.Log("Welcome to Dream！");
        _dreamState = DreamState.Dream;
        _tmpTrans = _playerTrans.position;
        _isCountTimer = false;
        _time = 0f;
        GManager.Instance.SetBGMVolume(_dreamAudioVolume);
    }

    void ChangeDreamNormal()
    {
        Debug.Log("夢の時間は終わる...");
        _dreamState = DreamState.Normal;
        _playerTrans.position = _tmpTrans;
        _isCountTimer = false;
        GManager.Instance.SetBGMVolume(_initialAudioVolume);
    }
}

public enum DreamState
{
    Normal,
    Dream
}
