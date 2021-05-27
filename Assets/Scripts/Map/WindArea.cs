using UnityEngine;

public class WindArea : MonoBehaviour
{
    public GameObject Wind;
    private BoxCollider2D m_BoxCollider;
    private CPlayer m_Player;
    [SerializeField] private int Trigger_Count;
    [SerializeField] private int _Count = 0;
    [SerializeField] private Vector2 Direction;
    [SerializeField] private float Force = 60f; //重力为26f
    [SerializeField] private bool b_Active_WindArea;    //气流区域初始状态
    private float Speed_New;                            //气流区域中的限速
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
            if (_Count == Trigger_Count && Wind.activeInHierarchy == b_Active_WindArea)
            {
                Wind.SetActive(!b_Active_WindArea);
                m_BoxCollider.enabled = !b_Active_WindArea;
            }
        }
    }

    private void Awake()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        b_Active_WindArea = Wind.activeInHierarchy;
        m_BoxCollider.enabled = b_Active_WindArea;
        Direction = Public.CTool.Angle2Direction(transform.eulerAngles.z);
        m_Player = PlayerController.Instance.m_Player;
        Speed_New = Force;
    }

    private void Start()
    {
        if(Trigger_Count ==0)
        {
            Count = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_Player.InWindArea = true;
            CAudioController.Instance.PlaySound(ESound.Wind);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_Player.InWindArea = false;
            CAudioController.Instance.StopSound(ESound.Wind);
        }
    }

    private void OnTriggerStay2D(Collider2D collosion)
    {
        if (collosion.CompareTag("Player"))
        {
            m_Player.InWindArea = true;
            collosion.attachedRigidbody.AddForce(Direction*Force*(Speed_New-collosion.attachedRigidbody.velocity.magnitude)/Speed_New,ForceMode2D.Force);
        }    
    }

}
