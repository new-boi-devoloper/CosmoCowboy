using NPCData;

namespace DialogSystem.Models
{
    public class DialogModel
    {
        public DialogModel(Base_NPC_SO npc)
        {
            NPC = npc;
            CurrentLineIndex = 0;
        }

        public Base_NPC_SO NPC { get; }
        public int CurrentLineIndex { get; private set; }

        public string GetCurrentLine()
        {
            // Используем массив questLines из Base_NPC_SO
            if (CurrentLineIndex < NPC.questLines.Length)
            {
                return NPC.questLines[CurrentLineIndex];
            }
            return NPC.FirstPhrase; // Если реплик нет, возвращаем первую фразу
        }

        public void NextLine()
        {
            if (CurrentLineIndex < NPC.questLines.Length - 1)
            {
                CurrentLineIndex++;
            }
        }

        public void PreviousLine()
        {
            if (CurrentLineIndex > 0)
            {
                CurrentLineIndex--;
            }
        }
    }
}