using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BaseSceneDirector : MonoBehaviour {

	// --------
	#region インスペクタ設定用フィールド
	[Header ("*Initial Display Fade Settings")]
	/// <summary>
	/// 初期表示時のフェード処理の時間
	/// </summary>
	[SerializeField] protected float initialDisplayFadeDurationTime;
	/// <summary>
	/// 初期表示時のフェードの色
	/// </summary>
	[SerializeField] protected Color initialDisplayFadeColor;

	[Header ("*Scene Translate Settings")]
	/// <summary>
	/// フェード開始までの遅延時間
	/// </summary>
	[SerializeField] protected float fadeStartDelayTime;
	/// <summary>
	/// フェード時間
	/// </summary>
	public float fadeDurationTime;
	/// <summary>
	/// フェード後の待ち時間
	/// </summary>
	public float waitTimeAfterFade;
	/// <summary>
	/// フェードカラー
	/// </summary>
	public Color fadeColor;
	/// <summary>
	/// イベントシステム格納フィールド
	/// </summary>
	[SerializeField] protected EventSystem m_EventSystem;

	[Header ("*Screen AutoRotaion Settings")]
	/// <summary>
	/// 画面自動回転を自動的に有効化するフラグ
	/// </summary>
	[SerializeField] protected bool m_AutoSetupToScreenAutoRotation = false;
	/// <summary>
	/// The allowed autorotate to portrait.
	/// </summary>
	[SerializeField] protected bool m_AllowedAutorotateToPortrait = true;
	/// <summary>
	/// The allowed autorotate to portrait upside down.
	/// </summary>
	[SerializeField] protected bool m_AllowedAutorotateToPortraitUpsideDown = false;
	/// <summary>
	/// The allowed autorotate to landscape left.
	/// </summary>
	[SerializeField] protected bool m_AllowedAutorotateToLandscapeLeft = true;
	/// <summary>
	/// The allowed autorotate to landscape right.
	/// </summary>
	[SerializeField] protected bool m_AllowedAutorotateToLandscapeRight = true;
	[Header("*Touch Settings")]
	/// <summary>
	/// マルチタッチ有効化フラグ
	/// </summary>
	[SerializeField] protected bool m_multiTouchEnabled = true;
	#endregion

	// --------
	#region メンバフィールド
	/// <summary>
	/// The scene translate flag.
	/// </summary>
	protected bool sceneTranslatingFlag = false;
	/// <summary>
	/// delegate型を宣言
	/// </summary>
	public delegate void OnComplete();  // delegate
	protected OnComplete callBack;		// コールバック
	#endregion

	// --------
	#region MonoBehaviourメソッド
	/// <summary>
	/// 初期化処理
	/// </summary>
	protected virtual void Awake(){

		//マルチタッチ有効・無効の設定
		Input.multiTouchEnabled = m_multiTouchEnabled;

		if(m_AutoSetupToScreenAutoRotation){
			setupToScreenAutoRotaion ();
		}
	}

	/// <summary>
	/// 開始処理
	/// </summary>
	protected virtual void Start () {
		ScreenFadeManager.Instance.FadeIn (initialDisplayFadeDurationTime, initialDisplayFadeColor, ()=>{
		});
	}

	/// <summary>
	/// 更新処理
	/// </summary>
	protected virtual void Update () {

	}

	/// <summary>
	/// 更新処理
	/// </summary>
	protected virtual void LateUpdate () {

	}

	/// <summary>
	/// 更新処理
	/// </summary>
	protected virtual void FixedUpdate () {

	}
	#endregion

	// --------
	#region メンバメソッド
	/// <summary>
	/// 画面自動回転セットアップ関数
	/// </summary>
	public virtual void setupToScreenAutoRotaion(){

		Screen.autorotateToLandscapeLeft      = m_AllowedAutorotateToLandscapeLeft;
		Screen.autorotateToLandscapeRight     = m_AllowedAutorotateToLandscapeRight;
		Screen.autorotateToPortrait           = m_AllowedAutorotateToPortrait;
		Screen.autorotateToPortraitUpsideDown = m_AllowedAutorotateToPortraitUpsideDown;

		Screen.orientation = ScreenOrientation.AutoRotation;
	}

	/// <summary>
	/// Translates the scene.
	/// </summary>
	/// <param name="_sceneName">Scene name.</param>
	public virtual void translateScene(string _sceneName){

		if(!sceneTranslatingFlag){

			sceneTranslatingFlag = true;

			if(m_EventSystem != null){
				m_EventSystem.enabled = false;
			}

			StartCoroutine(waitTimer(fadeStartDelayTime, ()=>{

				ScreenFadeManager.Instance.FadeOut (fadeDurationTime, fadeColor, () => {
					StartCoroutine(_translateScene(_sceneName, waitTimeAfterFade));
				});

			}));
		}

	}

	/// <summary>
	/// Translates the scene.
	/// </summary>
	/// <returns>The scene.</returns>
	/// <param name="_sceneName">Scene name.</param>
	/// <param name="_waitTime">Wait time.</param>
	protected IEnumerator _translateScene(string _sceneName, float _waitTime = 0){

		yield return new WaitForSeconds (_waitTime);

		SceneManager.LoadScene(_sceneName);
		if(m_EventSystem != null){
			m_EventSystem.enabled = true;
		}
		sceneTranslatingFlag = false;
	}

	/// <summary>
	/// Translates the scene landscape.
	/// </summary>
	/// <param name="_sceneName">Scene name.</param>
	public virtual void translateSceneLandscape(string _sceneName){

		if(!sceneTranslatingFlag){

			sceneTranslatingFlag = true;

			if(m_EventSystem != null){
				m_EventSystem.enabled = false;
			}

			StartCoroutine(waitTimer(fadeStartDelayTime, ()=>{
				ScreenFadeManager.Instance.FadeOut (fadeDurationTime, fadeColor, () => {
					StartCoroutine(_translateSceneLandscape(_sceneName, waitTimeAfterFade));
				});
			}));
		}

	}

	protected IEnumerator _translateSceneLandscape(string _sceneName, float _waitTime = 0){

		yield return new WaitForSeconds(_waitTime);

		//強制的にLandscapeに変更してからシーン遷移
		Screen.orientation = ScreenOrientation.Landscape;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeLeft  = true;
		Screen.autorotateToLandscapeRight = true;

		SceneManager.LoadScene(_sceneName);
		if (m_EventSystem != null){
			m_EventSystem.enabled = true;
		}
		sceneTranslatingFlag = false;

	}

	/// <summary>
	/// Translates the scene Portrait.
	/// </summary>
	/// <param name="_sceneName">Scene name.</param>
	public virtual void translateScenePortrait(string _sceneName){

		if(!sceneTranslatingFlag){

			sceneTranslatingFlag = true;

			if(m_EventSystem != null){
				m_EventSystem.enabled = false;
			}

			StartCoroutine(waitTimer(fadeStartDelayTime, ()=>{
				ScreenFadeManager.Instance.FadeOut (fadeDurationTime, fadeColor, () => {
					StartCoroutine(_translateScenePortrait(_sceneName, waitTimeAfterFade));
				});
			}));
		}

	}

	protected IEnumerator _translateScenePortrait(string _sceneName, float _waitTime = 0){

		yield return new WaitForSeconds(_waitTime);

		//強制的にPortraitに変更してからシーン遷移
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeLeft  = false;
		Screen.autorotateToLandscapeRight = false;

		SceneManager.LoadScene(_sceneName);
		if (m_EventSystem != null){
			m_EventSystem.enabled = true;
		}
		sceneTranslatingFlag = false;

	}

	/// <summary>
	/// Waits the timer.
	/// </summary>
	/// <returns>The timer.</returns>
	/// <param name="waitTime">Wait time.</param>
	/// <param name="t_cb">T cb.</param>
	protected IEnumerator waitTimer(float waitTime, OnComplete t_cb = default(OnComplete)){
		callBack = t_cb;
		yield return new WaitForSeconds (waitTime);

		callBack ();
	}
	#endregion
}