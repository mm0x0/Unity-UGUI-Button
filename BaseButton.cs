using UnityEngine;
using UnityEngine.Events;

// ボタンイベント設定用抽象クラス
[DisallowMultipleComponent]
public abstract class BaseButton : MonoBehaviour
{
	public abstract void RegisterTouchEvent    (UnityAction f);
	public abstract void RegisterClickEvent    (UnityAction f);
	public abstract void RegisterRolloverEvent (UnityAction f);
	public abstract void RegisterRolloutEvent  (UnityAction f);
}