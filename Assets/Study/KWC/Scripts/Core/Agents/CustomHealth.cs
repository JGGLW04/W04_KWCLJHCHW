using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine.Serialization;

public class CustomHealth : Health
    {
        [MMInspectorGroup("GUI", true, 12)]
        public HealthBubble healthBubble;

        public override void UpdateHealthBar(bool show)
        {
            base.UpdateHealthBar(show);
        
            if (healthBubble != null)
            {
                healthBubble.UpdateHeart((int)CurrentHealth, (int)MaximumHealth);
            }
        }
    }