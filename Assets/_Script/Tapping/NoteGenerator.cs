using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : Button {

    public List<ParticleSystem> particles;
    public Level level;

    private void Start()
    {
        particles.ForEach(p => p.Stop());
    }

    public override void Clicked()
    { 
        if(level.inTime)
            particles.ForEach(p=> p.Emit(2));
        
    }
}
