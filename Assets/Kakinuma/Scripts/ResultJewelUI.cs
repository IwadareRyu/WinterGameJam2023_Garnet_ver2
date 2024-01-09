using UnityEngine;

public class ResultJewelUI : MonoBehaviour
{
    [SerializeField] GameObject[] _jewelUI;
    GManager _gManager = null;

    int _jewelCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        _gManager = GManager.Instance;
        _jewelCount = _gManager.GetJewelCount();
    }

    // Update is called once per frame
    void Update()
    {
        ShowResultJewel();
    }

    private void ShowResultJewel()
    {
        switch (_jewelCount)
        {
            case 0:
                _jewelUI[0].SetActive(true);
                break;
            case 1:
                _jewelUI[1].SetActive(true);
                break;
            case 2:
                _jewelUI[2].SetActive(true);
                break;
            case 3:
                _jewelUI[3].SetActive(true);
                break;
        }
    }
}
