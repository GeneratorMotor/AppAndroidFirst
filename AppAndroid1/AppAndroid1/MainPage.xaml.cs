using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Net;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using AppAndroid1.Classess;
using ZXing.Mobile;
using ZXing;
using AppAndroid1.Droid.Classess;
using AppAndroid1.Droid;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace AppAndroid1
{
    public partial class MainPage : ContentPage
    {
       
        // Создается один раз для всего приложения.
        private readonly HttpClient client;

        public const string TextCopyright = "Разработчик ЦКСЗН г. Саратов";

        // Строка подключения к БД.
        private readonly string con = "Server=10.159.102.21;Database=Android;User ID=myAndroid;Password=12A60Asd;";
        public MainPage()
        {
            InitializeComponent();


            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;


            client = new HttpClient();
        }

        private async void buttin1_Clicked(object sender, EventArgs e)
        {
            //// TODO: Пока не разобрался с Unit Testami. После теста удалить.
            //// Тестируем модулем из эмулятора.
            //// url к реальной WebApi.
            //var uri = "http://217.23.79.222:9090/api/Values/3";

            //GetMessage getMessage = new GetMessage();

            //// Получим ответ от сервера.
            //string resultDate = await getMessage.Get(uri);

            ////string json = @"{'id':3,'seriynNumber':'V216B064/3992','inventoryNumber':'1101040069','name':'Компьютер в сборе : CPU LGA 1155 Intel Core i5 3450 oem','firstName':'Дугин','expr1':'Евгений','secondName':'Викторович','departmentName':'332-05'}";
            //string json = @"{'id':3,'SeriynNumber':'V216B064/3992','InventoryNumber':'1101040069','Name' : CPU LGA 1155 Intel Core i5 3450 oem','FirstName':'Дугин','Expr1':'Евгений','SecondName':'Викторович','DepartmentName':'332-05'}";

            //string strJson = json.Replace("{", string.Empty).Replace("}", string.Empty);

            //string[] jsonArray = strJson.Split(',');

            //Dictionary<string, string> dictionary = new Dictionary<string, string>();

            //foreach (var str in jsonArray)
            //{
            //    string[] jsonArr = str.Split(':');

            //    if (jsonArr[0] != "Компьютер_в_сборе")
            //    {
            //        if (jsonArr.Length == 2)
            //        {
            //            dictionary.Add(jsonArr[0], jsonArr[1]);
            //        }
            //    }
            //    else
            //    {
            //        if (jsonArr.Length == 3)
            //        {
            //            string values = jsonArr[1] + " " + jsonArr[1];
            //            dictionary.Add(jsonArr[0], values);
            //        }
            //    }
            //}

            ////Распарсим строку с результатом ответа в JSON.
            //JObject o = JObject.Parse(resultDate);

            //var str = o.SelectToken(@"$.pre");

            ////string json = @"{'id':3,'seriynNumber':'V216B064/3992','InventoryNumber':'1101040069','name':'Компьютер в сборе : CPU LGA 1155 Intel Core i5 3450 oem','firstName':'Дугин','expr1':'Евгений','secondName':'Викторович','departmentName':'332-05'}"


            ////var str = o.SelectToken(@"$.query.results.ViewCompyter");


            ////var rateInfo = JsonConvert.DeserializeObject<ViewCompyter>(resultDate);

            //var rateInfo = JsonConvert.DeserializeObject<ViewCompyter>(json);

            //var test = "";

            //var test = resultDate.ToString();

            //string iTest = "";

            //string asd = "";

            //PageDisplay pd = new PageDisplay(rateInfo);

            //Navigation.PushAsync(pd);

            //string iTest = "";

            

            // /Проверим подключение к сети.
            var current = Connectivity.NetworkAccess;

            // Проверим доступна ли сеть.
            if (current == NetworkAccess.Internet)
            {
                //bool flagFlashlightError = false;

                //try
                //{
                //    // Включим фонарик.
                //    //await Flashlight.TurnOnAsync();
                //    await Flashlight.TurnOffAsync();


                //}
                //catch
                //{
                //    // TODO: Не знгаю как обработать ошибку при работе с фонариком.
                //    // Пока не знаю.
                //    flagFlashlightError = true;
                //}

                // TODO: Отключили пока камеру для теста.

                // Подключим камеру и прочитаем QR код.
                MobileBarcodeScanner scanner = new MobileBarcodeScanner();
               
                // Включим сканер в камере.
                scanner.UseCustomOverlay = true;

                // Установим свойства камеры.
                var opt = new MobileBarcodeScanningOptions();
                opt.PossibleFormats.Clear();
                opt.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE);
                opt.DelayBetweenContinuousScans = 3000;

                // Включим ингвертирование изображения.
                opt.TryInverted = true;



                try
                {
                    // TODO: Отключим камеру пока для теста
                    //Прочитаем ID записи из QR кода.
                    Result result = await scanner.Scan(opt);

                    // Если прочитали данные с камеры.
                    if (result != null)
                    {

                        //if (flagFlashlightError == false)
                        //{
                        //    Отключим фанарик.
                        //    await Flashlight.TurnOffAsync();
                        //await Flashlight.TurnOnAsync();
                        //}

                        // Подадим звуковой сигнал.
                        // Откажемся от вибрации дабы не насиловать телефон
                        var duration = TimeSpan.FromSeconds(1);
                        Vibration.Vibrate(duration);

                        // Подадим звуковой сигнал.
                        // Файл храниться в AppAndroid1.Android.Assets.
                        //MyMediaPlayer myMediaPlayer = new MyMediaPlayer("zvonok.mp3");
                        //myMediaPlayer.StartPlayer();
                    }


                    //string filePath = "Sound/zvonok.mp3";
                    // Моя реализация звукового сигнала.
                    //MyMediaPlayer myMediaPlayer = new MyMediaPlayer(filePath);

                    // Переменная для хранения id оргтехники для передачи в строку запроса.
                    string id = string.Empty;

                    // Получим значение id из QR кода
                    id = result.Text.Trim();

                    // TODO: Выключить тестовый путь на эмуляторе.
                    // Получим строку запроса к REST.
                    //var uri = "http://10.159.102.93:80/api/Values/" + id.Trim();

                    // url к реальной REST API.
                    //var uri = "http://217.23.79.222:9090/api/Values/" + id.Trim();
                    
                    // TODO: Для тестироования в эмуляторе.
                    var uri = "http://217.23.79.222:9090/api/Values/" + id.Trim();

                    // Получим данные о компьюторе.
                    GetMessage getMessage = new GetMessage();

                    // Получим ответ от сервера.
                    string jsonResultDate = await getMessage.Get(uri);

                    // Так как я долбаеб, и не разобрался с JObject
                    // Распарсим строку своими силами.

                    string[] jsonArray = jsonResultDate.Replace("{",string.Empty).Replace("}",string.Empty).Split(',');

                    // Модель компьютера.
                    ViewCompyter vk = new ViewCompyter();


                    // Счетчик.
                    int iCount = 1;

                    // Флаг указывает что мы должны отобразить данные по монитору.
                    bool flagDisplay = false;


                    // Распарсим строку на поля объекта.
                    foreach (var jstr in jsonArray)
                    {
                        string[] jsonArr = jstr.Split(':');

                        if (iCount == 1)
                        {
                            if (jsonArr != null && jsonArr[1] != null)
                            {
                                vk.id = Convert.ToInt32(jsonArr[1]);
                            }
                            else
                            {
                                vk.id = 0;
                            }
                        }
                        else if(iCount == 2)
                        {
                            if (jsonArr != null && jsonArr[1] != null)
                            {
                                vk.SeriynNumber = jsonArr[1].Trim();
                            }
                            else
                            {
                                vk.SeriynNumber = " - Ошибка выгрузки";
                            }
                        }
                        else if (iCount == 3)
                        {
                            if (jsonArr != null && jsonArr[1] != null)
                            {
                                vk.InventoryNumber = jsonArr[1].Trim();
                            }
                            else
                            {
                                vk.InventoryNumber = " - Ошибка выгрузки";
                            }
                        }
                        else if (iCount == 4)
                        {
                            if (jsonArr != null && jsonArr[1] != null)
                            {
                                vk.Name = jsonArr[1].Trim();
                            }
                            else
                            {
                                vk.Name = " - Ошибка выгрузки";
                            }
                        }
                        else if (iCount == 5)
                        {
                            if (jsonArr != null && jsonArr[1] != null)
                            {
                                vk.DepartmentName = jsonArr[1].Trim();
                            }
                            else
                            {
                                vk.DepartmentName = " - Ошибка выгрузки";
                            }
                        }
                        else if(iCount == 6)
                        {
                            if(jsonArr != null && jsonArr[1] != null)
                            {
                                vk.Сотрудник = jsonArr[1].Trim();
                            }
                            else
                            {
                            vk.Сотрудник = "Ошибка выгрузки";
                            }
                        }

                        iCount++;
                    }


                 
                    // TODO: Оставим пока не разберусь с JObject.
                    //// Распарсим строку с результатом ответа в JSON.
                    //JObject o = JObject.Parse(jsonResultDate);

                    ////var str = o.SelectToken(@"$.query.results.ViewCompyter");
                    //var str = o.SelectToken(@"$.pre");

                    ////var rateInfo = JsonConvert.DeserializeObject<ViewCompyter>(str.ToString());
                    //var rateInfo = JsonConvert.DeserializeObject<ViewCompyter>(jsonResultDate);

                    //// Проверим что мы получили системный блог, монитор или принтер.
                    //if(vk.flagDisplay == false && vk.flagPrinter == false)
                    //{
                        // Если оба флага лож, значит мы получили системный блок.
                        // Перейдем на другую страницу.
                        //PageDisplay pd = new PageDisplay(vk);
                        PageCompyter pd = new PageCompyter(vk);

                        // Добавим страницу в навигацию.
                        Navigation.PushAsync(pd);
                //}


            }
                catch (Exception exp)
            {
                await DisplayAlert("Ошибка", exp.Message, "Закрыть");
                //Если ошибка
                //inputId.Text = exp.Message;
            }

        }
            else
            {
                await DisplayAlert("Подключенние", "Подключение к сети отсутствует", "ОК");
            }

        }
    }
}
