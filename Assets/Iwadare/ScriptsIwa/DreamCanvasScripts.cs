using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamCanvasScripts : MonoBehaviour
{
    Canvas _dreamCanvas;
    [SerializeField] Image _backDreamImage;
    [SerializeField] Transform _sleepImage;
    [SerializeField] float _sleepSpeed;

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

    }

    public void UIDreamWorldEnd()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
