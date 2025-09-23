using UnityEngine;

public class CamBehavior : MonoBehaviour, ICameraShake
{
    [Header("Camera Basics")]
    [SerializeField] private Transform m_cameraTransform;
    [SerializeField] private GameObject m_target;
    [SerializeField] private float m_smoothTime;
    private Vector3 m_offset;

    [Header("ScreenShake")]
    [SerializeField] private float m_sSIntensity;
    [SerializeField] private float m_sSTime;
    [SerializeField] private float m_sSFallOff;
    [SerializeField] private float m_sSMaxTrauma;
    private float m_sSTrauma;
    private Vector3 m_sSOffset;
    void Start()
    {
        m_offset = m_cameraTransform.position - m_target.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        CalmDown();
    }

    void LateUpdate()
    {
        CameraFollow();
        CamShake();
    }
    private void CameraFollow()
    {
        Vector3 targetPosition = m_target.transform.position + m_target.transform.forward;
        transform.position = Vector3.Lerp(transform.position, targetPosition + m_offset, Time.deltaTime /* m_SmoothTime*/);
    }

    private void CamShake()
    {
        Vector3 randShake = Random.onUnitSphere;
        m_sSOffset = randShake * Mathf.Pow(m_sSTrauma, m_sSFallOff) * m_sSIntensity;
        transform.localPosition += m_sSOffset;
    }

    private void CalmDown()
    {
        m_sSTrauma = Mathf.MoveTowards(m_sSTrauma, 0.0f, Time.deltaTime * m_sSTime);
    }
    public void CamShake(float trauma)
    {
        Mathf.Min(m_sSTrauma += trauma, m_sSMaxTrauma);


    }
}
