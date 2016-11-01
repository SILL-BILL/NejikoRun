using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //シーン遷移

public class TitleController : MonoBehaviour {

	public void OnStartButtonClicked(){

		SceneManager.LoadScene("Main");

	}

}
