namespace Instruments
{
    // [CustomEditor(typeof(EnemyBase))]
    // public class EnemyBaseEditor : Editor
    // {
    //     public override void OnInspectorGUI()
    //     {
    //         var enemyBase = (EnemyBase)target;
    //
    //         EditorGUILayout.LabelField("Enemy Setup", EditorStyles.boldLabel);
    //         //Serialize Object Input
    //         enemyBase.EnemyTeamConfig = (EnemyTeamConfig)EditorGUILayout.ObjectField("Enemy Team Config",
    //             enemyBase.EnemyTeamConfig, typeof(EnemyTeamConfig), false);
    //
    //         enemyBase.EnemyCharacter =
    //             (CharacterType)EditorGUILayout.EnumPopup("Character Type", enemyBase.EnemyCharacter);
    //
    //         enemyBase.isMoving = EditorGUILayout.Toggle("Is Moving", enemyBase.isMoving);
    //         if (enemyBase.isMoving)
    //         {
    //             enemyBase.moveType = (MoveType)EditorGUILayout.EnumPopup("Move Type", enemyBase.moveType);
    //             if (enemyBase.moveType == MoveType.Patrol)
    //             {
    //                 EditorGUILayout.LabelField("Patrol Points", EditorStyles.boldLabel);
    //                 var patrolPoints = serializedObject.FindProperty("patrolPoints");
    //                 EditorGUILayout.PropertyField(patrolPoints, true);
    //                 serializedObject.ApplyModifiedProperties();
    //             }
    //         }
    //
    //         enemyBase.isAttacking = EditorGUILayout.Toggle("Is Attacking", enemyBase.isAttacking);
    //         if (enemyBase.isAttacking)
    //             enemyBase.attackType = (AttackType)EditorGUILayout.EnumPopup("Attack Type", enemyBase.attackType);
    //
    //         enemyBase.isAlerting = EditorGUILayout.Toggle("Is Alerting", enemyBase.isAlerting);
    //
    //         if (GUI.changed) EditorUtility.SetDirty(enemyBase);
    //     }
    // }
}