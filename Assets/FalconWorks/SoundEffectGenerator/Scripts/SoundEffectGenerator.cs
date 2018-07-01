using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectGenerator : MonoBehaviour {

	// --------
	#region インスペクタ設定用フィールド
	/// <summary>
	/// The pet prefab table.
	/// </summary>
	[SerializeField] private SoundEffectPrefabDataTable m_SoundEffectPrefabDataTable;
	#endregion

	// --------
	#region メンバフィールド
	/// <summary>
	/// The se prefabs.
	/// </summary>
	private Dictionary<string, SoundEffectPrefabData> m_SoundEffectPrefabDataDict;
	public Dictionary<string, SoundEffectPrefabData> SoundEffectPrefabDataDict {
		get { return m_SoundEffectPrefabDataDict; }
		private set { m_SoundEffectPrefabDataDict = value; }
	}
	#endregion

	// --------
	#region MonoBehaviourメソッド
	/// <summary> 
	/// 初期化処理
	/// </summary>
	void Awake() {
		m_SoundEffectPrefabDataDict = m_SoundEffectPrefabDataTable.GetTable ();
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

		if (!string.IsNullOrEmpty(_keyName) && m_SoundEffectPrefabDataDict.ContainsKey(_keyName)) {

			GameObject effObj;
			Vector3 pos = Vector3.zero;
			if(m_SoundEffectPrefabDataDict[_keyName].m_PopinPosition != null){
				pos = m_SoundEffectPrefabDataDict [_keyName].m_PopinPosition;
			}


			if (m_SoundEffectPrefabDataDict [_keyName].m_PopinPosition != null) {
			
				effObj = (GameObject)Instantiate (
					m_SoundEffectPrefabDataDict [_keyName].m_SoundEffectPrefab,
					Vector3.zero,
					Quaternion.identity,
					m_SoundEffectPrefabDataDict [_keyName].m_ParentTransform
				);
				effObj.transform.localPosition = pos;

			} else {

				Instantiate (
					m_SoundEffectPrefabDataDict [_keyName].m_SoundEffectPrefab,
					pos,
					Quaternion.identity,
					this.transform
				);
			}


		}
	}
	#endregion

	// --------
	#region インナークラス
	/// <summary>
	/// SoundEffectPrefabData
	/// </summary>
	[System.SerializableAttribute]
	public class SoundEffectPrefabData {
		/// <summary>
		/// SEプレファブ
		/// </summary>
		public GameObject m_SoundEffectPrefab;
		/// <summary>
		/// The popin position.
		/// </summary>
		public Vector3 m_PopinPosition;
		/// <summary>
		/// The parent transform.
		/// </summary>
		public Transform m_ParentTransform;

		//コンストラクタ
		public SoundEffectPrefabData(){
		}
	}

	/// <summary>
	/// ジェネリックを隠すために継承してしまう
	/// [System.Serializable]を書くのを忘れない
	/// </summary>
	[System.Serializable]
	public class SoundEffectPrefabDataTable : Serialize.TableBase<string, SoundEffectPrefabData, SoundEffectPrefabDataPair>{

	}
	/// <summary>
	/// ジェネリックを隠すために継承してしまう
	/// [System.Serializable]を書くのを忘れない
	/// </summary>
	[System.Serializable]
	public class SoundEffectPrefabDataPair : Serialize.KeyAndValue<string, SoundEffectPrefabData>{
		public SoundEffectPrefabDataPair (string key, SoundEffectPrefabData value) : base (key, value) {
		}
	}
	#endregion
}
