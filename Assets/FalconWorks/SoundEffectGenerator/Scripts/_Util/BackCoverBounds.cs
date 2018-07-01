using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackCoverBounds : MonoBehaviour {

	// --------
	#region インスペクタ設定用フィールド
	/// <summary>
	/// The preferred t.
	/// </summary>
	[SerializeField] private LayoutElement m_PreferredT;
	/// <summary>
	/// The preferred c.
	/// </summary>
	[SerializeField] private LayoutElement m_PreferredC;
	/// <summary>
	/// The panel.
	/// </summary>
	[SerializeField] private RectTransform m_Panel;
	#endregion

	// --------
	#region メンバフィールド
	/// <summary>
	/// The size of the current panel(Vector2)
	/// </summary>
	private Vector2 m_CurrentPanelSize;
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

	}
	/// <summary> 
	/// 更新処理
	/// </summary>
	void Update () {

		if((m_CurrentPanelSize.x != m_Panel.rect.width) && (m_CurrentPanelSize.y != m_Panel.rect.height)){

			m_CurrentPanelSize.x = m_Panel.rect.width;
			m_CurrentPanelSize.y = m_Panel.rect.height;

			m_PreferredC.preferredWidth = m_CurrentPanelSize.x;
			m_PreferredT.preferredHeight = m_CurrentPanelSize.y;

			Debug.Log ("MOB^m_CurrentPanelSize.x："+m_CurrentPanelSize.x);
			Debug.Log ("MOB^m_CurrentPanelSize.y："+m_CurrentPanelSize.y);
		}

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

}
