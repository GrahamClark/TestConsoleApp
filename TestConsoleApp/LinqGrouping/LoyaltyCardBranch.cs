using System;

namespace TestConsoleApp.LinqGrouping
{
    class LoyaltyCardBranch
    {
        public LoyaltyCardBranch(long card, int merchant, long branch, long[] offers, int[] catgegories)
        {
            CardId = card;
            MerchantId = merchant;
            BranchId = branch;
            OfferIds = offers;
            Categories = catgegories;
        }

        public long BranchId { get; set; }
        public long CardId { get; set; }
        public int MerchantId { get; set; }
        public long[] OfferIds { get; set; }
        public int[] Categories { get; set; }

        public static void Print(long cardId, long branchId, int merchantId, long[] offers, int[] categories)
        {
            Console.WriteLine(
                "CardId: {1}{0}BranchId: {2}{0}MerchantId: {3}{0}OfferIds: [{4}]{0}Categories: [{5}]{0}{0}",
                Environment.NewLine,
                cardId,
                branchId,
                merchantId,
                string.Join(",", offers),
                string.Join(",", categories));
        }
    }
}
