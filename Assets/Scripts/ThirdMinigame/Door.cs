using UnityEngine;
using System.Collections;

namespace ThirdMinigame
{
	[RequireComponent(typeof(AudioSource))]
	public class Door : MonoBehaviour
	{
		private bool _inDoor;
		[SerializeField] private bool isOpen;
		[SerializeField] private float smooth = 1.0f;
		[SerializeField] private float doorOpenAngle = -90.0f;
		[SerializeField] private float doorCloseAngle = 0.0f;
		[SerializeField] private AudioSource asource;
		[SerializeField] private AudioClip openDoor;
		[SerializeField] private AudioClip closeDoor;
		
		private void Start () 
        {
			asource = GetComponent<AudioSource> ();
		}
		
		private void Update () 
        {
			if (isOpen)
			{
	            var target = Quaternion.Euler (0, doorOpenAngle, 0);
	            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
			}
			else
			{
	            var target1= Quaternion.Euler (0, doorCloseAngle, 0);
	            transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
			}  
		}

		private void OnTriggerEnter(Collider other)
		{
			if (_inDoor) return;
			
			_inDoor = true;
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
			isOpen =!isOpen;
			asource.clip = isOpen ? openDoor : closeDoor;
			asource.Play ();
		}
	}
}