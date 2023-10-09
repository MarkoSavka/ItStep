using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

internal class Program
{
    static TelegramBotClient tgbot;
    static long chatId;
    private static bool directionsButtonClicked = false; 
    private static void Main(string[] args)
    {
        tgbot = new TelegramBotClient("6401616545:AAHMgQ_sqZO3uy9xxAmIcSgvGrpgX6jg3Fo");
        tgbot.OnMessage += Bot_OnMessageAsync;
        tgbot.OnCallbackQuery += Bot_OnCallbackQuery;
        tgbot.StartReceiving(); 
        var me = tgbot.GetMeAsync().Result;
        Console.WriteLine(me.FirstName);
        Console.ReadKey();
        tgbot.StopReceiving();
    }
    private static async void Buttons(long chatid)
    {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
       {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Напрямки"),
                InlineKeyboardButton.WithCallbackData("Як добратись")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Контакти"),
                InlineKeyboardButton.WithCallbackData("Офіційний сайт")
            },
            //new[]
            //{
            //    InlineKeyboardButton.WithCallbackData("Завершити роботу")
            //}
        }); 
        // Надіслати контакти разом з інлайн-клавіатурою
        await tgbot.SendTextMessageAsync(chatId, "Виберіть питання яке Вас цікавить:", replyMarkup: inlineKeyboard);
    }
    private static async void Bot_OnMessageAsync(object? sender, MessageEventArgs e)
    {
        chatId = e.Message.Chat.Id;
        Console.WriteLine(chatId);
        var message = e.Message;

        if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
        {
            return;
        }
        string name = $"{message.From.FirstName} {message.From.LastName} {message.From.Id}";
        Console.WriteLine($"{name} відправив повідомлення: {message.Text}");
        Console.WriteLine();
        //було свічом message.text==
        //i case case case
        if (message.Text == "/start")
        {
            string text = @"Вас вітає офіційний бот Itstep Academy!";
            await tgbot.SendPhotoAsync(message.From.Id, "https://scontent.flwo6-1.fna.fbcdn.net/v/t39.30808-6/326063001_1455762454951473_2231666051157284516_n.jpg?_nc_cat=104&ccb=1-7&_nc_sid=a2f6c7&_nc_ohc=x5rNEkMHYToAX953_aq&_nc_ht=scontent.flwo6-1.fna&oh=00_AfDwgPhLbvS8EqdLfJJmYrm3ALbihaPEDq1iauuimmqEWg&oe=6521D30F");
            await tgbot.SendTextMessageAsync(message.From.Id, text);

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
            //new[]
            //{
            //    InlineKeyboardButton.WithCallbackData("Почати роботу"),
            //},
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Напрямки"),
                InlineKeyboardButton.WithCallbackData("Як добратись")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Контакти"),
                InlineKeyboardButton.WithCallbackData("Офіційний сайт")
            },
            //new[]
            //{
            //    InlineKeyboardButton.WithCallbackData("Завершити роботу")
            //}
        });
            Thread.Sleep(1000);
            await tgbot.SendTextMessageAsync(message.From.Id, "Виберіть питання, яке Вас цікавить:", replyMarkup: inlineKeyboard);
        }
        else if (directionsButtonClicked)
        {
            // Перевіряємо, яку кнопку "Напрямки" вибрав користувач
            if (message.Text == "Програмування" || message.Text == "програмування")
            {
                await tgbot.SendTextMessageAsync(message.From.Id, $"Так, у нас є такий напрямок\nОсь посилання:\nhttps://lviv.itstep.org/adult_IT_courses#c302");
                Buttons(chatId);
            }
            else if (message.Text == "Дизайн" || message.Text == "дизайн")
            {
                await tgbot.SendTextMessageAsync(message.From.Id, $"Так, у нас є такий напрямок\nОсь посилання:\nhttps://lviv.itstep.org/adult_IT_courses#c305");
                Buttons(chatId);
            }
            else if (message.Text == "Маркетинг" || message.Text == "маркетинг")
            {
                await tgbot.SendTextMessageAsync(message.From.Id, $"Так, у нас є такий напрямок\nОсь посилання:\nhttps://lviv.itstep.org/adult_IT_courses#c308");
                Buttons(chatId);
            }
            else if (message.Text == "Кібербезпека" || message.Text == "кібербезпека")
            {
                await tgbot.SendTextMessageAsync(message.From.Id, $"Так, у нас є такий напрямок\nОсь посилання:\nhttps://lviv.itstep.org/adult_IT_courses#c314");
                Buttons(chatId);
            }
            else
            {
                await tgbot.SendTextMessageAsync(message.From.Id, $"Такого напрямку у нас на жаль нема,спробуйте ще");
                Buttons(chatId);
            }
                directionsButtonClicked = false;
        }
    }
    private static async void Bot_OnCallbackQuery(object? sender, CallbackQueryEventArgs e)
    {
        string buttonText = e.CallbackQuery.Data;
        string messageText = e.CallbackQuery.Message.From.FirstName;
        string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName} {e.CallbackQuery.From.Id}";
        Console.WriteLine($"Користувач {name} натиснув: {buttonText}");
        if (buttonText=="Контакти")
        {
            string contacts =$"КОНТАКТИ\n\n"+
                "Приймальна комісія" +
                $"ПН.-ПТ.: 09:00-19:00 СБ.: 09:00-15:00\nНД.: ВИХІДНИЙ\n" +
                $"(067) 557-87-06,\n(050) 441-76-00\nlviv@itstep.org\n\n"
                +
                $"Навчальна частина\n" +
                $"ПН.-СБ.: 9:00-19:00 \nНД.: 10:00-17:00\n" +
                $"(073) 797-89-01,\n+380961754705\n\n"
                +
                "Бухгалтерія" +
                $"\nПН.-ПТ.: 09:00-18:30 \nСБ.: 09:00-15:00 НД.: ВИХІДНИЙ\n" +
                $"(073) 797-89-05\n\n"
                +
                $"Маркетинговий відділ\n"+
                $"mykhailyshyn_o@itstep.org\n\n"
                +
                "HR відділ" +
                $"\nklimko_n@itstep.org\n\n";
            await tgbot.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, contacts);
            Buttons(chatId);

        }
        else if (buttonText== "Як добратись")
        {
            float latitude = 49.857862f;
            float longitude = 24.0258832f;
            string locationTitle = "IT Step Academy";

            string locationAddress = "📍м. Львів, вул. Замарстинівська, 83а";
            await tgbot.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Локації: ");
            await tgbot.SendVenueAsync(
                e.CallbackQuery.Message.Chat.Id,
                latitude,
                longitude,
                locationTitle,
                locationAddress,
                "Дивіться на Google Maps:",
                "https://www.google.com/maps/place/IT+Step+Academy/@49.857862,24.0258832,20.14z/data=!4m22!1m15!4m14!1m6!1m2!1s0x473add07ed06aaab:0x7d8c4188be4262fe!2z0LLRg9C70LjRhtGPINCX0LDQvNCw0YDRgdGC0LjQvdGW0LLRgdGM0LrQsCwgODPQsCwg0JvRjNCy0ZbQsiwg0JvRjNCy0ZbQstGB0YzQutCwINC-0LHQu9Cw0YHRgtGMLCA3OTAwMA!2m2!1d24.0268762!2d49.8581233!1m6!1m2!1s0x0:0x1537e180901077af!2s49.837230,+24.035886!2m2!1d24.035886!2d49.83723!3m5!1s0x473add07f1731fd1:0xbadadff90f884085!8m2!3d49.8578826!4d24.0259805!16s%2Fg%2F121jj357?hl=uk-UK&entry=ttu"
            );
            float latitude2 = 49.8042761f;
            float longitude2 = 24.0163526f;
            string locationTitle2 = "IT Step Academy 2";

            string locationAddress2 = "📍м. Львів, вул. Стрийська 45, ТОЦ Fabrik";

            await tgbot.SendVenueAsync(
                e.CallbackQuery.Message.Chat.Id,
                latitude2,
                longitude2,
                locationTitle2,
                locationAddress2,
                "Дивіться на Google Maps:",
                "https://www.google.com/maps/search/%D0%BC.+%D0%9B%D1%8C%D0%B2%D1%96%D0%B2,+%D0%B2%D1%83%D0%BB.+%D0%A1%D1%82%D1%80%D0%B8%D0%B9%D1%81%D1%8C%D0%BA%D0%B0+45,+%D0%A2%D0%9E%D0%A6+Fabrik/@49.8042761,24.0163526,17z?hl=uk-UK&entry=ttu");
            Buttons(chatId);
        }
        else if (buttonText=="Офіційний сайт")
        {
            await tgbot.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, $"Адреса: \n"+ "https://lviv.itstep.org/");
            Buttons(chatId);
        }
        else if (buttonText=="Напрямки")
        {
            await tgbot.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, $"Доступні напрямки:\n✓Програмуавння\n✓Дизайн\n✓Маркетинг\n✓Кібербезпека");
            Thread.Sleep(1000);
            await tgbot.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, $"Напишіть напрямок");
            directionsButtonClicked = true;
        }
    }
}