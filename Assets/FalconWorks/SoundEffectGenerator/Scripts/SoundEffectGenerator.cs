using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectGenerator : MonoBehaviour {

	// --------
	#region インスペクタ設定用フィールド
	/// <summary>
	/// The pet prefab table.
	/// </summary>
	[SerializeField] private SoundEffectPrefabTable soundEffectPrefabTable;
	#endregion

	// --------
	#region メンバフィールド
	/// <summary>
	/// The pet prefabs.
	/// </summary>
	private Dictionary<string, GameObject> soundEffectPrefabs;
	#endregion

	// --------
	#region MonoBehaviourメソッド
	/// <summary> 
	/// 初期化処理
	/// </summary>
	void Awake() {
		soundEffectPrefabs = soundEffectPrefabTable.GetTable ();
	}
	/// <summary> 
	/// 開始処理
	/// </summary>
	void Start () {

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
	/// <summary>
	/// Popin the SoundEffect.
	/// </summary>
	/// <param name="_keyName">Key name.</param>
	public void popin(string _keyName){

		if (!string.IsNullOrEmpty(_keyName) && soundEffectPrefabs.ContainsKey(_keyName)) {

			Instantiate (
				soundEffectPrefabs[_keyName],
				this.transform.position,
				Quaternion.identity,
				this.transform
			);
		}
	}
	#endregion

	// --------
	#region インナークラス
	/// <summary>
	/// ジェネリックを隠すために継承してしまう
	/// [System.Serializable]を書くのを忘れない
	/// </summary>
	[System.Serializable]
	public class SoundEffectPrefabTable : Serialize.TableBase<string, GameObject, SoundEffectPrefabPair>{

	}
	/// <summary>
	/// ジェネリックを隠すために継承してしまう
	/// [System.Serializable]を書くのを忘れない
	/// </summary>
	[System.Serializable]
	public class SoundEffectPrefabPair : Serialize.KeyAndValue<string, GameObject>{
		public SoundEffectPrefabPair (string key, GameObject value) : base (key, value) {
		}
	}
	#endregion
}
