using System;
using Cysharp.Threading.Tasks;
using DialogSystem.Models;
using DialogSystem.View;
using NPCData;
using UnityEngine;

namespace DialogSystem
{
    public class DialogController : MonoBehaviour
    {
        [field: SerializeField] private DialogView dialogView;

        [Header("Tutor Info")] [SerializeField]
        private int index = 3;

        private bool isFirstPhraseShown;
        private DialogModel model;
        private bool _oneTimeTutor = true;

        private void Awake()
        {
            dialogView.OnConfirmedAction += OnConfirmedAction;
            dialogView.OnCancelledAction += OnCancelledAction;
        }

        private void OnDestroy()
        {
            dialogView.OnConfirmedAction -= OnConfirmedAction;
            dialogView.OnCancelledAction -= OnCancelledAction;
        }

        public static event Action<int> OnTutorRequest;

        public void StartDialog(Base_NPC_SO npc, Vector3 npcPosition)
        {
            model = new DialogModel(npc);
            dialogView.ShowFirstPhrase(npc.FirstPhrase, npcPosition);
            isFirstPhraseShown = true;

            OneTimeSolutionForTutor();

            if (isFirstPhraseShown)
            {
                dialogView.HideFirstPhrase();
                dialogView.Constructor(model);
            }
        }
        
        private void OneTimeSolutionForTutor()
        {
            if (_oneTimeTutor)
            {
                OnTutorRequest?.Invoke(index);
                _oneTimeTutor = false;
            }
        }

        public void ResetFirstPhrase()
        {
            isFirstPhraseShown = false;
            dialogView.HideFirstPhrase();
        }

        public void ShowFirstPhrase(string firstPhrase, Vector3 npcPosition)
        {
            dialogView.ShowFirstPhrase(firstPhrase, npcPosition);
        }

        public async UniTask<bool> WaitForConfirmationOrCancellation()
        {
            var completionSource = new UniTaskCompletionSource<bool>();
            Action onConfirmed = () => completionSource.TrySetResult(true);
            Action onCancelled = () => completionSource.TrySetResult(false);

            dialogView.OnConfirmedAction += onConfirmed;
            dialogView.OnCancelledAction += onCancelled;

            var result = await completionSource.Task;

            dialogView.OnConfirmedAction -= onConfirmed;
            dialogView.OnCancelledAction -= onCancelled;

            return result;
        }

        private void OnConfirmedAction()
        {
            Debug.Log("Quest confirmed");
        }

        private void OnCancelledAction()
        {
            ResetFirstPhrase();
        }
    }
}