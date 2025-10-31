using UnityEngine;

public class PisoTactil : ObstaculosInteractuables
{
    [SerializeField] private Transform[] _movePoint;
    private int currentPointIndex = 0;
    [SerializeField] private float moveSpeed = 10f;

    private Material mat;

    //SHADDER
    private float _dissolve = 0f;
    public float dissolveDuration = 2f;   // Duración del efecto
    [SerializeField] float dissolveSpeed = 0.5f; // velocidad de disolución
    private bool _isDissolving = false;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Animación de disolución
        if (_isDissolving)
        {
            ObjectAction();
        }
    }

    public override void Execute()
    {
        StartShadder();
    }

    private void StartShadder()
    {
        if (!_isDissolving)
        {
            Debug.Log("Piso tocado me muevo al point");
            _isDissolving = true;
        }
    }
    public override void ObjectAction()  //esto lo hice publico para que lo pueda overridear del padre
    {
        if (_movePoint.Length == 0) return; 

        Transform targetPoint = _movePoint[currentPointIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex++;

            if (currentPointIndex >= _movePoint.Length)
            {
                _isDissolving = false;
                currentPointIndex = _movePoint.Length - 1; 
            }
        }
    }
}
