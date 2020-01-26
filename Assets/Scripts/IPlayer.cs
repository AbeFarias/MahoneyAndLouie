using UnityEngine;

public interface IPlayer
{
    Animator Animator { get; set; }
    Rigidbody2D RB2D { get; set; }
    bool InControl { get; set; }
    void Attack();
}