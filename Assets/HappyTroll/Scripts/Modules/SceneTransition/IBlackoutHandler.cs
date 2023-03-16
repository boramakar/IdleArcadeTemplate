using System;

namespace HappyTroll
{
    public interface IBlackoutHandler
    {
        public void FadeIn(Action callback);
        public void FadeOut(Action callback);
    }
}