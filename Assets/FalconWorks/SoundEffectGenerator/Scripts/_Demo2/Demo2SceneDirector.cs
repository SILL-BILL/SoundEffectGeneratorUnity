using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo2SceneDirector : BaseSceneDirector {

	// --------
	#region インスペクタ設定用フィールド
[Header("*SE Settings")]
	/// <summary> 
	/// 
	/// </summary>
	[SerializeField] protected SoundEffectGenerator m_SoundEffectGenerator;
	/// <summary> 
	/// 
	/// </summary>
	[SerializeField] protected float m_TimeOut = 2.5f;
	/// <summary> 
	/// 
	/// </summary>
	[SerializeField] protected string[] m_SeDictkeyNames;
	#endregion

	// --------
	#region メンバフィールド
	/// <summary> 
	/// 
	/// </summary>
	protected float m_TimeElapsed;
	protected int m_CurrentKey;
	#endregion

	// --------
	#region MonoBehaviourメソッド
	/// <summary> 
	/// 初期化処理
	/// </summary>
	protected override void Awake() {
		base.Awake();
	}
	/// <summary> 
	/// 開始処理
	/// </summary>
	protected override void Start () {
		base.Start();
	}
	/// <summary> 
	/// 更新処理
	/// </summary>
	protected override void Update () {

		m_TimeElapsed += Time.deltaTime;

		if(m_TimeElapsed >= m_TimeOut){
			if(m_CurrentKey >= m_SeDictkeyNames.Length) m_CurrentKey = 0;
			m_SoundEffectGenerator.popin(m_SeDictkeyNames[m_CurrentKey]);
			m_TimeElapsed = 0.0f;
			m_CurrentKey++;
		}

		base.Update();
	}

	/// <summary> 
	/// 更新処理
	/// </summary>
	protected override void FixedUpdate(){
		base.FixedUpdate();
	}

	/// <summary> 
	/// 更新処理
	/// </summary>
	protected override void LateUpdate(){
		base.LateUpdate();
	}
	#endregion

	// --------
	#region メンバメソッド
	#endregion

	// --------
	#region インナークラス
	#endregion

}
