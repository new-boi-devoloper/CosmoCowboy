using System;
using AssigningQuestSystem;
using Base;
using Cysharp.Threading.Tasks;
using DialogSystem;
using Interfaces;
using Managers;
using NPCData;
using NPCData.QuestData;
using UnityEngine;
using UnityEngine.Experimental.Audio;

namespace NPC_Variants
{
    public class QuestNpc :
        NpcBase,
        IQuester
    {
        [field: SerializeField] public QuestNpcSo questNpcSo;
        [field: SerializeField] public DialogController dialogController;
        [field: SerializeField] public QuestController questController;
        [field: SerializeField] public LevelType LevelType { get; private set; }
//bad solution, but sorry
        private Game _game;

        public override Base_NPC_SO NpcSo { get; protected set; }
        public override CapsuleCollider2D NpcCollider { get; protected set; }

        private void Start()
        {
            NpcSo = questNpcSo;
            NpcCollider = GetComponent<CapsuleCollider2D>();

        }

        public void Construct(Game game)
        {
            _game = game;
        }

        private void CallMissionComplete()
        {
            
            Debug.Log($"teleporting to {LevelType}");
            _game.SwitchLevel(LevelType);
            
            Destroy(gameObject);
        }

        public void Talk(string phraseToSay)
        {
            dialogController.ShowFirstPhrase(phraseToSay, NpcPosition);
        }

        public void AssignQuest()
        {
            if (questController != null) questController.CreateQuest(questNpcSo.amountToKill, CallMissionComplete);

            Debug.Log("Quest Assigned");
        }

        protected override async UniTask OnHasInteracted()
        {
            Debug.Log("pressed twice");
            dialogController.StartDialog(questNpcSo, NpcPosition);

            var isConfirmed = await dialogController.WaitForConfirmationOrCancellation();
            if (isConfirmed) AssignQuest();
        }

        protected override async UniTask OnHasNotInteracted()
        {
            dialogController.ResetFirstPhrase();
            Talk(questNpcSo.FirstPhrase);
        }
    }
}