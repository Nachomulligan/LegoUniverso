using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnWeapon", menuName = "Commands/SpawnWeapon", order = 0)]
public class SpawnWeaponCommand : CommandSO
{
    [SerializeField] private GameObject weapon;

    public override void Execute()
    {
        Character character = FindObjectOfType<Character>();

        if (character != null)
        {
            Vector3 spawnPosition = character.transform.position + character.transform.forward;
            Instantiate(weapon, spawnPosition, Quaternion.identity);
        }
    }
}
