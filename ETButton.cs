using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// EventTriggerを使ったボタン
[RequireComponent (typeof (EventTrigger))]
public sealed class ETButton : BaseButton
{
	// タップイベントを設定
	public override void RegisterTouchEvent (UnityAction f) =>
		RegisterEvent (f, EventTriggerType.PointerDown);

	// クリックイベントを設定
	public override void RegisterClickEvent (UnityAction f) =>
		RegisterEvent (f, EventTriggerType.PointerClick);

	// ロールオーバーイベントを設定
	public override void RegisterRolloverEvent (UnityAction f) =>
		RegisterEvent (f, EventTriggerType.PointerEnter);

	// ロールアウトイベントを設定
	public override void RegisterRolloutEvent (UnityAction f) =>
		RegisterEvent (f, EventTriggerType.PointerExit);

	// タッチイベントの設定
	public void RegisterEvent (UnityAction f, EventTriggerType type)
	{
		// 重複登録させない
		RemoveEvent (f);

		// イベントエントリーを作成してEventTriggerに追加
		var entry = new ETButtonEntry (f);
		entry.eventID = type;
		entry.callback.AddListener ((x) => f ());
		GetComponent<EventTrigger> ().triggers.Add (entry);
	}

	// タッチイベントの削除
	// * 引数ActionとTouchEntryのアクションが一致したら削除する
	public void RemoveEvent (UnityAction f)
	{
		ETButtonEntry tgtEntry = null;

		foreach (EventTrigger.Entry entry in GetComponent<EventTrigger> ().triggers)
		{
			if (!(entry is ETButtonEntry))
				continue;

			if ((entry as ETButtonEntry).f == f)
				tgtEntry = (entry as ETButtonEntry);
		}

		if (tgtEntry != null)
			GetComponent<EventTrigger> ().triggers.Remove (tgtEntry);
	}
}

// Actionを保持するEventTrigger.Entryクラス
class ETButtonEntry : EventTrigger.Entry
{
	public UnityAction f;
	public ETButtonEntry (UnityAction f) : base () => this.f = f;
}