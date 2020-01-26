# SearchApp
Приложение для поиска ключевых слов по различным поисковым системам.
### Начну с минусов:
- Посмотрел API Гугла, Бинга, Яндекса. Все они либо платные, либо узкоспециализированные (например, гугл ищет только на определенном сайте, т.е. нужно создать правило). К тому-же в большинстве случаев пришлось бы писать для каждого апи отдельный клиент, что сложно для конечного пользователя. Потому - решил парсить результаты. Бинг понимает что я робот и отдает пустую выдачу, использование публичных прокси не помогало в ~90% случаев, потому предлагаю его не использовать :( )
- Тест написал всего лишь один.
- С логгером такая же ситуация, просто демонстрация что логгирование тоже умею подключать. Пишет он в файл, а не в БД, что тоже плохо, согласен.
- WebLayer знает о DataLayer, потому как пользуюсь стандартными средствами DI.
- Automapper тоже не использовал.
- В файле **OperationDetails.cs Result** имеет тип **object** и при вызове **Equals** в некоторых случаях теоретически может получиться StackOverflow или какая-либо другая ошибка.

```C#
...
		public object Result { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            if (!(obj is OperationDetails))
                return false;
            OperationDetails od = obj as OperationDetails;

            return Equals(Result, od.Result); //<- потенциальная проблема здесь
        }

        public override int GetHashCode()
        {
            int? hashCode = Succeedeed.GetHashCode() ^ Message?.GetHashCode() ^ Category?.GetHashCode();
            if (ReferenceEquals(Result, null))
                return hashCode ?? Succeedeed.GetHashCode();
            else
                return Result.GetHashCode() ^ hashCode ?? Succeedeed.GetHashCode(); //<- и здесь
        }
...
```

### Теперь к плюсам
- Сначала использовал **AngleSharp** для парсинга, но он какой-то кривой, с DOMом Гугла вообще отказался нормально работать, потому решил использовать **HtmlAgilityPack** с **Fizzler.Systems.HtmlAgilityPack** чтобы в качестве селекторов можно было использовать классы.
- Пользователь посмотрев DOM результатов поиска может понять какие селекторы ему необходимо. После этого он с легкостью может добавить новый поисковый движок в базу и осуществлять поиск по нему тоже.
- Совсем немного постарался над интерфейсом, надеюсь оцените :) (да, на главной внизу ссылки меню немного съезжают, думаю это мелочь)
- Несмотря на то что тест один, он, наверное, хорошо показывает понимание того, как тестировать код. Я надеюсь что покрытие тестами в задании требуется только для того, чтобы понять что разработчик умеет их писать и надеюсь одним единственным тестом я это смогу подтвердить.
- Потратил на анализ и разработку 4 часа + 10 на разработку, уложился в оценку (20-24 часа).
