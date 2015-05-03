namespace GitterClient.ViewModel
{
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            
            SimpleIoc.Default.Register(() => new MainViewModel());
            SimpleIoc.Default.Register(() => new RoomsViewModel(NavigationService));
            SimpleIoc.Default.Register(() => new RoomViewModel(NavigationService), true);
        }

        /// <summary>
        /// The create navigation service.
        /// </summary>
        /// <returns>
        /// The <see cref="INavigationService"/>.
        /// </returns>
        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();

            navigationService.Configure("Main", typeof(MainPage));
            navigationService.Configure("Room", typeof(RoomPage));
            navigationService.Configure("Rooms", typeof(RoomsPage));
            navigationService.Configure("Authentication", typeof(AuthenticationPage));

            return navigationService;
        }

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        public INavigationService NavigationService
        {
            get { return ServiceLocator.Current.GetInstance<INavigationService>(); }
        }

        /// <summary>
        /// Gets the main view model.
        /// </summary>
        public MainViewModel MainViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        /// <summary>
        /// Gets the rooms view model.
        /// </summary>
        public RoomsViewModel RoomsViewModel
        {
            get { return ServiceLocator.Current.GetInstance<RoomsViewModel>(); }
        }

        /// <summary>
        /// Gets the room view model.
        /// </summary>
        public RoomViewModel RoomViewModel
        {
            get { return ServiceLocator.Current.GetInstance<RoomViewModel>(); }
        }

        /// <summary>
        /// The cleanup.
        /// </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}