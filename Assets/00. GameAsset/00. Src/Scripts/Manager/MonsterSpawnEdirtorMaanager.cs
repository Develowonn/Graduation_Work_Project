// # Unity
using UnityEngine;
using UnityEngine.UI;

// # Etc
using TMPro;

public class MonsterSpawnEdirtorMaanager : MonoBehaviour
{
	[SerializeField]
	private int					gameMaxTime;
	private int					gameCurrentTime;

	[Header("Data")]
	[SerializeField]
	private EditorMonsterData[]	monsterData;

	[Header("UI")]
	[SerializeField]
	private Slider				timerSliderBar;
	[SerializeField]
	private TMP_Text			currentGameTimeText;

	[Space(10), SerializeField]
	private TMP_InputField		gameTimeInputfield;
	[SerializeField]
	private Button				gameTimeSaveButton;

	private void Start()
	{
		// 슬라이더 바 설정 
		timerSliderBar.maxValue = gameMaxTime;
		timerSliderBar.onValueChanged.AddListener(delegate { OnTimerValueChanged(); });

		// 게임 타임 설정 
		gameTimeSaveButton.onClick.AddListener(() => OnClickGameTimeSave());
	}

	private void OnTimerValueChanged()
	{
		currentGameTimeText.text = $"{(int)timerSliderBar.value}초";
	}

	private void OnClickGameTimeSave()
	{
		int.TryParse(gameTimeInputfield.text, out int gameTime);

		timerSliderBar.maxValue  = gameTime;
		gameTimeInputfield.text  = default;
	}
}