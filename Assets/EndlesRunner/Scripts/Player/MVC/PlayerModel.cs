using System.Collections;
using UnityEngine;
public class PlayerModel : Rewind
{
    private Vector3 playerMovementInput;
    [SerializeField] private LayerMask FloorMask;

    [SerializeField] Controller inputManager;

    [SerializeField] CapsuleCollider playerCollider;
    private float originalHeight;
    private Vector3 originalCenter;


    [SerializeField] private Transform Feets;
    [SerializeField] private Transform _myTransform;
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Animator _animator;

    [SerializeField] private float Speed = 5f;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private float maxForwardSpeed = 20f; //VARIABLES DEL REMOTE
    [SerializeField] private float ForwardSpeed = 10f;     //VARIABLES DEL REMOTE
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float GroundCheckDistance = 0.2f;
    [SerializeField] private float OverlapRadio = 10f;

    private Collider[] _hits = new Collider[10];
    //mobile
    [SerializeField] private float deadZone = 0.1f;

    private Movement movimiento; //composición
    private View view;



    public override void Save()
    {
       // _state.Rec(Speed, transform.position, transform.rotation);

        Debug.Log("Guardo la posicion");
    }

    public override void Load()
    {
        if (!_state.IsRemembered())
        {
            Debug.Log("No tengo nada que recordar");

            return;
        }

        var x = _state.Remember();
        Debug.Log("Pos" + (Vector3)x.parametres[0]);

        transform.position = (Vector3)x.parametres[0];

        transform.rotation = (Quaternion)x.parametres[1];

    }

    private void Awake()
    {
        movimiento = new Movement()
            .SetPlayerBody(PlayerBody)
            .SetPlayerTransfomr(Feets)
            .SetPlayerSpeed(Speed)
            .SetPlayerForwardSpeed(ForwardSpeed) //ACA TENEMOS VALORES DEL REMOTE
            .SetPlayerJumpForce(JumpForce)
            .SetPlayerAcceleration(acceleration)
            .SetPlayerMaxForwardSpeed(maxForwardSpeed); ///ACA TENEMOS VALORES DEL REMOTE

        view = new View().SetAnimator(_animator);

        inputManager.OnJump += Jump;
        inputManager.OnMove += Move;
        inputManager.OnRoll += Roll;
        inputManager.OnTouchAndClick += CheackWall;
    }
    private void Start()
    {
        EventManager.Subscribe(TypeEvents.RewindEvent, OnCollsionRewind);

        originalHeight = playerCollider.height;
        originalCenter = playerCollider.center;

        // inicializar con los valores de RemoteConfig
        if (RemoteConfigExample.Instance != null)
        {
            ForwardSpeed = RemoteConfigExample.Instance.forwardSpeed;
            maxForwardSpeed = RemoteConfigExample.Instance.maxForwardSpeed;
        }
    }

    #region Collisioner Detecter y Trigger Event Manager
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            OnCollsionDead();
            StartCoroutine(DeadTimeLapse()); 
        }
    }

    private void OnCollsionDead()
    {
        view.Collisioner();
        Debug.Log("view.PARTIC");
    }

    private IEnumerator DeadTimeLapse()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("1 segundos después");
        EventManager.Trigger(TypeEvents.GameOver);
      //  _state.Delete();
    }
    #endregion


    private void OnCollsionRewind(object[] parameters)
    {
        view.RewindAnim();
        Debug.Log("view.PARTIC");
    }

    private void CheackWall()
    {
        // Detecta colliders dentro del radio, sin generar garbage
        int count = Physics.OverlapSphereNonAlloc(_myTransform.position, OverlapRadio, _hits);

        for (int i = 0; i < count; i++)
        {
            var obstacleInt = _hits[i].GetComponent<ObstaculosInteractuables>();

            if (obstacleInt != null)
            {
                obstacleInt.Execute();
            }
        }
    }

    bool CheckIsGrounded()
    {
        // Chequear suelo
        return Physics.CheckSphere(Feets.position, GroundCheckDistance, FloorMask);
    }

    void Jump()
    {
        if ( ! CheckIsGrounded() ) return;
        movimiento.Jump();
        view.Jump();
    }
    void Move(float dirHorizontal)
    {
#if UNITY_ANDROID || UNITY_IOS
        if (Mathf.Abs(dirHorizontal) < deadZone)
            dirHorizontal = 0f;
#endif

        playerMovementInput = new Vector3(dirHorizontal, 0f, 0f);
        movimiento.Move(playerMovementInput);
    }

    void Roll()
    {
        if ( ! CheckIsGrounded() )  return;
        playerCollider.height = originalHeight * 0.7f;
        playerCollider.center = originalCenter * 0.9f;
        view.Roll();
    }

    public void OnRollEnd()
    {
        playerCollider.height = originalHeight;
        playerCollider.center = originalCenter;
    }
}