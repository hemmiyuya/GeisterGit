using UnityEngine;

namespace StateManager
{
    /// <summary>
    /// �^�b�`�Ǘ��N���X
    /// </summary>
    public class TouchManager
    {
        private bool _touch_flag;      // �^�b�`�L��
        private Vector3 _touch_position;   // �^�b�`���W
        private TouchPhase _touch_phase;   // �^�b�`���

        /// <summary>
        /// ���C���[�}�X�N�B�{�[�h�Ƃ̂ݓ�����悤��
        /// </summary>
        private const int layerMask = 7;

        /// <summary>
        /// �^�b�`���Ă�������X�V������
        /// </summary>
        /// <param name="flag">�^�b�`�t���O</param>
        /// <param name="position">�^�b�`�������W</param>
        /// <param name="phase">�^�b�`�̏��</param>
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
        /// �^�b�`����������X�N���v�g��update�ŌĂяo��
        /// </summary>
        public void update()
        {
            this._touch_flag = false;

            // �������u��
            if (Input.GetMouseButtonUp(0))
            {
                this._touch_flag = true;
                this._touch_phase = TouchPhase.Ended;
            }

            // �������ςȂ�
            if (Input.GetMouseButton(0))
            {
                this._touch_flag = true;
                this._touch_phase = TouchPhase.Moved;
            }

            // �������u��
            if (Input.GetMouseButtonDown(0))
            {
                this._touch_flag = true;
                this._touch_phase = TouchPhase.Began;
            }

            // ���W�擾
            //if (this._touch_flag) this._touch_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 200, Color.red);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //���C�����������ʒu�𓾂�
                this._touch_position = hit.point;
            }

        }

        /// <summary>
        /// �^�b�`���Ă������n��
        /// </summary>
        /// <returns></returns>
        public TouchManager GetTouch()
        {
            return new TouchManager(this._touch_flag, this._touch_position, this._touch_phase);
        }

        /// <summary>
        /// �������Ƃ�
        /// </summary>
        public void TouchBegan()
        {
            this._touch_phase = TouchPhase.Began;
        }

        /// <summary>
        /// ���ꂽ����
        /// </summary>
        public void TouchEnded()
        {
            //���ꂽ
            this._touch_flag = true;
            this._touch_phase = TouchPhase.Ended;
        }

        public bool TouchFlag => _touch_flag;

        public Vector3 TouchPosition => _touch_position;

        public TouchPhase TouchPhase => _touch_phase;
    }
}