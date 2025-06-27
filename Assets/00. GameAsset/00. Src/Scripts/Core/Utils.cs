// # Unity 
using UnityEngine;

// # ETC
using DG.Tweening;

public static class Utils
{
    public static class Dotween
    {
        public static void PlayScaleAnimation(Transform obj, Vector3 size, float duration)
        {
            obj.DOScale(size, duration);
        }

        public static void PlayScaleAnimation(Transform obj, Vector3 size, float duration, TweenCallback tweenCallback)
        {
            obj.DOScale(size, duration).OnComplete(tweenCallback);
        }
    }
}