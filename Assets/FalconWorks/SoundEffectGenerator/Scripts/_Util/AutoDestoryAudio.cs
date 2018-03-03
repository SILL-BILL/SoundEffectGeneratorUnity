using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestoryAudio : MonoBehaviour {

	// --------
	#region インスペクタ設定用フィールド
	#endregion

	// --------
	#region メンバフィールド
	/// <summary> 
	/// オーディオソース
	/// </summary>
	private AudioSource audio;
	private float endTime;
	#endregion

	// --------
	#region MonoBehaviourメソッド
	/// <summary> 
	/// 初期化処理
	/// </summary>
	void Awake() {

	}
	/// <summary> 
	/// 開始処理
	/// </summary>
	void Start () {
		audio = this.GetComponent<AudioSource>();
		endTime = audio.clip.length;
		Debug.Log (endTime);
		Destroy (this.gameObject, endTime);
	}
	/// <summary> 
	/// 更新処理
	/// </summary>
	void Update () {

	}

	/// <summary> 
	/// 更新処理
	/// </summary>
	void LateUpdate(){

	}
	#endregion

	// --------
	#region メンバメソッド
	#endregion

	// --------
	#region インナークラス
	#endregion

}
