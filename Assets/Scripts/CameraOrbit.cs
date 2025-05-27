using UnityEngine;

namespace TicTacToe3D
{
    /// <summary>���������� ������</summary>
    [RequireComponent(typeof(Camera))]
    public class CameraOrbit : MonoBehaviour
    {
        [Tooltip("������, ������ �������� ��������� (����� ����)")]
        public Transform target;
        [Tooltip("���������� �� ����")]
        public float distance = 6f;
        [Tooltip("�������� �������� �� �����������")]
        public float xSpeed = 120f;
        [Tooltip("�������� �������� �� ���������")]
        public float ySpeed = 120f;
        [Tooltip("�����������/������������ ���� �� ���������")]
        public float yMinLimit = -30f, yMaxLimit = 60f;

        private float xAngle = 0f, yAngle = 20f;

        void Start()
        {
            var angles = transform.eulerAngles;
            xAngle = angles.y;
            yAngle = angles.x;
            if (target == null)
                Debug.LogWarning("CameraOrbit: �� ������ ���� (target).");
        }

        void LateUpdate()
        {
            if (target == null) return;

            // ��� ���������� ������ ���
            if (Input.GetMouseButton(1))
            {
                xAngle += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                yAngle -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
                yAngle = ClampAngle(yAngle, yMinLimit, yMaxLimit);
            }

            // ������. ���� ������� ������
            Quaternion rot = Quaternion.Euler(yAngle, xAngle, 0);
            Vector3 pos = rot * new Vector3(0, 0, -distance) + target.position;

            transform.rotation = rot;
            transform.position = pos;
        }

        static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360) angle += 360;
            if (angle > 360) angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }
    }
}
