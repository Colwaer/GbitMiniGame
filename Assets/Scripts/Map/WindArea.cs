using UnityEngine;

public class WindArea : MonoBehaviour
{
    public GameObject Wind;
    private BoxCollider2D m_BoxCollider;
    [SerializeField] private int Trigger_Count;
    [SerializeField] private int _Count = 0;
    [SerializeField] private Vector2 Direction;
    [SerializeField] private float Force = 60f; //重力为26f
    public int Count
    {
        get
        {
            return _Count;
        }
        set
        {
            if (value < 0) value = 0;
            _Count = value;
            if (_Count == Trigger_Count)
            {
                Wind.SetActive(true);
                m_BoxCollider.enabled = true;
            }
        }
    }

    private void Awake()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_BoxCollider.enabled = false;
        Direction = Public.CTool.Angle2Direction(transform.rotation.z);
    }

    private void Start()
    {
        if(Trigger_Count ==0)
        {
            Wind.SetActive(true);
            m_BoxCollider.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collosion)
    {
        if (collosion.CompareTag("Player"))
        {
            collosion.attachedRigidbody.AddForce(Direction*Force);
        }    
    }

}
