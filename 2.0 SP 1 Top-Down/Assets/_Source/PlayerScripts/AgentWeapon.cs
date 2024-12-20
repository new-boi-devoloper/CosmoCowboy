using System;
using System.Collections.Generic;
using AttackSystem;
using InventorySystem.Models;
using Managers.Sound;
using UnityEngine;

namespace PlayerScripts
{
    public class AgentWeapon : MonoBehaviour
    {
        [SerializeField] private Transform weaponPoint;
        // [SerializeField] private Vector3 offset; // Отступ от позиции weaponPoint
        // [SerializeField] private float rotationOffset; // Поворот по оси Z

        [field: SerializeField] public InventorySO InventoryData { get; private set; }
        [field: SerializeField] public List<ItemParameter> parametersToModify;
        [field: SerializeField] public List<ItemParameter> itemCurrentState;

        [Header("Tutor Info")] [SerializeField]
        private int index = 3;

        private GameObject _currentFireWeapon;

        private bool _oneTimeTutor = true;
        private EquippableItemSO Weapon { get; set; }

        private void Update()
        {
            // Устанавливаем позицию и поворот оружия в соответствии с weaponPoint с учетом отступа и поворота
            if (_currentFireWeapon != null)
            {
                _currentFireWeapon.transform.position = weaponPoint.position;
                _currentFireWeapon.transform.rotation =
                    Quaternion.Euler(0, 0, weaponPoint.eulerAngles.z);
            }
        }

        public static event Action<int> OnTutorRequest;

        public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            Debug.Log("weapon set");
            // Если уже есть оружие, добавляем его обратно в инвентарь
            if (Weapon != null) InventoryData.AddItem(Weapon, 1, itemCurrentState);

            Weapon = weaponItemSO;
            itemCurrentState = new List<ItemParameter>(itemState);
            ModifyParameters();
            CreateWeaponInstance(weaponItemSO.weaponPrefab); // Создаем экземпляр оружия из префаба
        }

        public void FireCurrentFireWeapon()
        {
            Debug.Log("Command Received");
            if (_currentFireWeapon != null)
            {
                var abstractFireWeapon = _currentFireWeapon.GetComponent<AbstractWeapon>();

                if (abstractFireWeapon != null)
                {
                    AudioManager.Instance.PlaySound(2); // звук выстрела бластера по индексу

                    abstractFireWeapon.Shoot();
                    Debug.Log("Weapon Shoot Invoked");
                }
                else
                {
                    Debug.LogWarning("currentFireWeapon does not have an AbstractWeapon component.");
                }
            }
            else
            {
                OneTimeSolutionForTutor();
                Debug.Log("No Weapon assigned");
            }
        }

        private void CreateWeaponInstance(GameObject weaponPrefab)
        {
            // Уничтожаем старое оружие, если оно есть
            if (_currentFireWeapon != null) Destroy(_currentFireWeapon);

            Debug.Log($"Создано оружие: {weaponPrefab}");
            _currentFireWeapon = Instantiate(weaponPrefab);
            _currentFireWeapon.transform.SetParent(weaponPoint);
            _currentFireWeapon.transform.localPosition = Vector3.zero;
            _currentFireWeapon.transform.localRotation = Quaternion.identity;
        }

        private void ModifyParameters()
        {
            foreach (var parameter in parametersToModify)
                if (itemCurrentState.Contains(parameter))
                {
                    var index = itemCurrentState.IndexOf(parameter);
                    var newValue = itemCurrentState[index].value + parameter.value;
                    itemCurrentState[index] = new ItemParameter
                    {
                        itemParameter = parameter.itemParameter,
                        value = newValue
                    };
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