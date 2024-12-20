using UnityEngine;

namespace NPCData
{
    public abstract class Base_NPC_SO : ScriptableObject
    {
        [field: SerializeField] public string NPCName { get; set; }

        [field: TextArea]
        [field: SerializeField]
        public string FirstPhrase { get; set; }
        
        [TextArea] public string[] questLines; // adjustable number of lines 

    }
}