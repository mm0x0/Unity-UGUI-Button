using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// PointerHandlerを実装したボタン
public sealed class PointerButton : BaseButton, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
	enum TouchType { Touch, Click, RollOver, RollOut }

	UnityAction TouchEvent, ClickEvent, RollOverEvent, RollOutEvent;

	// タップイベントを設定
	public override void RegisterTouchEvent (UnityAction f) =>
		RegisterEvent (f, TouchType.Touch);

	// クリックイベントを設定
	public override void RegisterClickEvent (UnityAction f) =>
		RegisterEvent (f, TouchType.Click);

	// ロールオーバーイベントを設定
	public override void RegisterRolloverEvent (UnityAction f) =>
		RegisterEvent (f, TouchType.RollOver);

	// ロールアウトイベントを設定
	public override void RegisterRolloutEvent (UnityAction f) =>
		RegisterEvent (f, TouchType.RollOut);

	// タッチイベントの設定（重複は削除）
	void RegisterEvent (UnityAction f, TouchType type)
	{
		switch (type)
		{
			case TouchType.Touch    :
				TouchEvent    -= f;
				TouchEvent    += f;
				break;
			case TouchType.Click    :
				ClickEvent    -= f;
				ClickEvent    += f;
				break;
			case TouchType.RollOver :
				RollOverEvent -= f;
				RollOverEvent += f;
				break;
			case TouchType.RollOut  :
				RollOutEvent  -= f;
				RollOutEvent  += f;
				break;
		}
	}
	public void OnPointerClick (PointerEventData eventData) => ClickEvent   ?.Invoke ();
	public void OnPointerDown  (PointerEventData eventData) => TouchEvent   ?.Invoke ();
	public void OnPointerEnter (PointerEventData eventData) => RollOverEvent?.Invoke ();
	public void OnPointerExit  (PointerEventData eventData) => RollOutEvent ?.Invoke ();
}