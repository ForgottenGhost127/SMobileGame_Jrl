using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Agent : MonoBehaviour
{
	#region Properties
	public SlotButtonUI destiny { get; set; }
	[field: SerializeField] public float RepeatRate { get; set; }

	#endregion

	#region Unity Callbacks
	
	void Start()
  {
		if (gameObject != null)
		{
			SlotButtonUI.OnSlotClicked += SetDestiny;
			if (destiny != null)
			{
				Movement();
				InvokeRepeating(nameof(Click), 1, RepeatRate);
			}
			else
			{
				Debug.LogWarning("Advertencia: El agente se creó sin destino.");
			}
		}
	}

	#endregion

	#region Private Methods
	private void SetDestiny(SlotButtonUI newDestiny)
	{
		if (gameObject != null)
		{
			destiny = newDestiny;
			Movement();

		}
	}

	private void Click()
	{
		if (destiny != null)
		{
			Debug.Log("Agente haciendo clic en: " + destiny.name);
			destiny.Click(1, true);
		}
		else
		{
			Debug.LogError("Error en Click(): destiny es NULL.");
		}
	}

	protected void Movement()
	{
		if (destiny != null)
		{
			Debug.Log("Moviendo agente a: " + destiny.name);
			transform.DOMove(destiny.transform.position, 1);
		}
		else
		{
			Debug.LogError("Error en Movement(): destiny es NULL.");
		}
	}

	IEnumerator AngelsOut()
	{
		yield return new WaitForSeconds(10);
		this.gameObject.SetActive(false);
	}
	#endregion   
}
