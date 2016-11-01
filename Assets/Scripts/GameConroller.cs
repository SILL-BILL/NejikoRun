using UnityEngine;
using System.Collections;
using UnityEngine.UI; //UI名前空間のインポート
using UnityEngine.SceneManagement; //シーン遷移

public class GameConroller : MonoBehaviour {

	public NejikoController nejiko;
	public Text scoreLabel; //ScoreLabelの参照
	public LifePanel lifePanel;

	// Update is called once per frame
	void Update () {
	
		//スコアラベルを更新
		int score = CalcScore();
		scoreLabel.text = "Score : " + score + "m";

		//ライフパネルを更新
		lifePanel.UpdateLife(nejiko.Life());

		//ねじ子のライフが0になったらゲームオーバー
		if (nejiko.Life () <= 0) {

			//これ以降はUpdateは止める
			enabled = false;

			//2秒後にReturnToTitleを呼び出す
			Invoke("ReturnToTitle", 2.0f);

		}

	}

	int CalcScore(){

		//ねじ子の走行距離をスコアとする
		return (int)nejiko.transform.position.z;

	}

	void ReturnToTitle(){

		//タイトルシーンに切り替え
		SceneManager.LoadScene("Title");

	}
}
