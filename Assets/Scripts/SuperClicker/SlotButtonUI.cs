using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SlotButtonUI : MonoBehaviour
{
    #region Properties
    private AudioSource _thisAudioSource;
	[SerializeField] AudioClip outofStockClip;
	[SerializeField] AudioClip rewardClip;
    [field: SerializeField] public Reward Reward;
	public int ClicksLeft {
		get {
			return _clicksLeft;
		}
		set {
			_clicksLeft = value;
			
			if (_clicksLeft <= 0 && _stock > 0)
			{
				OnSlotReward?.Invoke(Reward);
				_thisAudioSource.PlayOneShot(rewardClip);
				_stock--;
				if (_stock > 0)
				{
					_initialClicks = Mathf.CeilToInt(_initialClicks * 1.15f);
					_clicksLeft = _initialClicks;
				}
				else
				{
					_thisAudioSource.Stop();
					GetComponent<Image>().enabled = false;
					_clickButton.interactable = false;
					_clicksText.enabled = false;

				}
				RefreshClicksText();
			}
		} 
	}

	public static event Action<Reward> OnSlotReward;
	public static event Action<SlotButtonUI> OnSlotClicked;
	#endregion

	#region Fields
	[Header("--Clicks--")]
	[SerializeField] private int _initialClicks = 10;

	[Header("--UI--")]
	[SerializeField] private Button _clickButton;
	[SerializeField] private TextMeshProUGUI _clicksText;
	[SerializeField] private ParticleSystem _particles;
	[SerializeField] private int _matParticleIndex;

	[Header("--Prefab points--")]
	[SerializeField] private PointsElementUI _pointsPrefab;
	
	private GameController _game;
	private int _stock = 5;
	private int _clicksLeft = 0;

    #endregion

    #region Unity Callbacks
    private void Awake()
	{
		_particles = GetComponentInChildren<ParticleSystem>();
		_game = FindObjectOfType<GameController>();
		Reward.ObjectReward = this;
		_thisAudioSource = GetComponent<AudioSource>();
		
		Initialize();
		_clickButton.onClick.AddListener(Click);
		RefreshClicksText();
	}

	#endregion

	#region Public Methods
	public void Click(int clickCount, bool agent = false)
	{
		var mainModule = _particles.main;
		mainModule.startSpeed = Mathf.Clamp(clickCount / 2, 1, 30);
		_particles.Emit(Mathf.Clamp(clickCount,1, 15));
		ClicksLeft -= clickCount;
		RefreshClicksText();
		Camera.main.DOShakePosition(Mathf.Clamp(0.01f * clickCount, 0, 2));
		if (!agent)
		{
			PointsElementUI newPoints = _game.Pool.GetPoints();
			newPoints.Initialize(transform);
			_game.RainParticles();
		}
	}

	
	#endregion

	#region Private Methods
	private void Initialize()
	{
		ClicksLeft = _initialClicks;

		float segment = 1f / 28f;
		float frame = segment * _matParticleIndex;
		var tex = _particles.textureSheetAnimation;
		tex.startFrame = frame;
	}

	private void Click()
	{
		if(this != null)
		{

		    OnSlotClicked?.Invoke(this);
		    int clickRatio = Mathf.RoundToInt(_game.ClickRatio);
		    Click(clickRatio);
		}
	}

	private void RefreshClicksText()
	{
		_clicksText.text = ClicksLeft.ToString();
	}

	#endregion
}
