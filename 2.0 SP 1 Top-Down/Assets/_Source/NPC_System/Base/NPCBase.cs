using Cysharp.Threading.Tasks;
using NPCData;
using UnityEngine;

namespace Base
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public abstract class NpcBase : MonoBehaviour
    {
        protected const int TimeToRespond = 2000; // 2 seconds in milliseconds
        private UniTaskCompletionSource _interactionTimer;

        protected Vector3 NpcPosition;
        public abstract Base_NPC_SO NpcSo { get; protected set; }
        public abstract CapsuleCollider2D NpcCollider { get; protected set; }

        protected bool IsInteracting { get; set; }
        protected bool HasInteracted { get; set; }

        private void Update()
        {
            NpcPosition = transform.position;
        }

        protected void OnMouseDown()
        {
            Interact();
        }

        protected abstract UniTask OnHasInteracted();
        protected abstract UniTask OnHasNotInteracted();

        private async UniTask Interact()
        {
            if (IsInteracting) return;

            IsInteracting = true;

            if (!HasInteracted)
            {
                await OnHasNotInteracted(); // Запуск асинхронного метода с ожиданием
                // Debug.Log(NPC_SO.FirstPhrase);
                HasInteracted = true;
                _ = StartInteractionTimer(); // Запуск таймера
            }
            else
            {
                ResetInteractionTimer(); // Сброс таймера
                // Debug.Log("1");
                await OnHasInteracted(); // Ожидание завершения асинхронного метода
                // Debug.Log("2");

                HasInteracted = false;
            }

            IsInteracting = false;
        }

        private async UniTask StartInteractionTimer()
        {
            _interactionTimer = new UniTaskCompletionSource();
            var result = await UniTask.WhenAny(UniTask.Delay(TimeToRespond), _interactionTimer.Task);
            if (result == 0) // Если таймер истек
                // await OnHasNotInteracted();
                HasInteracted = false;
        }

        private void ResetInteractionTimer()
        {
            _interactionTimer?.TrySetResult();
        }
    }
}