
using ObserverPattern;

var promoDiscountService = new PromoDiscountService();

var websiteConsumer = new WebsiteConsumer();
var newsletterConsumer = new NewsletterConsumer();

promoDiscountService.AddObserver(websiteConsumer);
promoDiscountService.AddObserver(newsletterConsumer);

await promoDiscountService.Notify(new() { Code = "A9F", DiscountPercentage = 30, Product = "Computer" });

promoDiscountService.RemoveObserver(websiteConsumer);
Console.WriteLine("The Website consumer has been removed!");

await promoDiscountService.Notify(new() { Code = "A9F", DiscountPercentage = 30, Product = "Computer" });

Console.ReadKey();