﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageGenerator : MonoBehaviour {

	const int StageTipSize = 30;

	int currentTipIndex;

	public Transform character;//ターゲットキャラクターの指定
	public GameObject[] stageTips;//ステージチッププレファブ配列
	public int startTipIndex;//自動生成開始インデックス
	public int preInstantiate;//生成先読み個数
	public List<GameObject> generatedStageList = new List<GameObject> ();//生成済みステージチップ保持リスト

	// 初期化処理
	void Start () {
		
		currentTipIndex = startTipIndex - 1;
		UpdateStage(preInstantiate);

	}
	
	// ステージの更新タイミングの監視
	void Update () {
	
		//キャラクターの位置から現在のステージチップのインデックスを計算
		int charaPositionIndex = (int)(character.position.z / StageTipSize);

		//次のステージチップに入ったらステージの更新処理をおこなう
		if (charaPositionIndex + preInstantiate > currentTipIndex) {

			UpdateStage (charaPositionIndex + preInstantiate);

		}

	}

	//指定のIndexまでのステージチップを生成して、管理下に置く
	void UpdateStage (int toTipIndex){

		if (toTipIndex <= currentTipIndex) return;

		//指定のステージチップまでを作成
		for (int i = currentTipIndex + 1; i <= toTipIndex; i++) {

			GameObject stageObject = GenerateStage(i);

			//生成したステージチップを管理リストに追加
			generatedStageList.Add(stageObject);

		}

		//ステージ保持上限内になるまで古いステージを削除
		while(generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

		currentTipIndex = toTipIndex;
	
	}

	//指定のインデックス位置にStageオブジェクトをランダムに生成
	GameObject GenerateStage(int tipIndex){

		int nextStageTip = Random.Range (0, stageTips.Length);

		GameObject stageObject = (GameObject)Instantiate (
			stageTips[nextStageTip],
			new Vector3(0, 0, tipIndex * StageTipSize),
			Quaternion.identity
		);

		return stageObject;

	}

	//一番古いステージを削除
	void DestroyOldestStage(){
		
		GameObject oldStage = generatedStageList[0];
		generatedStageList.RemoveAt (0);
		Destroy(oldStage);

	}

}
