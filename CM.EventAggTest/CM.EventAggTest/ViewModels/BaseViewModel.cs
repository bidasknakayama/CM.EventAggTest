using Caliburn.Micro;
using CM.EventAggTest.Message;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.EventAggTest.ViewModels
{
    public class BaseViewModel : Conductor<object>,
        IHandle<HelloMessage>
    {


        private readonly IEventAggregator _eventAggregator;

        #region Constructors
        public BaseViewModel()
        {

            _eventAggregator = IoC.Get<IEventAggregator>();



        }
        protected override void OnInitialize()
        {
            base.OnInitialize();


        }
        protected override async void OnActivate()/* OK */
        {
            base.OnActivate();

            _eventAggregator.Subscribe(this);



        }
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);




        }
        #endregion

        public void PublishOnBackgroundThread(int flag) {

            Debug.WriteLine($"PublishOnBackgroundThread/{flag}");
            ix++;
            if( flag == 0)
            {
                _eventAggregator.PublishOnBackgroundThread(new HelloMessage(ix.ToString(), false));
            }
            else if( flag == 1)
            {
                _eventAggregator.PublishOnBackgroundThread(new HelloMessage(ix.ToString(), true));
            }
        }

        public void PublishOnCurrentThread(int flag)
        {
            Debug.WriteLine($"PublishOnCurrentThread/{flag}");
            ix++;
            if (flag == 0)
            {
                _eventAggregator.PublishOnCurrentThread(new HelloMessage(ix.ToString(), false));
            }
            else if (flag == 1)
            {
                _eventAggregator.PublishOnCurrentThread(new HelloMessage(ix.ToString(), true));
            }
        }
        public void PublishOnUIThread(int flag)
        {
            Debug.WriteLine($"PublishOnUIThread/{flag}");
            ix++;
            if (flag == 0)
            {
                _eventAggregator.PublishOnUIThread(new HelloMessage(ix.ToString(), false));
            }
            else if (flag == 1)
            {
                _eventAggregator.PublishOnUIThread(new HelloMessage(ix.ToString(), true));
            }
        }
        public void PublishOnUIThreadAsync(int flag)
        {
            Debug.WriteLine($"PublishOnUIThreadAsync/{flag}");
            ix++;
            if (flag == 0)
            {
                _eventAggregator.PublishOnUIThreadAsync(new HelloMessage(ix.ToString(), false));
            }
            else if (flag == 1)
            {
                _eventAggregator.PublishOnUIThreadAsync(new HelloMessage(ix.ToString(), true));
            }
        }

        public void Handle(HelloMessage message)
        {

            Debug.WriteLine($"Handle(HelloMessage message)/{message.UiAsync}/{message.msg}");
            if (message.UiAsync)
            {
                Execute.OnUIThreadAsync(() =>
                {
                    _myText = message.msg;
                    MyText = _myText;

                });
                /*Execute.OnUIThread(() =>
                {
                    _myText = message.msg;
                    MyText = _myText;

                });*/
            }
            else
            {
                _myText = message.msg;
                MyText = _myText;
            }
        }
        
        private int ix = 0;
        private String _myText = "Update Number at Here !";
        public String MyText
        {
            get { return _myText; }
            set
            {
                Debug.WriteLine($"this.Set(ref _myText, value);");
                this.Set(ref _myText, value);
            }
        }

    }
}