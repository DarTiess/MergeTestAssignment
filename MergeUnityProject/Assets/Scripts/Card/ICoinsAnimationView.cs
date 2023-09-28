using UnityEngine;

namespace Card
{
    public interface ICoinsAnimationView
    {
        void GetCoinsAnimation(Transform startPosition);
        void ShowUpgradeEffect(Transform setPosition);
        void SpendCoinsAnimation();
    }
}