using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp.LinqGrouping
{
    public class Runner : IRunner
    {
        public void RunProgram()
        {
            GroupBranches();
            GroupBranchOffers();
            //GroupComparisonQuotes();
        }

        private static void GroupBranchOffers()
        {
            var branchOffers = new LoyaltyCardBranchOffer[]
	        {
		        new LoyaltyCardBranchOffer(1, 1, 10, 50, 20, new[] { 1, 2 }),
		        new LoyaltyCardBranchOffer(2, 1, 10, 50, 21, new[] { 3 }),
		        new LoyaltyCardBranchOffer(3, 1, 10, 60, 20, new[] { 1, 2 }),
		        new LoyaltyCardBranchOffer(4, 2, 11, 70, 25, new[] { 1, 2 }),
		        new LoyaltyCardBranchOffer(4, 2, 11, 80, 25, new[] { 1, 2 })
	        };

            var grouped = branchOffers.GroupBy(c => new { Card = c.CardId, Merchant = c.MerchantId, Branch = c.BranchId })
                                        .Select(g => new
                                        {
                                            Card = g.Key.Card,
                                            Merchant = g.Key.Merchant,
                                            Branch = g.Key.Branch,
                                            Offers = g.Select(i => i.OfferId).ToArray(),
                                            Categories = g.SelectMany(i => i.Categories).ToArray()
                                        }).ToArray();
        }

        private static void GroupBranches()
        {
            var branches = new LoyaltyCardBranch[]
                           {
                               new LoyaltyCardBranch(1, 1, 10, new long[] { 50 }, new[] { 1, 2 }), 
                               new LoyaltyCardBranch(2, 1, 11, new long[] { 50, 60 }, new[] { 3 }),
                               new LoyaltyCardBranch(3, 2, 20, new long[] { 70, 75 }, new[] { 1, 3 })
                           };

            var groups = branches.GroupBy(b => new { Card = b.CardId, Branch = b.BranchId, Merchant = b.MerchantId })
                    .Select(g => new
                             {
                                 Card = g.Key.Card,
                                 Branch = g.Key.Branch,
                                 Merchant = g.Key.Merchant,
                                 Offers = g.SelectMany(b => b.OfferIds).ToArray(),
                                 Categories = g.SelectMany(b => b.Categories).ToArray()
                             }).ToArray();

            foreach (var groupedBranch in groups)
            {
                LoyaltyCardBranch.Print(groupedBranch.Card, groupedBranch.Branch, groupedBranch.Merchant, groupedBranch.Offers, groupedBranch.Categories);
            }
        }

        private static void GroupComparisonQuotes()
        {
            List<Comparison> comparisons = new List<Comparison>()
            {
                new Comparison()
                {
                    Id = 1,
                    Quotes = new List<Quote>()
                    {
                        new Quote()
                        {
                            Id = 1,
                            Status = "Completed"
                        },
                        new Quote()
                        {
                            Id = 2,
                            Status = "Warning"
                        },
                        new Quote()
                        {
                            Id = 3,
                            Status = "Completed"
                        }
                    }
                },
                new Comparison()
                {
                    Id = 2,
                    Quotes = new List<Quote>()
                    {
                        new Quote()
                        {
                            Id = 1,
                            Status = "Error"
                        }
                    }
                },
                new Comparison()
                {
                    Id = 3,
                    Quotes = new List<Quote>()
                    {
                        new Quote()
                        {
                            Id = 1,
                            Status = "Warning"
                        },
                        new Quote()
                        {
                            Id = 2,
                            Status = "Completed"
                        },
                        new Quote()
                        {
                            Id = 3,
                            Status = "Error"
                        }
                    }
                }
            };

            var query = from c in comparisons
                        from q in c.Quotes
                        group q by q.Status;

            foreach (var item in query)
            {
                Console.WriteLine("{0} {1}", item.Key, item.Count());
            }
        }
    }
}
