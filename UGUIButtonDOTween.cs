using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public sealed class UGUIButtonDOTween : MonoBehaviour
{
	BaseButton baseButton => GetComponent<BaseButton> ();

	// どのボタンクラスを使うか設定するボタン
	// * 設定したら非表示にする
	[Button (ButtonSizes.Medium),
	 HideIfGroup ("InitBtns", MemberName = "baseButton"),
	 ButtonGroup ("InitBtns/Btn")]
	public void UseETButton () =>
		gameObject.AddComponent<ETButton> ();

	[Button (ButtonSizes.Medium),
	 ButtonGroup ("InitBtns/Btn")]
	public void UsePointerButton () =>
		gameObject.AddComponent<PointerButton> ();

	// 共通
	[SerializeField, ToggleLeft, LabelText ("Target"), LabelWidth (25),
	 ShowIfGroup     ("Param", MemberName = "baseButton"),
	 BoxGroup        ("Param/Common", centerLabel : true),
	 HorizontalGroup ("Param/Common/G1", 0.5f)]
	bool target = true;

	[SerializeField, ShowIf ("target"), HideLabel,
	 BoxGroup        ("Param/Common"),
	 HorizontalGroup ("Param/Common/G1")]
	GameObject commonTarget;

	[SerializeField, ToggleLeft, LabelText ("Duration"), LabelWidth (20),
	 HorizontalGroup ("Param/Common/G2", 0.5f),
	 BoxGroup        ("Param/Common/G2/Push", centerLabel : true),
	 HorizontalGroup ("Param/Common/G2/Push/L", 0.25f)]
	bool pushDuration = true;

	[SerializeField, ShowIf ("pushDuration"), HideLabel,
	 BoxGroup        ("Param/Common/G2/Push"),
	 HorizontalGroup ("Param/Common/G2/Push/L")]
	float commonPushDuration = 0.2f;

	[SerializeField, ToggleLeft, LabelText ("Easing"), LabelWidth (20),
	 BoxGroup        ("Param/Common/G2/Push"),
	 HorizontalGroup ("Param/Common/G2/Push/R", 0.25f)]
	bool pushEasing = true;

	[SerializeField, ShowIf ("pushEasing"), HideLabel,
	 BoxGroup        ("Param/Common/G2/Push"),
	 HorizontalGroup ("Param/Common/G2/Push/R")]
	Ease commonPushEasing = Ease.OutQuint;

	[SerializeField, ToggleLeft, LabelText ("Duration"), LabelWidth (20),
	 HorizontalGroup ("Param/Common/G2", 0.5f),
	 BoxGroup        ("Param/Common/G2/Release", centerLabel : true),
	 HorizontalGroup ("Param/Common/G2/Release/L", 0.25f)]
	bool releaseDuration = true;

	[SerializeField, ShowIf ("releaseDuration"), HideLabel,
	 BoxGroup        ("Param/Common/G2/Release"),
	 HorizontalGroup ("Param/Common/G2/Release/L")]
	float commonReleaseDuration = 0.2f;

	[SerializeField, ToggleLeft, LabelText ("Easing"), LabelWidth (20),
	 BoxGroup        ("Param/Common/G2/Release"),
	 HorizontalGroup ("Param/Common/G2/Release/R", 0.25f)]
	bool releaseEasing = true;

	[SerializeField, ShowIf ("releaseEasing"), HideLabel,
	 BoxGroup        ("Param/Common/G2/Release"),
	 HorizontalGroup ("Param/Common/G2/Release/R")]
	Ease commonReleaseEasing = Ease.OutQuint;

	bool UseCommonPush    => pushDuration    && pushEasing;
	bool UseCommonRelease => releaseDuration && releaseEasing;

	// ツイーン項目
	[SerializeField, ToggleLeft, LabelWidth (15),
	 BoxGroup        ("Param/Items", false),
	 HorizontalGroup ("Param/Items/List")]
	bool fade = false, move = false, rotate = false, scale = false;

	// フェード
	[SerializeField, LabelWidth (125),
	 ShowIfGroup     ("Param/fade"),
	 BoxGroup        ("Param/fade/Fade", centerLabel : true)]
	float fadeAlpha = 1;

	[SerializeField, LabelWidth (125),
	 BoxGroup        ("Param/fade/Fade", centerLabel : true)]
	Sprite fadeSprite;

	[SerializeField, HideIf ("fadeSprite"), LabelWidth (125),
	 BoxGroup        ("Param/fade/Fade", centerLabel : true)]
	Color fadeColor = new Color (0, 0, 0, 1);

	[SerializeField, LabelWidth (125),
	 BoxGroup        ("Param/fade/Fade", centerLabel : true)]
	bool fadeFront = false;

	[SerializeField, HideIf ("target"), LabelText ("Target"), LabelWidth (125),
	 BoxGroup        ("Param/fade/Fade", centerLabel : true)]
	GameObject fTarget;

	[SerializeField, HideIf ("pushDuration"), LabelText ("Duration"), LabelWidth (55),
	 HorizontalGroup ("Param/fade/Fade/G", 0.5f),
	 HideIfGroup     ("Param/fade/Fade/G/L", MemberName = "UseCommonPush"),
	 BoxGroup        ("Param/fade/Fade/G/L/Push")]
	float fPushDuration = 0.2f;

	[SerializeField, HideIf ("pushEasing"), LabelText ("Easing"), LabelWidth (55),
	 HorizontalGroup ("Param/fade/Fade/G", 0.5f),
	 BoxGroup        ("Param/fade/Fade/G/L/Push")]
	Ease fPushEasing = Ease.OutQuint;

	[SerializeField, HideIf ("releaseDuration"), LabelText ("Duration"), LabelWidth (55),
	 HorizontalGroup ("Param/fade/Fade/G", 0.5f),
	 HideIfGroup     ("Param/fade/Fade/G/R", MemberName = "UseCommonRelease"),
	 BoxGroup        ("Param/fade/Fade/G/R/Release")]
	float fReleaseDuration = 0.2f;

	[SerializeField, HideIf ("releaseEasing"), LabelText ("Easing"), LabelWidth (55),
	 HorizontalGroup ("Param/fade/Fade/G", 0.5f),
	 BoxGroup        ("Param/fade/Fade/G/R/Release")]
	Ease fReleaseEasing = Ease.OutQuint;

	// 移動
	[SerializeField, HideLabel,
	 ShowIfGroup     ("Param/move"),
	 BoxGroup        ("Param/move/Move To", centerLabel : true)]
	Vector3 pushPosition = Vector3.zero;

	[SerializeField, HideIf ("target"), LabelText ("Target"), LabelWidth (125),
	 BoxGroup        ("Param/move/Move To", centerLabel : true)]
	GameObject mTarget;

	[SerializeField, HideIf ("pushDuration"), LabelText ("Duration"), LabelWidth (55),
	 HorizontalGroup ("Param/move/Move To/G", 0.5f),
	 HideIfGroup     ("Param/move/Move To/G/L", MemberName = "UseCommonPush"),
	 BoxGroup        ("Param/move/Move To/G/L/Push")]
	float mPushDuration = 0.2f;

	[SerializeField, HideIf ("pushEasing"), LabelText ("Easing"), LabelWidth (55),
	 HorizontalGroup ("Param/move/Move To/G", 0.5f),
	 BoxGroup        ("Param/move/Move To/G/L/Push")]
	Ease mPushEasing = Ease.OutQuint;

	[SerializeField, HideIf ("releaseDuration"), LabelText ("Duration"), LabelWidth (55),
	 HorizontalGroup ("Param/move/Move To/G", 0.5f),
	 HideIfGroup     ("Param/move/Move To/G/R", MemberName = "UseCommonRelease"),
	 BoxGroup        ("Param/move/Move To/G/R/Release")]
	float mReleaseDuration = 0.2f;

	[SerializeField, HideIf ("releaseEasing"), LabelText ("Easing"), LabelWidth (55),
	 HorizontalGroup ("Param/move/Move To/G", 0.5f),
	 BoxGroup        ("Param/move/Move To/G/R/Release")]
	Ease mReleaseEasing = Ease.OutQuint;

	// 回転
	[SerializeField, HideLabel,
	 ShowIfGroup     ("Param/rotate"),
	 BoxGroup        ("Param/rotate/Rotate To", centerLabel : true)]
	Vector3 pushRotation = Vector3.zero;

	[SerializeField, HideIf ("target"), LabelText ("Target"), LabelWidth (125),
	 BoxGroup        ("Param/rotate/Rotate To", centerLabel : true)]
	GameObject rTarget;

	[SerializeField, HideIf ("pushDuration"), LabelText ("Duration"), LabelWidth (55),
	 HorizontalGroup ("Param/rotate/Rotate To/G", 0.5f),
	 HideIfGroup     ("Param/rotate/Rotate To/G/L", MemberName = "UseCommonPush"),
	 BoxGroup        ("Param/rotate/Rotate To/G/L/Push")]
	float rPushDuration = 0.2f;

	[SerializeField, HideIf ("pushEasing"), LabelText ("Easing"), LabelWidth (55),
	 HorizontalGroup ("Param/rotate/Rotate To/G", 0.5f),
	 BoxGroup        ("Param/rotate/Rotate To/G/L/Push")]
	Ease rPushEasing = Ease.OutQuint;

	[SerializeField, HideIf ("releaseDuration"), LabelText ("Duration"), LabelWidth (55),
	 HorizontalGroup ("Param/rotate/Rotate To/G", 0.5f),
	 HideIfGroup     ("Param/rotate/Rotate To/G/R", MemberName = "UseCommonRelease"),
	 BoxGroup        ("Param/rotate/Rotate To/G/R/Release")]
	float rReleaseDuration = 0.2f;

	[SerializeField, HideIf ("releaseEasing"), LabelText ("Easing"), LabelWidth (55),
	 HorizontalGroup ("Param/rotate/Rotate To/G", 0.5f),
	 BoxGroup        ("Param/rotate/Rotate To/G/R/Release")]
	Ease rReleaseEasing = Ease.OutQuint;

	// スケール
	[SerializeField, HideLabel,
	 ShowIfGroup     ("Param/scale"),
	 BoxGroup        ("Param/scale/Scale To", centerLabel : true)]
	Vector3 pushScale = Vector3.one;

	[SerializeField, HideIf ("target"), LabelText ("Target"), LabelWidth (125),
	 BoxGroup        ("Param/scale/Scale To", centerLabel : true)]
	GameObject sTarget;

	[SerializeField, HideIf ("pushDuration"), LabelText ("Duration"), LabelWidth (55),
	 HorizontalGroup ("Param/scale/Scale To/G", 0.5f),
	 HideIfGroup     ("Param/scale/Scale To/G/L", MemberName = "UseCommonPush"),
	 BoxGroup        ("Param/scale/Scale To/G/L/Push")]
	float sPushDuration = 0.2f;

	[SerializeField, HideIf ("pushEasing"), LabelText ("Easing"), LabelWidth (55),
	 HorizontalGroup ("Param/scale/Scale To/G", 0.5f),
	 BoxGroup        ("Param/scale/Scale To/G/L/Push")]
	Ease sPushEasing = Ease.OutQuint;

	[SerializeField, HideIf ("releaseDuration"), LabelText ("Duration"), LabelWidth (55),
	 HorizontalGroup ("Param/scale/Scale To/G", 0.5f),
	 HideIfGroup     ("Param/scale/Scale To/G/R", MemberName = "UseCommonRelease"),
	 BoxGroup        ("Param/scale/Scale To/G/R/Release")]
	float sReleaseDuration = 0.2f;

	[SerializeField, HideIf ("releaseEasing"), LabelText ("Easing"), LabelWidth (55),
	 HorizontalGroup ("Param/scale/Scale To/G", 0.5f),
	 BoxGroup        ("Param/scale/Scale To/G/R/Release")]
	Ease sReleaseEasing = Ease.OutQuint;

	float    baseAlpha    = 0;
	Vector3  basePosition = Vector3.one;
	Vector3  baseScale    = Vector3.one;
	Vector3  baseRotation = Vector3.one;

	Sequence   movieSeq;
	GameObject fadeObj;
	bool       touching = false;

	void Reset ()
	{
		// targetを自動設定
		if (commonTarget == null) commonTarget = gameObject;
		if (fTarget      == null) fTarget      = gameObject;
		if (mTarget      == null) mTarget      = gameObject;
		if (rTarget      == null) rTarget      = gameObject;
		if (sTarget      == null) sTarget      = gameObject;
	}

	void Awake ()
	{
		if (target) fTarget = mTarget = rTarget = sTarget = commonTarget;

		// 初期形状を記録
		if (move)   basePosition = mTarget.transform.localPosition;
		if (rotate) baseRotation = rTarget.transform.localEulerAngles;
		if (scale)  baseScale    = sTarget.transform.localScale;

		// タッチイベントを設定
		baseButton?.RegisterTouchEvent   (TouchDown);
		baseButton?.RegisterClickEvent   (TouchUp);
		baseButton?.RegisterRolloutEvent (RollOut);
	}

	// 押下時に形状を変化
	public void TouchDown ()
	{
		movieSeq.Kill (true);

		// Common設定
		if (pushDuration)
			fPushDuration = mPushDuration = rPushDuration = sPushDuration = commonPushDuration;
		if (pushEasing)
			fPushEasing   = mPushEasing   = rPushEasing   = sPushEasing   = commonPushEasing;

		// フェード画像を作成
		if (fade && fadeObj == null)
		{
			fadeObj = new GameObject ("FadeObj");
			fadeObj.AddComponent<Image> ();
			fadeObj.AddComponent<CanvasGroup> ();
			fadeObj.transform.SetParent (fTarget.transform);
			fadeObj.transform.localPosition = Vector3.zero;
			if (fadeFront) fadeObj.transform.SetAsLastSibling ();
			else           fadeObj.transform.SetAsFirstSibling ();

			// スプライトを使うかカラーを使うかで設定を分ける
			if (fadeSprite == null)
			{
				fadeObj.GetComponent<RectTransform> ().sizeDelta =
					fTarget.GetComponent<RectTransform> ().sizeDelta;
				fadeObj.GetComponent<Image> ().color = fadeColor;
			}
			else
			{
				fadeObj.GetComponent<RectTransform> ().sizeDelta =
					new Vector2 (fadeSprite.bounds.size.x, fadeSprite.bounds.size.y);
				fadeObj.GetComponent<Image> ().sprite = fadeSprite;
			}
		}

		// フェード画像は透明度を0にしておく
		if (fade)   fadeObj.GetComponent<CanvasGroup> ().alpha = 0;
		if (fade)   fadeObj.SetActive (true);

		// ツイーンを再生
		touching = true;

		movieSeq = DOTween.Sequence ();

		if (fade)
			movieSeq.Join (DOTween.Sequence ()
				.Join (fadeObj.GetComponent<CanvasGroup> ()
					.DOFade      (fadeAlpha,    fPushDuration).SetEase (fPushEasing)));
		if (move)
			movieSeq.Join (DOTween.Sequence ()
				.Join (mTarget.transform
					.DOLocalMove (pushPosition, mPushDuration).SetEase (mPushEasing)));
		if (rotate)
			movieSeq.Join (DOTween.Sequence ()
				.Join (rTarget.transform
					.DORotate    (pushRotation, rPushDuration).SetEase (rPushEasing)));
		if (scale)
			movieSeq.Join (DOTween.Sequence ()
				.Join (sTarget.transform
					.DOScale     (pushScale   , sPushDuration).SetEase (sPushEasing)));

		movieSeq.Play ();
	}

	// 押下時に初期形状に戻す
	public void TouchUp ()
	{
		movieSeq.Kill (true);

		// Common設定
		if (releaseDuration)
			fReleaseDuration = mReleaseDuration = rReleaseDuration = sReleaseDuration = commonReleaseDuration;
		if (releaseEasing)
			fReleaseEasing   = mReleaseEasing   = rReleaseEasing   = sReleaseEasing   = commonReleaseEasing;

		movieSeq = DOTween.Sequence ()
			.OnComplete (() => { ResetTransform (); });

		touching = false;

		if (fade)
			movieSeq.Join (DOTween.Sequence ()
				.Join (fadeObj.GetComponent<CanvasGroup> ()
					.DOFade      (baseAlpha,    fReleaseDuration).SetEase (fReleaseEasing))
		);
		if (move)
			movieSeq.Join (DOTween.Sequence ()
				.Join (mTarget.transform
					.DOLocalMove (basePosition, mReleaseDuration).SetEase (mReleaseEasing))
		);
		if (rotate)
			movieSeq.Join (DOTween.Sequence ()
				.Join (rTarget.transform
					.DORotate    (baseRotation, rReleaseDuration).SetEase (rReleaseEasing))
		);
		if (scale)
			movieSeq.Join (DOTween.Sequence ()
				.Join (sTarget.transform
					.DOScale     (baseScale   , sReleaseDuration).SetEase (sReleaseEasing))
		);

		movieSeq.Play ();
	}

	// 押下しながらロールアウト時は初期形状に戻す
	public void RollOut ()
	{
		if (touching)
		{
			movieSeq.Kill (true);
			ResetTransform ();
		}
	}

	// 初期状態に戻す
	void ResetTransform ()
	{
		if (fade && fadeObj != null)
		{
			fadeObj.SetActive (false);
			fadeObj.GetComponent<CanvasGroup> ().alpha = baseAlpha;
		}

		if (move)   mTarget.transform.localPosition    = basePosition;
		if (rotate) rTarget.transform.localEulerAngles = baseRotation;
		if (scale)  sTarget.transform.localScale       = baseScale;
	}

	// 非アクティブになったときはツイーンを止めて初期形状に戻す
	public void OnDisable ()
	{
		movieSeq.Kill (true);
		ResetTransform ();
	}
}