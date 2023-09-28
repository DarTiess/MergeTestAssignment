using UnityEngine;

namespace Card.States
{
    public abstract class State 
    {
        public virtual Sprite Enter()
        {
            return null;
        }
        public virtual Sprite Exit()
        {
            return null;
            
        }
      
    }
}