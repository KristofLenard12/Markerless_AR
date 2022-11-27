using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem.HID;

public class Interactor : MonoBehaviour
{
	public TextMeshProUGUI interactText;
	private Camera cam;
	public float rayDistance;
	private int layerMask;
	private Ray ray;

	// Start is called before the first frame update
	void Start()
	{
		cam = GetComponent<Camera>();
		layerMask = LayerMask.GetMask("Interactible"); 
	}

	// Update is called once per frame
	void Update()
	{
		ShootRay();
	}

	private void ShootRay()
	{
		RaycastHit hit;
		ray = new Ray(transform.position, cam.transform.forward);

		if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
		{
			//Debug.Log("Hit: " + hit.collider.gameObject.name);
			if (hit.transform.tag == "Interactible")
			{
				ActivateText();

				GetInput(hit);
			}
			else
			{
				DeactivateText();
			}
		}
		else
		{
			DeactivateText();
		}
	}

	private void GetInput(RaycastHit hit)
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if(touch.phase == TouchPhase.Began)
			{	
				ActivateAnimation(hit);
			}
		}
		else if (Input.GetKeyDown(KeyCode.E))
		{
			ActivateAnimation(hit);
		}
	}

	private void ActivateAnimation(RaycastHit hit)
	{
		var anim = hit.collider.GetComponent<Animator>();

		if (anim == null)
		{
			Debug.LogError("Door has no animator");
			return;
		}

		if (anim.GetBool("toOpen"))
		{
			anim.SetBool("toOpen", false);
		}
		else
		{
			anim.SetBool("toOpen", true);
		}
	}

	private void ActivateText()
	{
		if (interactText.gameObject.activeSelf) return;

		interactText.gameObject.SetActive(true);
	}

	private void DeactivateText()
	{

		if (!interactText.gameObject.activeSelf) return;

		interactText.gameObject.SetActive(false);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, rayDistance);
	}
}
