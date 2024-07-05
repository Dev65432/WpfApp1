using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.DAL.Entityes;
using WpfApp1.DAL;
using WpfApp1.DAL.Context;
using Microsoft.Extensions.Configuration;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        private IConfiguration _configuration;

        // Repository --- ---- 
        private IRepository<Deal> _dealsRepository;        
        private IRepository<Picture> _pictureRepository;
        
        Deals2ViewModel _deals2ViewModel;        

        public MainWindowViewModel(IConfiguration configuration,
                                   IRepository<Deal> DealsRepository,                                   
                                   IRepository<Picture> pictureRepository)
        {
            
            _configuration = configuration;
            _dealsRepository = DealsRepository;
            
            _pictureRepository = pictureRepository;

            _deals2ViewModel = new Deals2ViewModel(_configuration,
                                                   _dealsRepository,
                                                   _pictureRepository);

            CurrentModel = _deals2ViewModel;
        }

        private string title = "Главное окно программы";

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private object menuModel;

        public object MenuModel
        {
            get { return menuModel; }
            set
            {
                menuModel = value;
                OnPropertyChanged(nameof(MenuModel));
            }
        }


        private object currentModel;

        public object CurrentModel
        {
            get { return currentModel; }
            set
            {
                currentModel = value;
                OnPropertyChanged(nameof(CurrentModel));
            }
        }

        #region LoadMenuCommand
        /// <summary>Отобразить представление статистики</summary>
        private ICommand _LoadMenuCommand;

        public ICommand LoadMenuCommand
        {
            get
            {
                return _LoadMenuCommand ??
                    (_LoadMenuCommand = new RelayCommand(() => OnLoadMenuExecuted()));
            }
        }


        private void OnLoadMenuExecuted()
        {
           
        }
        #endregion

    }
}