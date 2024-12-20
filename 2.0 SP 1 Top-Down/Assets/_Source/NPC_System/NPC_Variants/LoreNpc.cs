using System;
using Base;
using Cysharp.Threading.Tasks;
using DialogSystem;
using Interfaces;
using NPCData;
using NPCData.SellerNPCData;
using UnityEngine;

namespace NPC_Variants
{
    public class LoreNpc :
        NpcBase,
        ILorer
    {
        [field: SerializeField] public LorerNpcSo LorerNpcSo { get; protected set; }
        [field: SerializeField] public DialogController dialogController;

        public override Base_NPC_SO NpcSo { get; protected set; }
        public override CapsuleCollider2D NpcCollider { get; protected set; }
        
        private void Start()
        {
            NpcSo = LorerNpcSo;
            NpcCollider = GetComponent<CapsuleCollider2D>();
        }

        public void Talk(string phraseToSay)
        {
            dialogController.ShowFirstPhrase(phraseToSay, NpcPosition);
        }

        public void CreateLore()
        {
            Debug.Log("Lore created");
        }

        protected override async UniTask OnHasInteracted()
        {
            if (dialogController != null) dialogController.StartDialog(LorerNpcSo, NpcPosition);
            var isConfirmed = false;
            if (dialogController != null) isConfirmed = await dialogController.WaitForConfirmationOrCancellation();
            if (isConfirmed) CreateLore();
        }

        protected override async UniTask OnHasNotInteracted()
        {
            if (dialogController != null) dialogController.ResetFirstPhrase();
            Talk(LorerNpcSo.FirstPhrase);
        }
    }
}