using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float m_rayCD = 3.0f;
    private float m_currentCD;
    private Camera m_camera;
    private Vector3 m_camRayPos = new Vector3(200, 200, 0);

    private Vector2 m_inputVector;
    private Vector3 m_movements;
    private float m_curentVelocity;
    [SerializeField] private float m_rotationSmooth;
    [SerializeField] private float m_speed;
    private NavMeshAgent m_agent;
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnMouseDown()
    {
        Ray ray = m_camera.ScreenPointToRay(m_camRayPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("Flying"), QueryTriggerInteraction.Collide))
        {
            //hit.transform.gameObject.TryGetComponent<IFlying>(out IFlying flying);
            //if(flying != null){flying.Flying();}
            //set position of lazer beam (world space !!)
            if (hit.transform.gameObject.tag == "Box") Debug.Log("Hit box");
        }
    }
    private void CoolDown()
    {
        m_currentCD += Time.deltaTime;
        Mathf.Min(m_currentCD, m_rayCD); 
    }

    #region movements
    private void Movement()
    {
        m_agent.Move(m_movements * (m_speed * m_movements.magnitude) * Time.deltaTime);

        if(m_movements.magnitude != 0.0f)
        {
            float rot = transform.rotation.eulerAngles.y;
            float target = Mathf.Atan2(m_movements.x, m_movements.z) * Mathf.Rad2Deg;
            float rotate = Mathf.SmoothDampAngle(rot, target, ref m_curentVelocity, m_rotationSmooth);
            transform.rotation = Quaternion.Euler(0, rotate, 0);
        }

    }

    private void OnMove(InputValue value)
    {
        m_inputVector = value.Get<Vector2>();
        m_movements = (m_inputVector.x * Camera.main.transform.right + m_inputVector.y * Camera.main.transform.forward);
        
    }

    #endregion
}
