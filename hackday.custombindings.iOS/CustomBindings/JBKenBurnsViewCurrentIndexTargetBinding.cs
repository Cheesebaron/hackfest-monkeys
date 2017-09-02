using System;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using XamarinStore;

namespace hackday.custombindings.iOS
{
    public class JBKenBurnsViewCurrentIndexTargetBinding : MvxConvertingTargetBinding
    {
        public JBKenBurnsViewCurrentIndexTargetBinding(JBKenBurnsView target) : base(target)
        {
        }

        public override Type TargetType => typeof(int);
        public override MvxBindingMode DefaultMode
        {
            get
            {
                return MvxBindingMode.TwoWay;
            }
        }

        protected override void SetValueImpl(object target, object value)
        {
            if (target is JBKenBurnsView burnsView)
            {
                if (burnsView != null)
                {
                    burnsView.CurrentIndex = (int)value;
                }
            }
        }

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();

            if (Target is JBKenBurnsView burnsView)
            {
                if (burnsView != null)
                {
                    burnsView.CurrentIndexChanged += BurnsView_CurrentIndexChanged;
                }
            }
        }

        void BurnsView_CurrentIndexChanged(object sender, int e)
        {
            FireValueChanged(e);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (isDisposing)
            {
                if (Target is JBKenBurnsView burnsView)
                {
                    if (burnsView != null)
                    {
                        burnsView.CurrentIndexChanged -= BurnsView_CurrentIndexChanged;
                    }
                }
            }
        }
    }

}
