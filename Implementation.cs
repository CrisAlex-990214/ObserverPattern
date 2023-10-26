namespace ObserverPattern
{
    public class PromoDiscount
    {
        public string Product { get; set; }
        public string Code { get; set; }
        public int DiscountPercentage { get; set; }
    }

    //Subject (Observable)
    public abstract class PromoDiscountNotifier
    {

        private List<IPromoDiscountListener> observers = new();

        public void AddObserver(IPromoDiscountListener observer) => observers.Add(observer);
        public void RemoveObserver(IPromoDiscountListener observer) => observers.Remove(observer);

        public async Task Notify(PromoDiscount promoDiscount)
        {
            await Task.WhenAll(observers.Select(x => x.Update(promoDiscount)));
        }
    }

    //Observer (Consumer)
    public interface IPromoDiscountListener
    {
        Task Update(PromoDiscount promoDiscount);
    }

    //Concrete Observer
    public class WebsiteConsumer : IPromoDiscountListener
    {
        public async Task Update(PromoDiscount promoDiscount)
        {
            await Task.Delay(3000);
            Console.WriteLine($"{nameof(WebsiteConsumer)} -> You have a promo code: ${promoDiscount.Code} with a " +
                $"{promoDiscount.DiscountPercentage}% discount for the product {promoDiscount.Product}");
        }
    }

    public class NewsletterConsumer : IPromoDiscountListener
    {
        public async Task Update(PromoDiscount promoDiscount)
        {
            await Task.Delay(1000);
            Console.WriteLine($"{nameof(NewsletterConsumer)} -> You have a promo code: ${promoDiscount.Code} with a " +
                $"{promoDiscount.DiscountPercentage}% discount for the product {promoDiscount.Product}");
        }
    }

    //Concrete Subject
    public class PromoDiscountService : PromoDiscountNotifier
    {
        public async Task SendPromoNotification(PromoDiscount promoDiscount) => await Notify(promoDiscount);
    }
}
