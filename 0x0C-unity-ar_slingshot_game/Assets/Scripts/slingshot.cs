using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class slingshot : MonoBehaviour
{
    public Transform sling;
    public Transform slingPoint;
    public Transform ammoSpawn;
    public GameObject Ammo;
    public GameObject ammoUI;
    public GameObject tryAgainButton;
    private GameObject ammoInst;
    public score scoreUI;
    public LineRenderer trajectoryRenderer;
    public int lineSegmentCount = 20;

    private List<Vector3> _linePoints = new List<Vector3>();
    int ammoCount = 7;
    public float power = 1f;
    public bool gameActive = false;
    public ARPlane plane;

    public void Activate()
	{
        gameActive = false;
        slingPoint.gameObject.SetActive(true);
        scoreUI.ScoreReset();
        tryAgainButton.SetActive(false);
        DelayInput();
        ammoUI.GetComponent<AmmoUI>().Reload();
        ammoUI.SetActive(true);
        ammoCount = 7;
        ammoInst = Instantiate(Ammo, ammoSpawn);
        DelayInput();
    }

    public async void DelayInput()
	{
        var end = Time.time + 1f;
        while (Time.time < end)
		{
            await Task.Yield();
		}
        gameActive = true;
    }

    public void Deactivate()
    {
        slingPoint.gameObject.SetActive(false);
        gameActive = false;
        ammoUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
		{
            if (Input.touchCount > 0 && ammoCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                DrawTrajectory(sling.position, ammoInst.GetComponent<Rigidbody>(), ammoSpawn.position);
                // Handle finger movements based on TouchPhase
                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        plane = taptoplay.plane;
                        sling.transform.SetParent(plane.transform);
                        break;

                    //Determine if the touch is a moving touch
                    case TouchPhase.Moved:
                        break;

                    case TouchPhase.Ended:
                        trajectoryRenderer.positionCount = 0;
                        float dist = Vector3.Distance(sling.position, slingPoint.position);
                        ammoCount -= 1;
                        ammoUI.GetComponent<AmmoUI>().UpdateAmount(ammoCount);
                        LaunchAmmo(dist);
                        SlingMotion();
                        break;
                }
            }
        }
    }

    void LaunchAmmo(float dist)
    {
        ammoInst.transform.parent = null;
        ammoInst.transform.LookAt(sling.position);
        ammoInst.GetComponent<Rigidbody>().isKinematic = false;
        ammoInst.GetComponent<Rigidbody>().AddForce(ammoInst.transform.forward * dist * power, ForceMode.Impulse);
        ammoInst.GetComponent<AmmoBehavior>().Delete();
        ammoInst = null;
    }

    public async void SlingMotion()
    {
        sling.SetParent(sling, true);
        var end = Time.time + 1f;
        while (Time.time < end)
        {
            await Task.Yield();
        }
        sling.position = slingPoint.position;
        sling.rotation = slingPoint.rotation;
        sling.SetParent(slingPoint, true);
        if (ammoCount > 0)
            ammoInst = Instantiate(Ammo, ammoSpawn);
		else
		{
            gameActive = false;
            tryAgainButton.SetActive(true);
        }
    }

    void DrawTrajectory(Vector3 slingVector, Rigidbody rb, Vector3 ammoPos)
	{
        Vector3 velocity = (slingVector / rb.mass) * Time.fixedDeltaTime;
        float flightDuration = (2 * velocity.y) / Physics.gravity.y;
        float stepTime = flightDuration / lineSegmentCount;
        _linePoints.Clear();

		for (int i = 0; i < lineSegmentCount; i++)
		{
            float stepTimePassed = stepTime * i;
            Vector3 moveVector = new Vector3(velocity.x * stepTimePassed, velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed, velocity.z * stepTimePassed);
            _linePoints.Add(-moveVector + ammoPos);
		}
        trajectoryRenderer.positionCount = _linePoints.Count;
        trajectoryRenderer.SetPositions(_linePoints.ToArray());
	}
}
