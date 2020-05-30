# Unity-UGUI-Button

## 説明
  - BaseButton        ... ボタンイベント設定用抽象クラス
  - ETButton          ... UnityのEventTriggerを使ったボタン
  - PointerButton     ... UnityのIPointerHandlerを使ったボタン
  - UGUIButtonDOTween ... BaseButtonのタッチモーションをインスペクタで設定するコンポーネント

## 必要なライブラリ
  - DOTween
  - OdinInspector

## C#の動作バージョン
  - C# 6.0以上

## 使い方 (ETButton, PointerButton)
  - タッチイベントの設定
    ```
    [SerializeField]
    BaseButton btn;

    void Awake ()
    {
        btn.RegisterTouchEvent    (TouchBtn);
        btn.RegisterClickEvent    (TouchBtn);
        btn.RegisterRollOverEvent (RollOverBtn);
        btn.RegisterRollOutEvent  (RollOutBtn);
    }

    void TouchBtn    () => Debug.Log ("Touch");
    void ClickBtn    () => Debug.Log ("Click");
    void RollOverBtn () => Debug.Log ("RollOver");
    void RollOutBtn  () => Debug.Log ("RollOut");
    ```

## 使い方 (UGUIButtonDOTween)
  1. Canvas上のImageにUGUIButtonDOTweenをアタッチ
  3. 使うボタンを選択
  4. インスペクタで動きを設定
      - Common ... ボタンの共通の動き（チェックを外すと個別に設定可能）
        - Target ... 動かす対象オブジェクト
        - Push / Duration ... 動かす時間（押したとき）
        - Push / Easing ... 動きのカーブ（押したとき）
        - Release / Duration ... 動かす時間（離したとき）
        - Release / Duration ... 動きのカーブ（離したとき）
      - Fade ... Color, Spriteを設定した透明度でフェードイン
        - Fade Front ... オンにするとフェード画像を最前面に表示
      - Move ... localPositionを設定した数値に移動
      - Rotate ... rotationを設定した数値に変形
      - Scale ... localScaleを設定した数値に変形


