using System;
using System.Windows;
using System.Windows.Controls;


namespace MeioMundoEditor.CustomsControls
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : UserControl
    {
        public enum ProgresBarState
        {
            Working,
            Busy,
            Stop
        }


        public ProgresBarState State
        {
            get { return (ProgresBarState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        public ProgresBarState state = ProgresBarState.Busy;
        // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(ProgresBarState), typeof(ProgressBar), new PropertyMetadata(ProgresBarState.Stop, ProgressBarStateChanged));

        public int Percentagem
        {
            get { return (int)GetValue(PercentagemProperty); }
            set { SetValue(PercentagemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Percentagem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercentagemProperty =
            DependencyProperty.Register("Percentagem", typeof(int), typeof(ProgressBar), new PropertyMetadata(0, ProgressBarValueChnage));


        public static readonly RoutedEvent SwicthStateBusyEvent =
            EventManager.RegisterRoutedEvent("StateBusy", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ProgressBar));

        #region Eventos 

        public event EventHandler ProgressBarCompleted;
        public event RoutedEventHandler StateBusy
        {
            add { this.AddHandler(SwicthStateBusyEvent, value); Console.WriteLine("D"); }
            remove { this.AddHandler(SwicthStateBusyEvent, value); }
        }

        #endregion


        public double progressBarWidth { get; set; }

        public ProgressBar()
        {
            InitializeComponent();         
        }

        #region Secção de Fazer andar o slider da barra

        private static void ProgressBarValueChnage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBar obj = d as ProgressBar;
            obj.SetBarValue((int)e.NewValue);
        }

        /// <summary>
        /// Defenia a percentagem da barra de progresso
        /// </summary>
        /// <param name="value">Percentagem</param>
        public void SetBarValue(int value)
        {
            if (value >= 100)
            {
                OnCompleted(EventArgs.Empty);
            }
            fill.Width = 12 + (value * ((int)border.Width - 12) / 100);
            info.Text = string.Format("{0} %", value);
        }
        /// <summary>
        /// Acontece quando a percentagem chega a 100%
        /// </summary>
        /// <param name="e">Retrun null</param>
        protected virtual void OnCompleted(EventArgs e)
        {
            EventHandler handler = ProgressBarCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion


        #region Secção de mudanças de Estado
        /// <summary>
        /// Ocorre quando a propriedade muda
        /// </summary>
        /// <param name="d">objecto de onde veio</param>
        /// <param name="e">propriedade do novo e velho valor</param>
        private static void ProgressBarStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBar progressBar = (ProgressBar)d;
            ProgresBarState state = (ProgresBarState)e.NewValue;
            progressBar.ChangeState(state);
        }
        /// <summary>
        /// Muda o estado a logica da representação grafica
        /// </summary>
        /// <param name="state">Para qual estado vai a seguir</param>
        public void ChangeState(ProgresBarState state)
        {
            switch (state)
            {
                case ProgresBarState.Working:
                    break;
                case ProgresBarState.Busy:
                    //RoutedEventArgs routedEvent = new RoutedEventArgs(ProgressBar.SwicthStateBusyEvent);
                    RaiseEvent(new RoutedEventArgs(SwicthStateBusyEvent));
                    Console.WriteLine("sad");
                    break;
                case ProgresBarState.Stop:
                    break;
                default:
                    break;
            }
        }

        public void RaiseTestEvent()
        {
            RoutedEventArgs routedEvent = new RoutedEventArgs(ProgressBar.SwicthStateBusyEvent);
            RaiseEvent(routedEvent);

        }
        #endregion

    }
}
