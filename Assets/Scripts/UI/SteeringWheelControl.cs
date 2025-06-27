using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringWheelControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float maxWheelAngle = 720f; // e.g., 2 full rotations
    public float maxRudderAngle = 30f; // degrees left/right
    public float returnSpeed = 180f;   // wheel reset speed (deg/sec)

    private RectTransform wheelRect;
    private float currentWheelAngle = 0f;
    private float lastDragAngle;
    private bool isDragging = false;

    void Start()
    {
        wheelRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!isDragging)
        {
            // Auto-return to center
            if (Mathf.Abs(currentWheelAngle) > 1f)
            {
                float delta = returnSpeed * Time.deltaTime * Mathf.Sign(currentWheelAngle);
                currentWheelAngle -= delta;
                currentWheelAngle = Mathf.Clamp(currentWheelAngle, -maxWheelAngle, maxWheelAngle);
                wheelRect.localEulerAngles = new Vector3(0, 0, -currentWheelAngle);
            }
            else
            {
                currentWheelAngle = 0f;
                wheelRect.localEulerAngles = Vector3.zero;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        lastDragAngle = GetPointerAngle(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float currentDragAngle = GetPointerAngle(eventData);
        float angleDelta = Mathf.DeltaAngle(lastDragAngle, currentDragAngle);

        currentWheelAngle -= angleDelta;
        currentWheelAngle = Mathf.Clamp(currentWheelAngle, -maxWheelAngle, maxWheelAngle);
        wheelRect.localEulerAngles = new Vector3(0, 0, -currentWheelAngle);

        lastDragAngle = currentDragAngle;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    // Get rudder value in degrees (for boat script)
    public float GetRudderAngle()
    {
        return Mathf.Clamp(currentWheelAngle / maxWheelAngle * maxRudderAngle, -maxRudderAngle, maxRudderAngle);
    }

    private float GetPointerAngle(PointerEventData eventData)
    {
        Vector2 dir = eventData.position - RectTransformUtility.WorldToScreenPoint(null, wheelRect.position);
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
