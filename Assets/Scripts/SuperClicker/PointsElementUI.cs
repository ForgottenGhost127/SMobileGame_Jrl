using UnityEngine;
using System;
using DG.Tweening;
using TMPro;

public class PointsElementUI : MonoBehaviour
{
	#region Fields
	[SerializeField] private TextMeshProUGUI _clicksText;
	[SerializeField] private float _duration = 3;
	private GameController _game;
	
	#endregion

	#region Unity Callbacks
	void Awake()
	{
		_game = FindObjectOfType<GameController>();
		DoEffect();
	}
	
	#endregion

	#region Public Methods
	public void Initialize(Transform transformClick)
	{
		_clicksText.color = Color.white;
		gameObject.SetActive(true);
		transform.SetParent(transformClick, false);
		transform.localPosition = Vector3.zero;
		DoEffect();
	}

	#endregion

	#region Private Methods
	private void DoEffect()
	{
		_clicksText.transform.Translate(Vector3.back);
		_clicksText.text = "+" + _game.ClickRatio.ToString();
		_clicksText.DOColor(new Color(0, 0, 0, 0), _duration);

		transform.DOMoveY(transform.position.y + UnityEngine.Random.Range(100, 500), _duration);
		_game.Pool.AddToPool(this, _duration);
	}

	#endregion
}
