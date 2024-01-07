using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DreamCanvasScripts : MonoBehaviour
{
    Canvas _dreamCanvas;
    [SerializeField] Image _backDreamImage;
    [SerializeField] Transform _sleepImage;
    [SerializeField] Transform _playerPos;
    [SerializeField] GameObject _stunPlayer;
    GameObject _nowStunPlayer;
    [Header("ZZZÇÃà íuÇ‚ë¨ìxÇÃÉâÉìÉ_ÉÄí≤êÆ")]
    [SerializeField] float _minY;
    [SerializeField] float _maxY;
    [SerializeField] float _minsleepSpeed;
    [SerializeField] float _maxsleepSpeed;
    [SerializeField] float _dreamTime = 0.1f;
    Vector3 _cameraPos;
    Coroutine _nowDreamCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        DreamStateScripts.DreamWorld += UIDreamWorld;
        DreamStateScripts.DreamWorldEnd += UIDreamWorldEnd;
        _dreamCanvas = GetComponent<Canvas>();
        Color tmpColor = _backDreamImage.color;
        tmpColor.a = 0;
        _backDreamImage.color = tmpColor;

    }

    private void OnDisable()
    {
        DreamStateScripts.DreamWorld -= UIDreamWorld;
        DreamStateScripts.DreamWorldEnd -= UIDreamWorldEnd;
    }

    public void UIDreamWorld()
    {
        _nowDreamCoroutine = StartCoroutine(DreamSleep());
    }

    public void UIDreamWorldEnd()
    {
        EndSleep();
        StopCoroutine(_nowDreamCoroutine);
        if (_nowDreamCoroutine != null) { _nowDreamCoroutine = null; }
    }

    IEnumerator DreamSleep()
    {
        _backDreamImage.DOFade(1f,1f);
        _nowStunPlayer = Instantiate(_stunPlayer,_playerPos.position,Quaternion.identity);

        while(true)
        {
            for(float time = 0;time < 0.1f;time += Time.deltaTime)
            {
                yield return new WaitForFixedUpdate();
            }

            var camera = Camera.main.transform;
            _cameraPos = camera.position;
            _cameraPos.x += 25;
            var YPosRam = Random.Range(_minY,_maxY);
            _cameraPos.y = YPosRam;
            _cameraPos.z += 30;
            var speedRam = Random.Range(_minsleepSpeed, _maxsleepSpeed);
            var sleepImage = Instantiate(_sleepImage,_cameraPos,Quaternion.identity);
            sleepImage.SetParent(camera);
            sleepImage.GetComponent<MoveSleep>().MoveObject(speedRam);
            yield return null;
        }
    }

    void EndSleep()
    {
        _backDreamImage.DOFade(0,1f);
        Destroy(_nowStunPlayer);
        if (_nowStunPlayer != null) { _nowStunPlayer = null; }
    }
}
