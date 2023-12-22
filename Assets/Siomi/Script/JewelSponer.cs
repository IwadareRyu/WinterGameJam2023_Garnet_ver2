using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelSponer : MonoBehaviour
{
    [SerializeField, Header("ランダムスポナーの場所")] Transform[] _jewelSponer;
    [SerializeField, Header("宝石のプレハブ")] GameObject _jewelPrefab;
    [SerializeField, Header("スポーンさせたい宝石の数")] int _jewelCount = 3;
    private void Awake()
    {
        JewelSpon();
    }

    void JewelSpon()
    {
        //何個宝石を生成したのかのカウンタ変数
        int count = 0;
        //スポナー状態管理用の変数を宣言
        int[] sponerCount = new int[_jewelSponer.Length];

        //どこに生成するかの処理を行う
        while(count < _jewelCount)
        {
            int rand = Random.Range(0, _jewelSponer.Length);
            //被っていたら生成しない
            if (sponerCount[rand] == 0 )
            {
                Instantiate( _jewelPrefab, _jewelSponer[rand].position, Quaternion.identity);
                sponerCount[rand] = 1;
                count++;
            }
        }
    }
}
