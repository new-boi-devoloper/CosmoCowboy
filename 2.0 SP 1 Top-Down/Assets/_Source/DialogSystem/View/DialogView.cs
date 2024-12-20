using System;
using Cysharp.Threading.Tasks;
using DialogSystem.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace DialogSystem.View
{
    public class DialogView : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI NpcNameText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI DialogText { get; private set; }
        [field: SerializeField] public Button NextButton { get; private set; }
        [field: SerializeField] public Button PreviousButton { get; private set; }
        [field: SerializeField] public Button CancellationButton { get; private set; }
        [field: SerializeField] public Button ConfirmationButton { get; private set; }
        [field: SerializeField] public GameObject FirstPhraseWindowPrefab { get; private set; }
        [field: SerializeField] public GameObject DialogUI { get; private set; }

        private GameObject _currentFirstPhraseWindow;
        private Vector3 _currentNpcPosition;
        private TextMeshProUGUI _firstPhraseText;
        private DialogModel _model;

        private void Start()
        {
            NextButton.onClick.AddListener(OnNextButtonClicked);
            PreviousButton.onClick.AddListener(OnPreviousButtonClicked);
            CancellationButton.onClick.AddListener(OnCancellationButtonClicked);
            ConfirmationButton.onClick.AddListener(OnConfirmedButtonClicked);
            InitializeFirstPhraseWindow();
        }

        private void OnDestroy()
        {
            DestroyFirstPhraseWindow();
        }

        public event Action OnConfirmedAction;
        public event Action OnCancelledAction;

        public void Constructor(DialogModel model)
        {
            DialogUI.SetActive(true);
            _model = model;
            UpdateView();
        }

        private void UpdateView()
        {
            TimeManager.TurnOnPause();

            NpcNameText.text = _model.NPC.NPCName;
            DialogText.text = _model.GetCurrentLine();

            // Включаем кнопки только если есть больше одной реплики
            bool hasMultipleLines = _model.NPC.questLines.Length > 1;

            NextButton.interactable = hasMultipleLines && _model.CurrentLineIndex < _model.NPC.questLines.Length - 1;
            PreviousButton.interactable = hasMultipleLines && _model.CurrentLineIndex > 0;
        }

        private void OnNextButtonClicked()
        {
            _model.NextLine();
            UpdateView();
        }

        private void OnPreviousButtonClicked()
        {
            _model.PreviousLine();
            UpdateView();
        }

        private void OnConfirmedButtonClicked()
        {
            DialogUI.SetActive(false);
            OnConfirmedAction?.Invoke();
            
            TimeManager.TurnOffPause();
        }

        private void OnCancellationButtonClicked()
        {
            DialogUI.SetActive(false);
            OnCancelledAction?.Invoke();
            
            TimeManager.TurnOffPause();
        }

        private void InitializeFirstPhraseWindow()
        {
            if (_currentFirstPhraseWindow == null)
            {
                _currentFirstPhraseWindow = Instantiate(FirstPhraseWindowPrefab, transform);
                _firstPhraseText = _currentFirstPhraseWindow.GetComponentInChildren<TextMeshProUGUI>();
                _currentFirstPhraseWindow.SetActive(false);
            }
        }

        public async void ShowFirstPhrase(string firstPhrase, Vector3 npcPosition)
        {
            if (_currentFirstPhraseWindow == null) InitializeFirstPhraseWindow();

            _firstPhraseText.text = firstPhrase;
            _currentNpcPosition = npcPosition;

            UpdateFirstPhraseWindowPosition();

            _currentFirstPhraseWindow.SetActive(true);

            await UniTask.Delay(2000); // Окно будет отображаться 2 секунды
            HideFirstPhrase();
        }

        private void UpdateFirstPhraseWindowPosition()
        {
            if (Camera.main != null)
            {
                var screenPosition = Camera.main.WorldToScreenPoint(_currentNpcPosition);
                _currentFirstPhraseWindow.transform.position =
                    screenPosition + new Vector3(0, 50, 0); // Добавляем смещение по высоте
            }
        }

        public void HideFirstPhrase()
        {
            if (_currentFirstPhraseWindow != null) _currentFirstPhraseWindow.SetActive(false);
        }

        private void DestroyFirstPhraseWindow()
        {
            if (_currentFirstPhraseWindow != null) Destroy(_currentFirstPhraseWindow);
        }
    }
}