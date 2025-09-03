using System;
using UnityEngine;
using System.Collections;

namespace ThirdMinigame
{
	[RequireComponent(typeof(AudioSource))]
	public class Door : MonoBehaviour
	{
		private bool inDoor = false;
		public bool open;
		public float smooth = 1.0f;
		float DoorOpenAngle = -90.0f;
	    float DoorCloseAngle = 00.0f;
		public AudioSource asource;
		public AudioClip openDoor,closeDoor;
		// Use this for initialization
		void Start () {
			asource = GetComponent<AudioSource> ();
		}
		
		// Update is called once per frame
		void Update () {
			if (open)
			{
	            var target = Quaternion.Euler (0, DoorOpenAngle, 0);
	            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
		
			}
			else
			{
	            var target1= Quaternion.Euler (0, DoorCloseAngle, 0);
	            transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
		
			}  
		}

		private void OnTriggerEnter(Collider other)
		{
			if (inDoor) return;
			
			inDoor = true;
			if (other.CompareTag("Player"))
				StartCoroutine(DoorOpenRoutine());
		}

		private IEnumerator DoorOpenRoutine()
		{
			OpenDoor();
			yield return new WaitForSeconds(0.3f);
			OpenDoor();
		}

		private void OpenDoor(){
			open =!open;
			asource.clip = open?openDoor:closeDoor;
			asource.Play ();
		}
	}
}