namespace TestConsoleApp.LinqGrouping
{
    class LoyaltyCardBranchOffer
    {
        public LoyaltyCardBranchOffer(long id, long card, int merchant, long branch, long offer, int[] catgegories)
        {
            BranchOfferId = id;
            CardId = card;
            MerchantId = merchant;
            BranchId = branch;
            OfferId = offer;
            Categories = catgegories;
        }

        public long BranchOfferId { get; set; }
        public long BranchId { get; set; }
        public long CardId { get; set; }
        public int MerchantId { get; set; }
        public long OfferId { get; set; }
        public int[] Categories { get; set; }
    }
}
