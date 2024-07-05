using System;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp1.Commands;

using WpfApp1.DAL.Entityes;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WpfApp1.DAL;
using System.Windows;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Extensions.Configuration;

namespace WpfApp1.ViewModels
{
    public class Deals2ViewModel : ViewModelBase
    {
        IConfiguration _configuration;
        private IRepository<Deal> _dealsRepository;
        private IRepository<Picture> _pictureRepository;

        public Deals2ViewModel(IConfiguration configuration,
                               IRepository<Deal> DealsRepository,
                               IRepository<Picture> pictureRepository)
        {
            _configuration = configuration;
            _dealsRepository = DealsRepository;
            _pictureRepository = pictureRepository;
        }

        private string _title = "Заголовок. DealsViewModel ";

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }




        #region Deals : BindingList<Deal> - Коллекция сделки
        private Deal _selectedDeal;

        public Deal SelectedDeal
        {
            get { return _selectedDeal; }
            set
            {
                _selectedDeal = value;
                OnPropertyChanged(nameof(SelectedDeal));
            }
        }



        private BindingList<Deal> _deals;

        public BindingList<Deal> Deals
        {
            get { return _deals; }
            set
            {
                _deals = value;
                OnPropertyChanged(nameof(Deals));
            }
        }
        #endregion

        #region  Command LoadDataCommand - Команда загрузки данных из репозитория
        /// <summary>Отобразить представление статистики</summary>
        private ICommand _loadDataCommand;

        public ICommand LoadDataCommand
        {
            get
            {
                return _loadDataCommand ??
                    (_loadDataCommand = new RelayCommand(() => OnLoadDataExecuted()));
            }
        }

        private async Task OnLoadDataExecuted()
        {
            try
            {

                // Deals 
                var items = await _dealsRepository.Items.ToArrayAsync();
                Deals = new BindingList<Deal>(items);
                SelectedDeal = Deals[1];


            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }

        }

        #endregion


        #region  Command InsertPictureOfOpeningOftransactionCommand - Команда Вставить картинку открытия сделки
        /// <summary>Отобразить представление статистики</summary>
        private ICommand _insertPictureOfOpeningOftransactionCommand;

        public ICommand InsertPictureOfOpeningOftransactionCommand
        {
            get
            {
                return _insertPictureOfOpeningOftransactionCommand ??
                    (_insertPictureOfOpeningOftransactionCommand
                            = new RelayCommand(()
                            => InsertPictureOfOpeningOftransactionExecuted()));
            }
        }

        private async Task InsertPictureOfOpeningOftransactionExecuted()
        {
            try
            {
                if (Clipboard.ContainsImage())
                {
                    string typeDeal = "open";
                    // Имя файла
                    string nameFile = $"idDeal-{SelectedDeal.Id}_type-{typeDeal}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.jpg";

                    // Сохранить в файл
                    SaveToFile(nameFile);

                    // Сохранить в БД
                    var rnd = new Random();

                    int i = rnd.Next(0, 10000);

                    Picture picture = new Picture
                    {
                        // Id = i,
                        Id = 6905,
                        Name = nameFile,
                        DealId = SelectedDeal.Id,
                        Deal = SelectedDeal
                    };

                    SelectedDeal.Pictures.Add(picture);

                    // _dealsRepository --- --- --- --- ---
                    //var dealUp = _dealsRepository.Get(SelectedDeal.Id);
                    //dealUp.Pictures = SelectedDeal.Pictures;

                    //_dealsRepository.Update(dealUp);

                    // _pictureRepository --- --- --- --- ---
                    //var picUp = _pictureRepository.Get(picture.Id);
                    //if (picUp == null)
                    //{
                    //    _pictureRepository.Add(picture);
                    //    return;
                    //}
                    //picUp = picture;
                    //_pictureRepository.Update(picUp);

                    // _pictureRepository --- --- --- --- ---
                    //var picUp = _pictureRepository.Get(picture.Id);
                    //if (picUp == null)
                    //{
                    //    _pictureRepository.Add(picture);
                    //    return;
                    //}
                    //picUp.Name = picture.Name;
                    //_pictureRepository.Update(picUp);

                    //// _pictureRepository --- --- --- --- ---
                    //var picUp = _pictureRepository.Get(picture.Id);
                    //if (picUp == null)
                    //{
                    //    _pictureRepository.Add(picture);
                    //    return;
                    //}
                    //// picUp.Name = picture.Name;
                    //_pictureRepository.Update(picture);


                    // =====

                    // _pictureRepository --- --- --- --- ---
                    var picUp = _pictureRepository.ge Get(picture.Id);
                    if (picUp == null)
                    {
                        _pictureRepository.Add(picture);
                        return;
                    }
                    picUp = picture;
                    _pictureRepository.Update(picUp);


                }
                else
                {
                    // Console.WriteLine("Буфер обмена не содержит изображения.");
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }

        }

        #endregion


        #region MethodsPrivate
        private void SaveToFile(string nameFile)
        {
            var image = Clipboard.GetImage();

            // Папка
            // var directoryPath =  @"AppFiles\Imgs\";
            var directoryPath = _configuration["Path"];
            // Создаем папку, если она еще не существует
            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryPath));

            // Полный путь
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryPath, nameFile);
            // var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryPath, "img1.jpg");


            // Сохраняем в файл
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }

        }

        #endregion

    }
}

