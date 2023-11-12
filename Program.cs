using System.Net.Mime;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.BotAPI;
using InlineKeyboardButton = Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton;
using ReplyKeyboardMarkup = Telegram.BotAPI.AvailableTypes.ReplyKeyboardMarkup;
using UpdateType = Telegram.Bot.Types.Enums.UpdateType;

namespace Program
{

    public class Start
    {
        private static readonly TelegramBotClient _bot =
            new TelegramBotClient("6503722531:AAGK-4YyevTon5p3tm8-NSkBCItdCIyTurc");

        private static readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private static readonly ReceiverOptions _receiverOptions = new ReceiverOptions {AllowedUpdates = { }};
        
        public async static Task Main(string[] args)
        {
            while (true)
            {
                _bot.StartReceiving(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    _receiverOptions,
                    cancellationToken:
                    _cts.Token );
                
                Console.ReadLine();
                await Task.Delay(-1);
            }
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cts)
        {
        
            
            // keyboard = types.ReplyKeyboardMarkup(row_width=1) #создаем клавиатуру
            //     webAppTest = types.WebAppInfo("https://telegram.mihailgok.ru") #создаем webappinfo - формат хранения url
            //     one_butt = types.KeyboardButton(text="Тестовая страница", web_app=webAppTest) #создаем кнопку типа webapp
            // keyboard.add(one_butt) #добавляем кнопки в клавиатуру
            //
            // return keyboard #возвращаем клавиатуру
            Console.WriteLine("ddd");
            if (update.Message.Type == MessageType.WebAppData)
            {
                Console.WriteLine("Webb info");
            }
            if (update.Message.Text != null && update.Type == UpdateType.Message )
            {
                InlineKeyboardButton button = new InlineKeyboardButton("Text");
                //button.CallbackData = "back";
                //button.Url = webapp.Url;

                
                var webapp = new WebAppInfo();
                webapp.Url = "https://dim-dzimych.github.io/WebAppCheckData/";
                

                KeyboardButton one = KeyboardButton.WithWebApp("textTest",webapp);
                WebAppData data = new WebAppData();
                //data.Data
                

                 await bot.SendTextMessageAsync(update.Message.Chat.Id, "БОТ веб", replyMarkup: new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup(one));
            }
        }
        public static async Task<Task> HandleErrorAsync(ITelegramBotClient bot, Exception exception, CancellationToken cts)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"API ERROR -{apiRequestException.ErrorCode}",
                _ => exception.Message
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }

}