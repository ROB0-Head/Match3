using Navigation;
using Scripts;
using UnityEngine;

namespace UI.Screens
{
    public class MainScreen : DefaultScreen
    {
        [SerializeField] private AnimatedButton _startButton;
        [SerializeField] private AnimatedButton _settingsButton;
        [SerializeField] private AnimatedButton _chatButton;
        [SerializeField] private AnimatedButton _spinWheelButton;
        [SerializeField] private AnimatedButton _infoButton;
        [SerializeField] private AnimatedButton _helpButton;
        [SerializeField] private AnimatedButton _pauseButton;

        private void Awake()
        {
            _startButton.onClick.AddListener(() => { SelectTab(MainTabType.Start); });
            _settingsButton.onClick.AddListener(() => { SelectTab(MainTabType.Settings); });
            _chatButton.onClick.AddListener(() => { SelectTab(MainTabType.Settings); });
            _spinWheelButton.onClick.AddListener(() => { SelectTab(MainTabType.Settings); });
            _infoButton.onClick.AddListener(() => { SelectTab(MainTabType.Start); });
            _helpButton.onClick.AddListener(() => { SelectTab(MainTabType.Start); });
            _pauseButton.onClick.AddListener(() => { SelectTab(MainTabType.Settings); });
        }

        public override void Setup(ScreenSettings settings)
        {
            if (settings is not MainScreenSettings mainScreenSettings)
                return;

            SelectTab(mainScreenSettings.TabType);
        }

        public void SelectTab(MainTabType tabType)
        {
            switch (tabType)
            {
                case MainTabType.Start:
                    /*
                    NavigationController.Instance.ScreenTransition<LevelScene>();
                    */
                    break;
                case MainTabType.Settings:
                    
                    break;
                case MainTabType.Chat:

                    break;
            }
        }


        public enum MainTabType
        {
            Start = 0,
            Settings = 1,
            Chat = 2,
            SpinWheel = 5,
            Info = 6,
            Help = 7,
            Pause = 8,
            
        }
    }

    public class MainScreenSettings : ScreenSettings
    {
        public MainScreen.MainTabType TabType;
    }
}