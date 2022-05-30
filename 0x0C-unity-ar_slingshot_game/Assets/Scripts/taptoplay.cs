using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.AI;

public class taptoplay : MonoBehaviour
{
    public GameObject instructions;
    public GameObject scoreText;
    public GameObject startButton;
    public GameObject tryAgainButton;

    public GameObject target;
    public int numberOfTargets;
    public NavMeshSurface _navMeshSurface;
    public static ARPlane plane;

    private ARRaycastManager _arRaycastManager;
    private ARPlaneManager _arPlaneManager;
    private slingshot slingshotBehavior;
    private Vector2 touchPosition;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arPlaneManager = GetComponent<ARPlaneManager>();
        slingshotBehavior = GetComponent<slingshot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            if (plane == null)
            {
                foreach (ARRaycastHit hit in hits)
                {
                    plane = _arPlaneManager.GetPlane(hit.trackableId);
                }
                plane.GetComponent<MeshRenderer>().enabled = false;
                plane.GetComponent<LineRenderer>().enabled = false;
                startButton.SetActive(true);
                _arPlaneManager.enabled = false;
            }
        }
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        instructions.SetActive(false);
        foreach (ARPlane plane in _arPlaneManager.trackables)
        {
            if (!(plane.trackableId == plane.trackableId))
            {
                plane.gameObject.SetActive(false);
            }
        }
        _navMeshSurface = plane.GetComponent<NavMeshSurface>();
        _navMeshSurface.BuildNavMesh();
        plane.gameObject.GetComponent<MeshRenderer>().enabled = false;
        plane.gameObject.GetComponent<LineRenderer>().enabled = false;
        SpawnAI(numberOfTargets);
    }

    public void SpawnAI(int amount)
	{
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), 0.05f, Random.Range(-0.1f, 0.1f));
            Instantiate(target, plane.center + randomOffset, Quaternion.Euler(0, 0, 0));
        }
    }

    public void ResetGame()
    {
        slingshotBehavior.Deactivate();
        tryAgainButton.SetActive(false);
        startButton.SetActive(false);
        scoreText.SetActive(false);
        instructions.SetActive(true);
        plane.gameObject.GetComponent<MeshRenderer>().enabled = true;
        plane.gameObject.GetComponent<LineRenderer>().enabled = true;
        plane = null;
        _arPlaneManager.enabled = true;
        foreach (ARPlane plane in _arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
        foreach(GameObject _target in GameObject.FindGameObjectsWithTag("Target")) 
        {
            Destroy(_target);
        }
    }

    public void Quit()
	{
        Application.Quit();
    }
}
