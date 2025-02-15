﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ゲームのステージの各データを保持するクラス
/// </summary>
public class StageDatabase : MonoBehaviour
{
    private static NomalCarSpawnData nomalCarSpawnData;
    private static EnemyCarSpawnData enemyCarSpawnData;
    private static RoadData roadData;
    private static RoadDesignDocument roadDesignDocument;
    private static CarTypesData carTypesData;
    private static MapData mapData;

    /// <summary>
    /// 一般車の生成に関するデータ
    /// </summary>
    public static NomalCarSpawnData NomalCarSpawnData => nomalCarSpawnData;
    /// <summary>
    /// 敵の車の生成に関するデータ
    /// </summary>
    public static EnemyCarSpawnData EnemyCarSpawnData => enemyCarSpawnData;
    /// <summary>
    /// 道に関するデータ
    /// </summary>
    public static RoadData RoadData => roadData;
    /// <summary>
    /// 道の生成に関するデータ
    /// </summary>
    public static RoadDesignDocument RoadDesignDocument => roadDesignDocument;
    /// <summary>
    /// 出現する車たちのデータ
    /// </summary>
    public static CarTypesData CarTypesData => carTypesData;
    /// <summary>
    /// 地形生成に関するデータ
    /// </summary>
    public static MapData MapData => mapData;

    private void Awake()
    {
        LoadData();
        Destroy(this.gameObject);
    }

    /// <summary>
    /// データを一通り用意する
    /// </summary>
    public static void LoadData()
    {
        nomalCarSpawnData = Resources.Load("NomalCarSpawnData") as NomalCarSpawnData;
        enemyCarSpawnData = Resources.Load("EnemyCarSpawnData") as EnemyCarSpawnData;
        roadData = Resources.Load("RoadData") as RoadData;
        roadDesignDocument = Resources.Load("RoadDesignDocument") as RoadDesignDocument;
        carTypesData = Resources.Load("CarTypesData") as CarTypesData;
        mapData = Resources.Load("MapData") as MapData;
    }
}
