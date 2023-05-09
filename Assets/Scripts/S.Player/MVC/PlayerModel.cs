using UnityEngine;
using UnityEngine.Serialization;

namespace S.Player.MVC
{
    public class PlayerModel : MonoBehaviour
    {
        public float moveSpeed = 2.0f;
        public float rotationSpeed = 10.0f;
        private Rigidbody _rb;
        private Vector3 _movement;

        public Animator animator;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            _movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
            float currentSpeed = _movement.magnitude;
            animator.SetFloat(Speed,currentSpeed);
        }
        public void MoveCharacter()
        {
            Vector3 isometricDirection = IsoVector(_movement);
            _rb.MovePosition(_rb.position + isometricDirection * (moveSpeed * Time.fixedDeltaTime));
        }

        public void RotateCharacter()
        {
            if (_movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(IsoVector(_movement), Vector3.up);
                _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
            }
        }
        private Vector3 IsoVector(Vector3 inputVector)
        {
            Vector3 isoVector = new Vector3();
            isoVector.x = (inputVector.x - inputVector.z) * 0.5f;
            isoVector.y = inputVector.y;
            isoVector.z = (inputVector.x + inputVector.z) * 0.5f;
            return isoVector;
        }
    }

}
