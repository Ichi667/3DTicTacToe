using UnityEngine;

namespace TicTacToe3D
{
    /// <summary>Повернення камери</summary>
    [RequireComponent(typeof(Camera))]
    public class CameraOrbit : MonoBehaviour
    {
        [Tooltip("Объект, вокруг которого вращаться (центр поля)")]
        public Transform target;
        [Tooltip("Расстояние до цели")]
        public float distance = 6f;
        [Tooltip("Скорость вращения по горизонтали")]
        public float xSpeed = 120f;
        [Tooltip("Скорость вращения по вертикали")]
        public float ySpeed = 120f;
        [Tooltip("Минимальный/максимальный угол по вертикали")]
        public float yMinLimit = -30f, yMaxLimit = 60f;

        private float xAngle = 0f, yAngle = 20f;

        void Start()
        {
            var angles = transform.eulerAngles;
            xAngle = angles.y;
            yAngle = angles.x;
            if (target == null)
                Debug.LogWarning("CameraOrbit: не задана цель (target).");
        }

        void LateUpdate()
        {
            if (target == null) return;

            // Для повернення камери ПКМ
            if (Input.GetMouseButton(1))
            {
                xAngle += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                yAngle -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
                yAngle = ClampAngle(yAngle, yMinLimit, yMaxLimit);
            }

            // розрах. нову позицію камери
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
