using UnityEngine;

namespace StateManager
{
    /// <summary>
    /// タッチ管理クラス
    /// </summary>
    public class TouchManager
    {
        private bool _touch_flag;      // タッチ有無
        private Vector3 _touch_position;   // タッチ座標
        private TouchPhase _touch_phase;   // タッチ状態

        /// <summary>
        /// レイヤーマスク。ボードとのみ当たるように
        /// </summary>
        private const int layerMask = 7;

        /// <summary>
        /// タッチしている情報を更新するやつ
        /// </summary>
        /// <param name="flag">タッチフラグ</param>
        /// <param name="position">タッチした座標</param>
        /// <param name="phase">タッチの状態</param>
        public TouchManager(bool flag = false, Vector3? position = null, TouchPhase phase = TouchPhase.Began)
        {
            this._touch_flag = flag;
            if (position == null)
            {
                this._touch_position = new Vector3(0, 0,0);
            }
            else
            {
                this._touch_position = (Vector3)position;
            }
            this._touch_phase = phase;
        }

        /// <summary>
        /// タッチを実装するスクリプトでupdateで呼び出す
        /// </summary>
        public void update()
        {
            this._touch_flag = false;

            // 離した瞬間
            if (Input.GetMouseButtonUp(0))
            {
                this._touch_flag = true;
                this._touch_phase = TouchPhase.Ended;
            }

            // 押しっぱなし
            if (Input.GetMouseButton(0))
            {
                this._touch_flag = true;
                this._touch_phase = TouchPhase.Moved;
            }

            // 押した瞬間
            if (Input.GetMouseButtonDown(0))
            {
                this._touch_flag = true;
                this._touch_phase = TouchPhase.Began;
            }

            // 座標取得
            //if (this._touch_flag) this._touch_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 200, Color.red);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //レイが当たった位置を得る
                this._touch_position = hit.point;
            }

        }

        /// <summary>
        /// タッチしている情報を渡す
        /// </summary>
        /// <returns></returns>
        public TouchManager GetTouch()
        {
            return new TouchManager(this._touch_flag, this._touch_position, this._touch_phase);
        }

        /// <summary>
        /// 押したとき
        /// </summary>
        public void TouchBegan()
        {
            this._touch_phase = TouchPhase.Began;
        }

        /// <summary>
        /// 離れた判定
        /// </summary>
        public void TouchEnded()
        {
            //離れた
            this._touch_flag = true;
            this._touch_phase = TouchPhase.Ended;
        }

        public bool TouchFlag => _touch_flag;

        public Vector3 TouchPosition => _touch_position;

        public TouchPhase TouchPhase => _touch_phase;
    }
}