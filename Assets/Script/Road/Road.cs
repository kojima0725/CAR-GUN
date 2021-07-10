﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// 道路及び世界を後ろに流していく
/// </summary>
[RequireComponent(typeof(RoadMaker))]
public class Road : MonoBehaviour, ICanGetTransforms
{
    /// <summary>
    /// 道路生成機
    /// </summary>
    [SerializeField]
    private RoadMaker roadMaker;

    [SerializeField]
    private World world;


    /// <summary>
    /// 道路の生成限界距離(二乗)
    /// </summary>
    private float sqrObjDistance;

    /// <summary>
    /// ロードチップ達
    /// </summary>
    readonly List<RoadChip> roadChips = new List<RoadChip>();

    /// <summary>
    /// ロードチップのリストを返す(リストの先頭が道路の先頭、リスト末尾が道路の末端)
    /// </summary>
    /// <returns></returns>
    public List<RoadChip> GetRoadChips() => roadChips;

    
    public IEnumerable<Transform> Transforms()
    {
        foreach (var item in roadChips)
        {
            yield return item.transform;
        }
    }

    /// <summary>
    /// プレイヤーが今どのロードチップの近くにいるかを返します(少し重い)
    /// </summary>
    /// <returns></returns>
    public RoadChip GetPlayerRoadChip()
    {
        RoadChip min = null;
        float minMag = 0;
        foreach (var item in roadChips)
        {
            if (min is null)
            {
                min = item;
                minMag = item.transform.position.sqrMagnitude;
            }
            else
            {
                float mag = item.transform.position.sqrMagnitude;
                if (mag < minMag)
                {
                    min = item;
                    minMag = mag;
                }
            }
        }

        return min;
    }


    private void Awake()
    {
        //最初の道路を設定しておく
        roadChips.Add(roadMaker.GetFirstRoadChip());
        //距離計算用のメンバ変数の設定
        float limit = StageDatabase.RoadData.LimitDistance;
        sqrObjDistance = limit * limit;

        world.JoinWorld(this);
    }


    private void Update()
    {
        MakeNewRoads();
        DestroyOldRoads();
    }

    

    /// <summary>
    /// 道をどんどん生成していく
    /// </summary>
    private void MakeNewRoads()
    {
        //生成距離が限界に達するまで、道路を生成する
        while (sqrObjDistance > roadMaker.GetLatestRoadChip().transform.position.sqrMagnitude)
        {
            foreach (var item in roadMaker.MakeRoads())
            {
                roadChips.Insert(0, item);
            }
        }
    }

    /// <summary>
    /// 生存距離を超えた道路を削除していく
    /// </summary>
    private void DestroyOldRoads()
    {
        RoadChip a;
        a = roadChips.Last();
        while (sqrObjDistance < a.transform.position.sqrMagnitude)
        {
            roadChips.RemoveAt(roadChips.Count - 1);
            Destroy(a.gameObject);
            a = roadChips.Last();
        }
    }


}
