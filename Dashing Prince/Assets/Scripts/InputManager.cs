using UnityEngine;

public static class InputManager
{
    private static GameControls _ctrls;

    public static void Init(Player p)
    {
        _ctrls = new();

        _ctrls.InGame.Movement.performed += a=> { 
            p.SetMovementDirection(a.ReadValue<Vector2>()); 
        };

        _ctrls.InGame.Jump.performed += b => { //look for input
            p.Jump();  //action performed
        };

        _ctrls.InGame.Dash.performed += c => {
            p.Dash(c.ReadValue<Vector2>());
        };

        _ctrls.InGame.Attack.performed += d => {
            p.Attack();  //action performed
        };

    }

    public static void EnableInGame()
    {
        _ctrls.InGame.Enable();
    }
}