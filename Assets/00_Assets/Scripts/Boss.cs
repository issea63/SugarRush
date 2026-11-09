using UnityEngine;

public class Boss : Interactable
{
    CharacterStats stats;

    private void Start()
    {
        player = PlayerManager.Instance.player.transform;
        stats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = PlayerManager.Instance.GetComponent<CharacterCombat>();
        playerCombat.Attack(stats);
    }
}
