using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using hackday.custombindings.Core.ViewModels;
using XamarinStore;
using UIKit;
using Cirrious.FluentLayouts.Touch;

namespace hackday.custombindings.iOS.Views
{
    public class FirstView : MvxViewController
    {
        private JBKenBurnsView _jkView;
        private UILabel _currentIndexLabel;
        private UIStackView _buttonStack;

        public override void LoadView()
        {
            base.LoadView();

            Title = "Monkeys!";

			View.BackgroundColor = UIColor.White;

			_jkView = new JBKenBurnsView();
			_currentIndexLabel = new UILabel
			{
				TextAlignment = UITextAlignment.Center
			};
			_buttonStack = new UIStackView
			{
				Spacing = 5,
				Axis = UILayoutConstraintAxis.Horizontal,
				Distribution = UIStackViewDistribution.FillEqually
			};

            for (var i = 1; i <= 6; i++)
            {
                var image = UIImage.FromBundle($"monkey{i}.jpg");
                _jkView.Images.Add(image);
            }

			Add(_jkView);
			Add(_currentIndexLabel);
			Add(_buttonStack);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            set.Bind(_currentIndexLabel).To(vm => vm.CurrentIndex);

            // todo: only one way until Custom Target Binding has been made!
            set.Bind(_jkView).For(v => v.CurrentIndex).To(vm => vm.CurrentIndex);

            for (var i = 1; i <= 6; i++)
            {
                var button = new UIButton(UIButtonType.RoundedRect);
                button.SetTitle($"{i}", UIControlState.Normal);

                _buttonStack.AddArrangedSubview(button);

                set.Bind(button).To(vm => vm.ChangeIndexCommand).CommandParameter(i);
            }

            set.Apply();

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                _jkView.AtTopOf(View, 40f),
                _jkView.AtLeftOf(View),
                _jkView.AtRightOf(View),
                _jkView.Height().EqualTo(300f),

                _currentIndexLabel.Below(_jkView, 10f),
                _currentIndexLabel.AtLeftOf(View, 10f),
                _currentIndexLabel.AtRightOf(View, 10f),

                _buttonStack.Below(_currentIndexLabel, 20f),
                _buttonStack.AtLeftOf(View),
                _buttonStack.AtRightOf(View)
            );
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            _jkView.Animate();
        }
    }
}
