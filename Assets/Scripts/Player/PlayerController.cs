using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    // 앞뒤 양옆 움직임에 필요한 변수
    public float moveSpeed;
    private Vector2 curMovementInput;

    // 점프에 필요한 변수
    public float jumptPower;
    public LayerMask groundLayerMask;

    [Header("Look")]
    // 카메라 회전에 필요한 변수
    public Transform cameraContainer;
    // 마우스 조작에 따른 카메라 회전에 필요한 변수
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    // 이벤트 등록
    // 입력이 될 때 마다 curMovementInput에 값이 들어가게 되는 함수
    public void OnMove(InputAction.CallbackContext context)
    {
        // context에는 현재 상태를 받아올 수 있다.
        if (context.phase == InputActionPhase.Performed) // 키가 계속 눌리는 상태.
        {
            curMovementInput = context.ReadValue<Vector2>(); // ReadValue : 값을 받아온다.
        }
        else if (context.phase == InputActionPhase.Canceled) // 키가 떨어졌다.
        {
            curMovementInput = Vector2.zero;
        }
    }

    // 실제로 움직이게 만들어주는 함수
    private void Move()
    {
        // 방향 가져오기, dir = 방향
        // 앞 뒤 움직임은 입력의 y값(w,s) , 양 옆 움직임은 입력의 x값(a,d)
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        // 상하로는 제어 받지 않도록 초기화, 점프로만 받을 수 있도록.
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir; // 리지드 바디 속도에 방향(힘)을 넣기
    }

    // 이벤트 등록
    // 마우스가 움직일 때 마다 mouseDelta에 값이 들어가게 되는 함수
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); // 마우스는 계속 값을 받아오는 상태기 때문에 값만 받아오면 된다.
    }

    // 실제로 카메라를 회전
    void CameraLook()
    {
        // 캐릭터가 좌우로 움직이기 위해선 카메라의 y축을 돌려줘야 한다.
        // 마우스 델타 값에서 x값을 카메라 Y에 넣어야 하고 y값을 카메라 x에 넣어야 한다.
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // 최소 최대 값 넘어가지 않게 설정
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // 카메라의 좌우 로컬 회전, -가 붙은 이유는 마우스가 위로 향했을 때 제대로 위를 볼 수 있게 하기 위해 값을 반전

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // 카메라의 위아래 회전, 캐릭터 각도 변화
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumptPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        // 플레이어를 기준으로 책상다리 형태의 레이 4개를 추가
        Ray[] rays = new Ray[4]
        {
            // 플레이어의 앞뒤 0.2f, 높이 0.1f 위치에서 down 방향으로 레이가 출발
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            // 플레이어의 양옆 0.2f, 높이 0.1f 위치에서 down 방향으로 레이가 출발
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            // 레이의 갯수 만큼 순회, 레이의 길이는 0.1f, 레이어 마스크에 해당되는 애들만 검출
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        // for문을 다 돌렸는데도 해당이 없으면
        return false;
    }


}
