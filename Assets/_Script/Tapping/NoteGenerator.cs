using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : Button {

    public List<ParticleSystem> particles;
    public Level level;

    private void Start()
    {
        base.Init();
        SaveToFile.instance.AddLog(gameObject.transform.parent.name);
        particles.ForEach(p => p.Stop());
    }

    public override void Clicked()
    {
        base.Clicked();
        if(level.inTime)
            particles.ForEach(p=> p.Emit(2));
        
    }

    public override string GetResult()
    {
        return base.GetResult();
    }

    public override bool WasGood()
    {
        return base.WasGood();
    }
}
