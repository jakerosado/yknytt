using Godot;

public class Spring : GDKnyttBaseObject
{
    Juni juni;

    public override void _Ready()
    {
        GetNode<Sprite>("Sprite").Modulate *= new Color(1, 1, 1, 0);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (juni == null) { return; }
        if (juni.GlobalPosition.y > GlobalPosition.y + 10f) { return; }

        // Restore Juni's position to the top of the spring for stability
        juni.GlobalPosition -= new Vector2(0, juni.Bottom.y - GlobalPosition.y);
        juni.MoveAndSlide(Godot.Vector2.Zero, Godot.Vector2.Up, true); // workaround to prevent landing

        // Spring
        // TODO: not accurate for highjump+hold and lowjump+nohold. maybe try to get rid of HIGH_JUMP_DEFAULT_POWER?
        juni.executeJump(juni.Swim ? -101f : -340f, sound: false, reset_jumps: true);
        juni.playSound("bounce");

        var anim = GetNode<AnimationPlayer>("AnimationPlayer");
        anim.Stop();
        anim.Play("Spring");
        juni = null;
    }

    public void _on_Area2D_body_entered(Node body)
    {
        if (body is Juni juni) { this.juni = juni; }
    }

    public void _on_Area2D_body_exited(Node body)
    {
        if (body is Juni juni && juni == this.juni) { this.juni = null; }
    }
}
