using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float m_rayCD = 3.0f;
    private float m_currentCD;
    private Camera m_camera;
    private Vector3 m_camRayPos = new Vector3(200, 200, 0);
    void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }

    private void CoolDown()
    {
        m_currentCD += Time.deltaTime;
        Mathf.Min(m_currentCD, m_rayCD); 
    }
}
