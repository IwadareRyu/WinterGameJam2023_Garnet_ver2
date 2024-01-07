using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBGMManager : MonoBehaviour
{
    GManager _gm;
    [SerializeField] AudioClip _bgmClip;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GManager.Instance;
        if (_gm.bgmSource.isPlaying) { _gm.StopBGM(); }
        _gm.PlayBGM(_bgmClip);
    }
}
