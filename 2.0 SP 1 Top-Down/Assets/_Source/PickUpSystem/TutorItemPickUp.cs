using System;
using UnityEngine;
using Utils;

namespace PickUpSystem
{
    public class TutorItemPickUp : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private int index;
        public static event Action<int> OnTutorRequest;

        private bool _oneTimeTutor = true;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerChecker.ContainsLayer(playerLayer, other.gameObject))
            {
                OneTimeSolutionForTutor();
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
    }
}