using OmniRiskAPI.Models;

namespace OmniRiskAPI.Services
{
    public class TwitterService
    {
        public Task<List<TweetModel>> GetTweetModels(string lat, string lng, double radius)
        {
            return Task.FromResult(new List<TweetModel>
            {
                new TweetModel
{
    Content = "Dziś odkryłem nową ulubioną kawiarnię. Nie mogę się doczekać, aby tam wrócić. #Kawa #Kawiarnia",
},
new TweetModel
{
    Content = "Przygotowuję się do maratonu! Treningi idą pełną parą. Trzymajcie kciuki! #Maraton #Sport",
},
new TweetModel
{
    Content = "Zapraszam do przeczytania mojego najnowszego artykułu na temat zdrowego stylu życia. Dajcie znać, co o nim sądzicie! #Zdrowie #StylŻycia",
},
new TweetModel
{
    Content = "Zainspirowany dzisiejszym wschodem słońca. Piękna chwila, by docenić naturę. #WschódSłońca #PięknoPrzyrody",
},
new TweetModel
{
    Content = "Czy wiecie, że dziś obchodzimy Światowy Dzień Zwierząt? Zwróćmy uwagę na ich dobrostan i ochronę. #DzieńZwierząt",
},
new TweetModel
{
    Content = "Wstrząsające wieści z sąsiedniego miasta. Mamy nadzieję, że poszkodowani po tragicznym wypadku autobusu szybko wrócą do zdrowia. #BezpieczeństwoNaDrogach",
},
new TweetModel
{
    Content = "Pożar w fabryce w centrum miasta! Dziękujemy strażakom za szybką reakcję i profesjonalne działania. Trzymajmy się z dala od obszaru zagrożonego. #BezpieczeństwoPubliczne",
},
new TweetModel
{
    Content = "Ostrzeżenie przed podtopieniami w okolicy. Bądźcie czujni i unikajcie miejsc narażonych na zalanie. #BezpieczeństwoWody",
},
new TweetModel
{
    Content = "Ważne informacje dla mieszkańców: wzrost przypadków włamań w naszej okolicy. Zwiększcie czujność i zabezpieczcie swoje domy i mieszkania. #BezpieczeństwoMieszkań",
},
new TweetModel
{
    Content = "Wczoraj wieczorem doszło do aktu wandalizmu w parku miejskim. Wspólnie dbajmy o nasze wspólne przestrzenie. #BezpieczeństwoPubliczne",
},
new TweetModel
{
    Content = "Silne burze i intensywne opady deszczu przewidywane na dziś wieczór. Pamiętajcie o bezpieczeństwie i chronieniu się przed niebezpiecznymi warunkami atmosferycznymi. #BezpieczeństwoPogoda",
},
new TweetModel
{
    Content = "Dziś dzień pełen wyzwań, ale niezłomnie idę do przodu. #motywacja"
}
            });
        }
    }
}
