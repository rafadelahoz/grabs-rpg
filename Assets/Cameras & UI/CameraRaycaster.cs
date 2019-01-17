using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit m_hit;
    public RaycastHit hit
    {
        get { return m_hit; }
    }

    Layer m_layerHit;
    public Layer layerHit
    {
        get { return m_layerHit; }
    }

    // Delegate type
    public delegate void OnLayerChange();
    // Observer set
    public OnLayerChange layerChangeObservers; 

    void Start()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        Layer prevLayer = m_layerHit;

        bool found = false;

        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
                m_layerHit = layer;
                found = true;
                break;
            }
        }

        // Otherwise return background hit
        if (!found)
        {
            m_hit.distance = distanceToBackground;
            m_layerHit = Layer.RaycastEndStop;
        }

        if (m_layerHit != prevLayer)
            layerChangeObservers();
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit _hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out _hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return _hit;
        }
        return null;
    }
}
